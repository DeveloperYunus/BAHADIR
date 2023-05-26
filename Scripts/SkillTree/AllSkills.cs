using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AllSkills : MonoBehaviour
{
    public Skill[] allSkill;
    public GameObject[] skillIcon;

    public Skill dataHolder;
    bool upgradable;

    AudioManager audioManager;

    private void Awake()
    {
        bool a = false;

        //sýfýrla deðerimiz true(=1) ise skilleri sýfýrla
        if (PlayerPrefs.GetInt("resetGame")==1)
        {
            PlayerPrefs.SetInt("resetGame", 0);
            a = true;
        }

        //yetenek seviyelerini her oyun acýldýgýnda seviye textlerine yazdýrýr
        for (int i = 0; i < allSkill.Length; i++)
        {
            if(a) PlayerPrefs.SetInt("Skill" + (i + 1).ToString(), 0);
            allSkill[i].skillLevel = PlayerPrefs.GetInt("Skill" + (i+1).ToString());
        }
    }
    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        int aa = allSkill.Length;
        for (int i = 0; i < aa; i++)
        {
            if(allSkill[i].skillLevel>0)
            {
                skillIcon[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                if(i+1<14)
                    skillIcon[i+1].GetComponent<Image>().color = new Color(255, 255, 255, 255);

                if (allSkill[i].skillID == 2) 
                {
                    skillIcon[4].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                }
                if(allSkill[i].skillID == 9)
                {
                    skillIcon[11].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                }
            }
        }
    }

    public void OpenSkillDarkness(Skill yetenek)
    {
        switch (yetenek.skillID)
        {
            case 1:
                skillIcon[1].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                break;
            case 2:
                skillIcon[2].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                skillIcon[4].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                break;
            case 3:
                skillIcon[3].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                break;
            case 4:
                skillIcon[6].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                break;
            case 5:
                skillIcon[5].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                break;
            case 6:
                skillIcon[6].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                break;

            case 8:
                skillIcon[8].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                break;
            case 9:
                skillIcon[9].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                skillIcon[11].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                break;
            case 10:
                skillIcon[10].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                break;
            case 11:
                skillIcon[13].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                break;
            case 12:
                skillIcon[12].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                break;
            case 13:
                skillIcon[13].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                break;
        }
    }
    public bool SkillUpgrade(Skill yetenek)
    {
        upgradable = false;
        switch (yetenek.skillID)
        {
            case 1:
                upgradable = true;
                break;
            case 2:
                if(allSkill[0].skillLevel>0)
                {
                    upgradable = true;
                }
                break;
            case 3:
                if (allSkill[1].skillLevel > 0)
                {
                    upgradable = true;
                }
                break;
            case 4:
                if (allSkill[2].skillLevel > 0)
                {
                    upgradable = true;
                }
                break;
            case 5:
                if (allSkill[1].skillLevel > 0)
                {
                    upgradable = true;
                }
                break;
            case 6:
                if (allSkill[4].skillLevel > 0)
                {
                    upgradable = true;
                }
                break;
            case 7:
                if (allSkill[5].skillLevel > 0 | allSkill[3].skillLevel > 0)
                {
                    upgradable = true;
                }
                break;


            case 8:
                upgradable = true;
                break;
            case 9:
                if (allSkill[7].skillLevel > 0)
                {
                    upgradable = true;
                }
                break;
            case 10:
                if (allSkill[8].skillLevel > 0)
                {
                    upgradable = true;
                }
                break;
            case 11:
                if (allSkill[9].skillLevel > 0)
                {
                    upgradable = true;
                }
                break;
            case 12:
                if (allSkill[8].skillLevel > 0)
                {
                    upgradable = true;
                }
                break;
            case 13:
                if (allSkill[11].skillLevel > 0)
                {
                    upgradable = true;
                }
                break;
            case 14:
                if (allSkill[12].skillLevel > 0 | allSkill[10].skillLevel > 0)
                {
                    upgradable = true;
                }
                break;
        }

        if (upgradable) return true;
        else return false;
    }

    public void YenileBtn()
    {
        audioManager.playSound("button1");

        for (int i = 0; i < allSkill.Length; i++)        
            PlayerPrefs.SetInt("Skill" + (i + 1).ToString(), 0);

        ResetGameData();
        Destroy(GameObject.Find("GameManager"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetGameData()
    {
        dataHolder.buyuHasari = 0;
        dataHolder.can = 0;
        dataHolder.canCalma = 0;
        dataHolder.canYenilenmesi = 0;
        dataHolder.canYenilenmesiPasif = 0;
        dataHolder.enerji = 0;
        dataHolder.enerjiYenilenmesi = 0;
        dataHolder.enerjiYenilenmesiPasif = 0;
        dataHolder.fizikselHasar = 0;
        dataHolder.zýrh = 0;
        dataHolder.skillLevel = 50;

        GetComponent<EnvanterButton>().EnvanterButtonSave();
    }
}
