using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core.Easing;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("방향")]
    [SerializeField]
    private Vector2 direction;

    [Header("트위닝 기간")]
    [SerializeField]
    private float duration;

    [SerializeField] private Ease ease;

    private void OnEnable()
    {

    }
}
