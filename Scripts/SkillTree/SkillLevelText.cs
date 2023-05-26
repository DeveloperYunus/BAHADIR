using UnityEngine;
using TMPro;

public class SkillLevelText : MonoBehaviour
{
    public Skill ownSkill; 
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = ownSkill.skillLevel.ToString();
    }   
}
