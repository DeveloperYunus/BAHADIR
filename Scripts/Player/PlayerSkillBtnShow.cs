using UnityEngine;

public class PlayerSkillBtnShow : MonoBehaviour
{
    public GameObject[] sword, staff;
    public EnvanterButton EB;

    void Start()
    {
        CloseAndShowSkillBtn();
    }

    public void CloseAndShowSkillBtn()
    {
        if (EB.lastSword.itemClass == 1)                                                   //elimde kýlýc var
        {            
            if (PlayerPrefs.GetInt("Skill7") != 0) 
                sword[1].SetActive(true);
            sword[0].SetActive(true);

            int aa = staff.Length;
            for (int i = 0; i < aa; i++)            
                staff[i].SetActive(false);                                        
        }
        else if (EB.lastSword.itemClass == 2)
        {
            int aa = sword.Length;
            for (int i = 0; i < aa; i++)            
                sword[i].SetActive(false);

            staff[0].SetActive(true);

            if (PlayerPrefs.GetInt("Skill12") != 0)
                staff[1].SetActive(true);
            if (PlayerPrefs.GetInt("Skill11") != 0)
                staff[2].SetActive(true);
            if (PlayerPrefs.GetInt("Skill10") != 0)
                staff[3].SetActive(true);
        }
    }
}
