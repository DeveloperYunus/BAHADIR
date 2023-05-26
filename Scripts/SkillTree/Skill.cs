using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill", fileName = "NewSkill")]
public class Skill : ScriptableObject
{
    public int skillID;
    public Sprite SkillSprite;
    public List<string> skillName = new List<string>();
    public List<string> skillDesc = new List<string>();

    [Header("Yüzde Olarak")]
    public float can; 
    public float zýrh, buyuHasari, fizikselHasar, enerji, canCalma;

    [Header("Kat Olarak(Yetenek)")]
    public float canYenilenmesi;
    public float enerjiYenilenmesi;

    [Header("Kat Olarak(Pasif)")]
    public float canYenilenmesiPasif;
    public float enerjiYenilenmesiPasif;

    [Header("")]
    public int skillLevel;
    public int maxSkillLevel;
    public bool isUpgrade;
}
