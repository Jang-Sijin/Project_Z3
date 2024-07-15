using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class Intro_TVContorl : MonoBehaviour
{
    /*
     * �迭�� ���� Ŭ���� ��Ƽ� ����մϴ�.
     * ȭ���� ��ġ�ϸ� Ƽ���� Ŭ���� �ٲ�ϴ�.
     */

    [SerializeField]private VideoClip[] videoClips = new VideoClip[5];
    private VideoClip CurVideo;
    private VideoPlayer player;
    public VideoPlayer _player => player;
    private Tween tween;
    private IntroUI introUI;

    Camera mainCamera;
    RaycastHit hit;

    private void Start()
    {
        mainCamera = Camera.main;
        player = GetComponent<VideoPlayer>();

        nextVideo(player);

        player.loopPointReached += nextVideo;
    }
    public void nextVideo(VideoPlayer Player)
    {
        while (CurVideo == player.clip)
        {
            player.clip = videoClips[Random.Range(0, 5)];
        }
 
        player.Play();
        CurVideo = player.clip;
    }
}
