using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/CharacterInfo")]
public class CharacterInfo : ScriptableObject
{
    public float maxHealth = 1000;
    public float maxSkillPoint = 100;
    public float defaultAttackDamage = 10;
    public float[] normalAttackDamageMultiple;
    public float exSkillDamage = 12;
}
