using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IntroCameraControl : MonoBehaviour
{
    /*
     * 인트로에서 카메라가 앞 뒤로 왔다리 갔다리 하길래 스크립트를 짰습니다.
     * 앞으로 갔다가 다시 돌아오는 로직입니다.
     */
    private Vector3 maxFrontPos;


    private float duration = 2f; // 지속 시간
    private int loopCount = -1; // 무한 반복

    private void Awake()
    {
        maxFrontPos = transform.position + (new Vector3(0, 0, 0.03f));
    }

    private void Start()
    {
        transform.DOMove(maxFrontPos, duration)
            .SetLoops(loopCount, LoopType.Yoyo) // Yoyo: 시작 위치와 끝 위치를 반복
            .SetEase(Ease.InOutQuad); // 선형으로 움직이기
    }

    //private void FixedUpdate()
    //{
    //    if(isMovingForward)
    //    {
    //        MoveFront();
    //    }
    //
    //    else
    //    {
    //        MoveBack();
    //    }
    //}
    //
    //private void MoveFront()
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, maxFrontPos, Speed * Time.deltaTime);
    //
    //    if (transform.position.z >= maxFrontPos.z)
    //    {
    //        isMovingForward = false;
    //    }
    //}
    //
    //private void MoveBack()
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, startPos, Speed * Time.deltaTime);
    //
    //    if (transform.position.z <= startPos.z)
    //    {
    //        isMovingForward = true;
    //    }
    //}
}
