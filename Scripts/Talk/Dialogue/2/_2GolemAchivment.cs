using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;

public class _2GolemAchivment : MonoBehaviour
{
    public SpriteRenderer sword;
    public GameObject swordBtn, swordBtnInventory;
    public TextMeshPro textEnemy1, textEnemy2;    
    public Light2D giveSwordLight;
    public bool lightUp;

    public static bool swordOn;
    //kýlýcýmý çekebilirmiyim onu gösteriyor, bazen kýlýcýmý bir sebepten kullanamýyorsam ve kýlýc kusanma tusuna basarsam ontrol edeyim diye


    private void Start()
    {
        swordOn = true;
        lightUp = false;
    }

    public void PlayerGiveSword()
    {
        swordOn = false;
        sword.DOFade(0, 0f);
        swordBtnInventory.GetComponent<RectTransform>().DOScale(0, 0);
        swordBtn.SetActive(false);
        // 0 dan baslar saymaya animasyon scale ine eriþtiðinden ben DoScale(0,0); yapamýyorum
    }
    public void OtherEnemyTalkBurnedVillage(float fadeAmount)
    {
        textEnemy1.text = GetComponent<_2GolemTalk>().dialog[0 * GetComponent<_2GolemTalk>().totalLanguage + GetComponent<_2GolemTalk>().currentLanguage];
        textEnemy1.DOFade(fadeAmount, 0.8f);
        textEnemy2.text = GetComponent<_2GolemTalk>().dialog[1 * GetComponent<_2GolemTalk>().totalLanguage + GetComponent<_2GolemTalk>().currentLanguage];
        textEnemy2.DOFade(fadeAmount, 0.8f).SetDelay(1f);
    }

    public void GiveSwordLight()
    {
        if (lightUp) StartCoroutine(GiveSwordLightEnum());
    }
    IEnumerator GiveSwordLightEnum()
    { 
        for (int i = 0; i < 170; i++)
        {
            yield return new WaitForSeconds(0.1f);
            if (giveSwordLight.intensity <= 0.6f) giveSwordLight.intensity += 0.07f;
            else giveSwordLight.intensity += Random.Range(0.1f, -0.1f);
        }
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.1f);
            giveSwordLight.intensity -= 0.07f;
        }
        giveSwordLight.intensity = 0;
    }

}