using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearButton : MonoBehaviour
{
    [SerializeField] private Button _clearButton;
    
    void Start()
    {
        _clearButton.onClick.AddListener(() => SceneManagerEx.Instance.LoadScene(Define.SceneType.Home));
    }
}
