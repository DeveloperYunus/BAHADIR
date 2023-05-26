using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ZerlikTalk3 : MonoBehaviour
{
    // [0] = buton1, [1] = buton2, [2] = buton3, [3] = player, [4] = Npc
    [HideInInspector] public string[] easyText = new string[5];
    [HideInInspector] public int speecCountOut;
    [HideInInspector] public bool npcTalkFirst;
    [HideInInspector] public string stringForOut;                                   //disariya bir cumle aktarmak gerektiðinde bunu kullanacaz

    
    //silinecek
    public GameObject ground1;
    public Light2D globalLight;
    public ParticleSystem fallingLeaf, soilParticle;
    AudioManager audioManager;

    public TextAsset csv;
    [HideInInspector] public string[] dialog;
    [HideInInspector] public int totalLanguage, currentLanguage;

    private void Start()
    {
        speecCountOut = 0;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        PlayerPrefs.DeleteKey("level3plyrkatlettimi");
        PlayerPrefs.DeleteKey("level3Secim");

        totalLanguage = PlayerPrefs.GetInt("totalLanguage");
        currentLanguage = PlayerPrefs.GetInt("language");
        dialog = csv.text.Split(new string[] { "@", "\n" }, System.StringSplitOptions.None);
    }

    public void FirsWords()
    {
        easyText[0] = null;
        easyText[1] = null;
        easyText[2] = null;
    }
    public void EB(int butonNo, int speecCount)//NPC nin verdiði cevaplar
    {
        audioManager.playSound("talkBtn");
        easyText[3] = null;// bu degeri case lerden birinde deðiþtirirsek bahadýr deðiþtirdiðimiz cumleyi soyler 
        switch (speecCount)
        {
            case 2:
                switch (butonNo)
                {
                    case 1:
                        easyText[4] = dialog[0 * totalLanguage + currentLanguage];
                        speecCountOut = 100;                        
                        break;
                    case 2:
                        easyText[4] = dialog[1 * totalLanguage + currentLanguage];
                        speecCountOut = 4;
                        break;
                    case 3:
                        easyText[3] = null;
                        speecCountOut = 10;
                        break;
                }
                break;
        }
    }
    public void ForwardSpeech(int speecCount)//Sorular ve karþýlýklý konusmalar
    {
    /*  easyText[0] = "Ýçeriði ne?";
        easyText[1] = "Dolandýrýlmak istemiyorum saðol.";
        easyText[2] = null;

        easyText[3] = // onemli not: bu null olamaz, olursa forwardspeech deki ilk if in içine giriyor kod
        easyText[4] = "...Hoþbulduk";
        speecCountOut = 1;
        npcTalkFirst = true;    
    */

        easyText[3] = null;
        easyText[4] = null;
        switch (speecCount)
        {
            case 0:
                if (PlayerPrefs.GetString("level3plyrkatlettimi") == "evet")
                {
                    easyText[3] = dialog[2 * totalLanguage + currentLanguage];
                    easyText[4] = dialog[3 * totalLanguage + currentLanguage];
                    PlayerController.specialDieCase = true;

                    StartCoroutine(WaitTalkFinish(6f, true));
                    audioManager.playSound("scaryDarkness");
                    globalLight.intensity = 0.1f;
                    StartCoroutine(WaitTalkFinish(0.2f, false));
                }
                else if (PlayerPrefs.GetString("kiliciVerdimi") == "verdi")
                {
                    easyText[3] = dialog[4 * totalLanguage + currentLanguage];
                    easyText[4] = dialog[5 * totalLanguage + currentLanguage];
                    GetComponent<Achievenmt3>().ZerlikGiveSwordLevel3();
                }
                else if (PlayerPrefs.GetString("level3Secim") == "yardimEtti") 
                {
                    easyText[3] = dialog[6 * totalLanguage + currentLanguage];
                    easyText[4] = dialog[7 * totalLanguage + currentLanguage];
                    GameObject.Find("EnvanterBackground").GetComponent<EnvanterButton>().ourCoin += 60;
                }
                else if (PlayerPrefs.GetString("level3Secim") == "yalanSoyledi")
                {
                    easyText[3] = dialog[8 * totalLanguage + currentLanguage];
                    easyText[4] = dialog[9 * totalLanguage + currentLanguage];
                    GameObject.Find("EnvanterBackground").GetComponent<EnvanterButton>().ourCoin -= 60;
                }
                else//hicbir secim yapmadýysa
                {
                    easyText[3] = dialog[10 * totalLanguage + currentLanguage];
                    easyText[4] = dialog[11 * totalLanguage + currentLanguage];
                }

                speecCountOut = 1;
                npcTalkFirst = true;
                break;

            case 1:
                easyText[3] = dialog[12 * totalLanguage + currentLanguage];
                easyText[4] = dialog[13 * totalLanguage + currentLanguage];
                speecCountOut = 25;
                npcTalkFirst = false;
                break;
        }
    }

    IEnumerator WaitTalkFinish(float time, bool a)
    {
        if (a)
        {
            yield return new WaitForSeconds(time);
            ground1.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerController>().extraJumpValue--;
            GameObject.Find("Player").GetComponent<PlayerController>().jumpForce = 4;
            soilParticle.Stop(false);
            yield return new WaitForSeconds(4f);
            soilParticle.Play(false);
        }
        else
        {
            yield return new WaitForSeconds(85 * time);

            for (int i = 0; i < 30; i++)
            {
                yield return new WaitForSeconds(time);
                globalLight.intensity += 0.013f;
            }
            soilParticle.Play(false);
            gameObject.SetActive(false);
        }
    }
}