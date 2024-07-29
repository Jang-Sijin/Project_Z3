using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Config/MonsterInfo")]
public class Build_MonsterInfo : ScriptableObject
{
    public float maxHealth = 1000f;
    public float defaultAttackDamage = 10f;
}
