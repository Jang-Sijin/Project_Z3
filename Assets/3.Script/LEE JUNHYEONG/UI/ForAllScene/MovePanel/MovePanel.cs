using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core.Easing;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("����")]
    [SerializeField]
    private Vector2 direction;

    [Header("Ʈ���� �Ⱓ")]
    [SerializeField]
    private float duration;

    [SerializeField] private Ease ease;

    private void OnEnable()
    {

    }
}
