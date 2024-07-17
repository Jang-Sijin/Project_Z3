using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CharIMGData", menuName = "Scriptable Object/CharIMGData")]
public class CharIMGData_CS : ScriptableObject
{
    [SerializeField]
    public Sprite[] sprites;
}
