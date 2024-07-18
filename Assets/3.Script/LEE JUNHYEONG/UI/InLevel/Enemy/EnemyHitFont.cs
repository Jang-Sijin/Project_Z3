using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine.Experimental.AI;
using System.Globalization;

public class EnemyHitFont : MonoBehaviour
{
    private TMP_Text textMesh;
    private RectTransform rectTransform;
    [SerializeField] private float minimumScale;
    [SerializeField] private float endScale;
    [SerializeField] private RectTransform canvas;

    private Tween[] tweens; 

    private void Awake()
    {
        textMesh = GetComponent<TMP_Text>();
        rectTransform = GetComponent<RectTransform>();
        tweens = new Tween[2];
    }


    private void OnEnable()
    {
        KillTweening();
        Init_Font();

        float randomYPos = Random.Range(-(canvas.rect.width * 0.5f), (canvas.rect.width * 0.5f));
        float randomXPos = Random.Range(-(canvas.rect.height* 0.5f), (canvas.rect.height * 0.5f));

        rectTransform.position = new Vector3(randomXPos, randomYPos, 0);

        
        tweens[0] = textMesh.rectTransform.DOAnchorPos3DY(randomYPos - 0.5f, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            tweens[1] = textMesh.DOFade(0f, 2f);// ��Ʈ ������� ����
        });// ��Ʈ Ƣ����� ����
    }

    private void KillTweening()
    {
        for (int i = 0; i < tweens.Length; i++)
        {
            if(tweens[i] != null)
            {
                tweens[i].Kill();
            }
        }
    }
    
    private void Init_Font() // ��Ʈ�� ����ȭ �մϴ�.
    {
        Color Invisible;

        textMesh.fontSize = minimumScale;
        Invisible = textMesh.color;
        Invisible.a = 0f;
        textMesh.color = Invisible;
    }
    //********************************************************************************************************************************************
    // �̿ϼ� �߰� ����Դϴ�.

    //private IEnumerator ChangeSizeAndA_co()
    //{
    //    textMesh.DOFade(1f, 1f);
    //    WaitForSeconds wfs = new WaitForSeconds(scaleDuration);
    //
    //    /*
    //     * ���� ���ý� �� ��� �ͼ� ũ�⸦ ��� ��ǥ���� ������ ��ǥ�� �ٲ۴�.
    //     */
    //
    //    for (int i = 0; i < textMesh.textInfo.characterCount; i++)
    //    {
    //        TMP_CharacterInfo charInfo = textMesh.textInfo.characterInfo[i];
    //        int materialIndex = charInfo.materialReferenceIndex;
    //        int vertexIndex = charInfo.vertexIndex;
    //    
    //        Vector3[] defaultVertPos = (Vector3[])textMesh.textInfo.meshInfo[materialIndex].vertices.Clone(); // ���� ���� ��
    //    
    //        Vector3[] vertices = textMesh.textInfo.meshInfo[i].vertices;// ��¥ ����
    //        
    //        Vector3 charCenter = (defaultVertPos[vertexIndex] + defaultVertPos[vertexIndex + 2]) / 2; // ���� ����
    //    
    //        vertices[0] = charCenter;
    //        vertices[1] = charCenter;
    //        vertices[2] = charCenter;
    //        vertices[3] = charCenter;
    //    
    //        DOTween.To(
    //            () => vertices[vertexIndex]
    //        , x => vertices[vertexIndex] = x
    //        , defaultVertPos[vertexIndex], scaleDuration
    //        );
    //        
    //        DOTween.To(
    //             () => vertices[vertexIndex + 1]
    //         , x => vertices[vertexIndex + 1] = x
    //         , defaultVertPos[vertexIndex + 1], scaleDuration
    //         );
    //        
    //        DOTween.To(
    //            () => vertices[vertexIndex + 2]
    //        , x => vertices[vertexIndex + 2] = x
    //        , defaultVertPos[vertexIndex + 2], scaleDuration
    //        );
    //        
    //        DOTween.To(
    //            () => vertices[vertexIndex + 3]
    //        , x => vertices[vertexIndex + 3] = x
    //        , defaultVertPos[vertexIndex + 3], scaleDuration
    //        );
    //    
    //        yield return wfs;
    //    }
    //}

}
