using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class SkillButton : MonoBehaviour
{
    public TextMeshProUGUI skillName, skillDesc;
    public TMP_Text ownSkillPointText;
    public GameObject skillImage;
    public GameObject skillInfoPanel;
    public SkillUIController skillUI;
    public Skill dataHolder;
    public TMP_Text currentBuf, nextBuff, bildiriText;

    [Header("")]
    public List<string> RegenSecondLanguage = new List<string>();
    public string[] skillPointLanguage;

    Skill infoSkill;
    AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        PlayerPrefs.SetString("whereFrom", "SkillTree");

        GetComponent<SaveLoad>().LoadItem();
        GetComponent<SaveLoad>().LoadSkill();
        skillInfoPanel.GetComponent<RectTransform>().DOScale(0, 0f);
        skillInfoPanel.GetComponent<CanvasGroup>().DOFade(0, 0f);
    }

    public void ShowSkillStats(Skill yetenek)
    {//yeteneðin ozelliklerini gösterir
        audioManager.playSound("itemInfo1");

        infoSkill = yetenek;
        if (infoSkill.skillID < 8) skillInfoPanel.GetComponent<RectTransform>().DOLocalMoveX(430, 0);
        else                       skillInfoPanel.GetComponent<RectTransform>().DOLocalMoveX(-430, 0);

        skillInfoPanel.GetComponent<RectTransform>().DOScale(1, 0f);
        skillInfoPanel.GetComponent<CanvasGroup>().DOFade(1, 0.3f);

        ownSkillPointText.text = skillPointLanguage[PlayerPrefs.GetInt("language")] + " " + dataHolder.skillLevel.ToString();
        skillImage.GetComponent<Image>().sprite = yetenek.SkillSprite;
        CurrentAndNextBuffShow();
    }
    public void UpgradeButton()
    {//yeteneði yükseltir        
        if (GetComponent<AllSkills>().SkillUpgrade(infoSkill) && infoSkill.skillLevel < infoSkill.maxSkillLevel && dataHolder.skillLevel > 0)
        {
            int a = Random.Range(0, 2);
            if(a==0) audioManager.playSound("button11");
            else audioManager.playSound("button12");

            infoSkill.skillLevel += 1;
            dataHolder.skillLevel -= 1;
            ownSkillPointText.text = skillPointLanguage[PlayerPrefs.GetInt("language")] + " " + dataHolder.skillLevel.ToString();
            GetComponent<AllSkills>().OpenSkillDarkness(infoSkill);
            PlayerPrefs.SetInt("Skill" + infoSkill.skillID.ToString(), infoSkill.skillLevel);

            //yeteneðin ismi + string olarak 1 objesini bulur text ine yeteneðin seviyesini yazar
            GameObject.Find(infoSkill.skillID.ToString() + "1").GetComponent<TextMeshProUGUI>().text = infoSkill.skillLevel.ToString();

            DataHolderFunction();
            GetComponent<SaveLoad>().SaveSkill();
            GetComponent<SaveLoad>().SaveItem();
        }
        else if (!GetComponent<AllSkills>().SkillUpgrade(infoSkill))
        {
            audioManager.playSound("button2");

            BildiriText(true);
            bildiriText.text = "Daha önceki yeteneði açmadým";
            StartCoroutine(BildiriDeShow());
        }
        else if (dataHolder.skillLevel <= 0)
        {
            audioManager.playSound("button2");

            BildiriText(true);
            bildiriText.text = "Yetenek puaným yok ya";
            StartCoroutine(BildiriDeShow());
        }
        else
        {
            audioManager.playSound("button2");

            BildiriText(true);
            bildiriText.text = "Zaten son seviyede";
            StartCoroutine(BildiriDeShow());
        }
        CurrentAndNextBuffShow();
    }
  
    public void ClearSkillPage()
    {
        skillInfoPanel.GetComponent<RectTransform>().DOScale(0, 0f).SetDelay(0.3f);
        skillInfoPanel.GetComponent<CanvasGroup>().DOFade(0, 0.3f);

        skillUI.statsPanel.GetComponent<RectTransform>().DOScale(0, 0f).SetDelay(0.3f);
        skillUI.statsPanel.GetComponent<CanvasGroup>().DOFade(0, 0.3f);
        skillUI.isOpenStatsPanel = false;
    }
    public void DataHolderFunction()
    {
        if (infoSkill.can != 0) 
        {
            dataHolder.can += infoSkill.can;
        }
        if (infoSkill.zýrh != 0)
        {
            dataHolder.zýrh += infoSkill.zýrh;
        }

        if (infoSkill.buyuHasari != 0)
        {
            dataHolder.buyuHasari += infoSkill.buyuHasari;
        }
        if (infoSkill.fizikselHasar != 0)
        {
            dataHolder.fizikselHasar += infoSkill.fizikselHasar;
        }

        if (infoSkill.enerji != 0)
        {
            dataHolder.enerji += infoSkill.enerji;
        }
        if (infoSkill.canCalma != 0)
        {
            dataHolder.canCalma += infoSkill.canCalma;
        }

        if (infoSkill.canYenilenmesi != 0)
        {
            dataHolder.canYenilenmesi += infoSkill.canYenilenmesi;
        }
        if (infoSkill.enerjiYenilenmesi != 0)
        {
            dataHolder.enerjiYenilenmesi += infoSkill.enerjiYenilenmesi;
        }

        if (infoSkill.canYenilenmesiPasif != 0)
        {
            dataHolder.canYenilenmesiPasif += infoSkill.canYenilenmesiPasif;
        }
        if (infoSkill.enerjiYenilenmesiPasif != 0)
        {
            dataHolder.enerjiYenilenmesiPasif += infoSkill.enerjiYenilenmesiPasif;
        }
    }
    public void CurrentAndNextBuffShow()
    {
        currentBuf.text = null;
        nextBuff.text = null;
        skillName.text = infoSkill.skillName[PlayerPrefs.GetInt("language")];
        skillDesc.text = infoSkill.skillDesc[PlayerPrefs.GetInt("language")];
        nextBuff.GetComponent<RectTransform>().DOScale(1, 0f);

        if (infoSkill.can > 0) 
        {
            currentBuf.text += "\n+% " + infoSkill.can * infoSkill.skillLevel;
            nextBuff.text += "\n>>>     +% " + infoSkill.can * (infoSkill.skillLevel + 1);
        }
        if (infoSkill.enerji > 0)
        {
            currentBuf.text += "\n+% " + infoSkill.enerji * infoSkill.skillLevel;
            nextBuff.text += "\n>>>     +% " + infoSkill.enerji * (infoSkill.skillLevel + 1);
        }
        if (infoSkill.zýrh > 0)
        {
            currentBuf.text += "\n+% " + infoSkill.zýrh * infoSkill.skillLevel;
            nextBuff.text += "\n>>>     +% " + infoSkill.zýrh * (infoSkill.skillLevel + 1);
        }
        if (infoSkill.fizikselHasar > 0)
        {
            currentBuf.text += "\n+% " + infoSkill.fizikselHasar * infoSkill.skillLevel;
            nextBuff.text += "\n>>>     +% " + infoSkill.fizikselHasar * (infoSkill.skillLevel + 1);
        }
        if (infoSkill.buyuHasari > 0)
        {
            currentBuf.text += "\n+% " + infoSkill.buyuHasari * infoSkill.skillLevel;
            nextBuff.text += "\n>>>     +% " + infoSkill.buyuHasari * (infoSkill.skillLevel + 1);
        }
        if (infoSkill.canCalma > 0)
        {
            currentBuf.text += "\n+% " + infoSkill.canCalma * infoSkill.skillLevel + "\n+% " + infoSkill.canCalma * infoSkill.skillLevel;
            nextBuff.text += "\n>>>     +% " + infoSkill.canCalma * (infoSkill.skillLevel + 1) + "\n>>>     +% " + infoSkill.canCalma * (infoSkill.skillLevel + 1);
        }

        if (infoSkill.canYenilenmesi > 0)
        {
            currentBuf.text += "\n+x " + infoSkill.canYenilenmesi * infoSkill.skillLevel;
            nextBuff.text += "\n>>>     +x " + infoSkill.canYenilenmesi * (infoSkill.skillLevel + 1); 
            if(infoSkill.canYenilenmesi==1)
            {
                if (infoSkill.skillLevel != 3)
                    skillDesc.text +="\n( " + infoSkill.skillLevel * 2 +" > "+ (infoSkill.skillLevel + 1) * 2 + RegenSecondLanguage[PlayerPrefs.GetInt("language")];
                else
                    skillDesc.text += "\n( " + infoSkill.skillLevel * 2 + RegenSecondLanguage[PlayerPrefs.GetInt("language")];
            }
        }
        if (infoSkill.enerjiYenilenmesi > 0)
        {
            currentBuf.text += "\n+x " + infoSkill.enerjiYenilenmesi * infoSkill.skillLevel; 
            nextBuff.text += "\n>>>     +x " + infoSkill.enerjiYenilenmesi * (infoSkill.skillLevel + 1);           
        }

        if (infoSkill.canYenilenmesiPasif > 0)
        {
            currentBuf.text += "\n+x " + infoSkill.canYenilenmesiPasif * infoSkill.skillLevel;
            nextBuff.text += "\n>>>     +x " + infoSkill.canYenilenmesiPasif * (infoSkill.skillLevel + 1);
        }
        if (infoSkill.enerjiYenilenmesiPasif > 0)
        {
            currentBuf.text += "\n+x " + infoSkill.enerjiYenilenmesiPasif * infoSkill.skillLevel;
            nextBuff.text += "\n>>>     +x " + infoSkill.enerjiYenilenmesiPasif * (infoSkill.skillLevel + 1);
        }

        if (infoSkill.skillLevel == infoSkill.maxSkillLevel) nextBuff.GetComponent<RectTransform>().DOScale(0, 0f);
    }

    void BildiriText(bool bildir)
    {
        if (bildir)
        {
            bildiriText.transform.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
            bildiriText.transform.GetComponent<RectTransform>().DOScale(1, 0f);
        }
        else
        {
            bildiriText.transform.GetComponent<CanvasGroup>().DOFade(0, 0.4f);
            bildiriText.transform.GetComponent<RectTransform>().DOScale(0, 0f).SetDelay(0.4f);
        }
    }
    IEnumerator BildiriDeShow()
    {
        yield return new WaitForSeconds(2f);
        BildiriText(false);
    }
}
