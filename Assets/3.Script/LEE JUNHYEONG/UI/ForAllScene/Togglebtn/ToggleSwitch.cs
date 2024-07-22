using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class ToggleSwitch : MonoBehaviour, IPointerClickHandler
{
    [SerializeField, Range(0, 1f)] protected float SliderValue;

    public bool CurrentSliderValue { get; private set; }

    private Slider _slider;

    [Header("Animation")]
    [SerializeField, Range(0, 1f)] private float animationDuration = 0.5f;
    [SerializeField]
    private AnimationCurve sliderEase =
        AnimationCurve.EaseInOut(timeStart: 0, valueStart: 0, timeEnd: 1, valueEnd: 1);

    private Coroutine animateSlider_co;

    [Header("Events")]
    [SerializeField] private UnityEvent onToggleOn;
    [SerializeField] private UnityEvent onToggleOff;

    protected Action transitionEffect;

    protected void OnValidate()
    {
        SetupToggleComponents();

        _slider.value = SliderValue;
    }

    private void SetupToggleComponents()
    {
        if (_slider != null)
            return;

        SetupSliderComponent();
    }

    private void SetupSliderComponent()
    {
        _slider = GetComponent<Slider>();

        if (_slider == null)
        {
            Debug.Log(message : "�����̴��� �����ϴ�.", context: this);
            return;
        }

        _slider.interactable = false;
        var sliderColors = _slider.colors;
        sliderColors.disabledColor = Color.white;
        _slider.colors = sliderColors;
        _slider.transition = Selectable.Transition.None; 
    }

    protected virtual void Awake()
    {
        SetupToggleComponents();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Toggle();
    }

    private void Toggle()
    {
        SetStateAndStartAnimation(!CurrentSliderValue);
    }
    public void ToggleByGroupManager(bool valueToSetTo)
    {
        SetStateAndStartAnimation(valueToSetTo);
    }


    private void SetStateAndStartAnimation(bool state)
    {
        CurrentSliderValue = state;

        if (CurrentSliderValue)
            onToggleOn?.Invoke(); // ?. �����ڴ� �ǿ������� null ���θ� �Ǻ����ִ� �������� �����ִ� �������Դϴ�.

        else
            onToggleOff?.Invoke();

        if (animateSlider_co != null)
        {
            StopCoroutine(animateSlider_co);
        }

        animateSlider_co = StartCoroutine(routine: AnimateSider());
    }

    private IEnumerator AnimateSider()
    {
        float startValue = _slider.value;
        float endValue = CurrentSliderValue ? 1 : 0; // ������ ��� �ݿ��� ������ �Ӱ��� float���� ��ȯ

        float time = 0;

        if (animationDuration > 0)
        {
            while (time < animationDuration) // ������ �ִϸ��̼� �ð�����
            {
                time += Time.deltaTime;

                float lerpFactor = sliderEase.Evaluate(time / animationDuration);
                _slider.value = SliderValue = Mathf.Lerp(startValue, endValue, lerpFactor);

                transitionEffect?.Invoke(); // �� �ٲ�� �̺�Ʈ ȣ��

                yield return null;
            }    
        }

            _slider.value = endValue; 
    }
}
