using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IntroCameraControl : MonoBehaviour
{
    /*
     * ��Ʈ�ο��� ī�޶� �� �ڷ� �Դٸ� ���ٸ� �ϱ淡 ��ũ��Ʈ�� ®���ϴ�.
     * ������ ���ٰ� �ٽ� ���ƿ��� �����Դϴ�.
     */
    private Vector3 maxFrontPos;


    private float duration = 2f; // ���� �ð�
    private int loopCount = -1; // ���� �ݺ�

    private void Awake()
    {
        maxFrontPos = transform.position + (new Vector3(0, 0, 0.03f));
    }

    private void Start()
    {
        transform.DOMove(maxFrontPos, duration)
            .SetLoops(loopCount, LoopType.Yoyo) // Yoyo: ���� ��ġ�� �� ��ġ�� �ݺ�
            .SetEase(Ease.InOutQuad); // �������� �����̱�
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
