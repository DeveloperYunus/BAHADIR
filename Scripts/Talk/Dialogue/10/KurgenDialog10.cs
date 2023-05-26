using UnityEngine;

public class KurgenDialog10 : MonoBehaviour
{
    // [0] = buton1, [1] = buton2, [2] = buton3, [3] = player, [4] = Npc
    [HideInInspector] public string[] easyText = new string[5];
    [HideInInspector] public int speecCountOut;
    [HideInInspector] public bool npcTalkFirst;
    [HideInInspector] public string stringForOut;                                   //disariya bir cumle aktarmak gerekti�inde bunu kullanacaz

    //silinecekler
    public GameObject bahad�rSelfTalk, paperPrefab;
    public Strings[] dialogg;
    AudioManager audioManager;

    public TextAsset csv;
    string[] dialog;
    [HideInInspector] public int totalLanguage, currentLanguage;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        speecCountOut = 0;

        totalLanguage = PlayerPrefs.GetInt("totalLanguage");
        currentLanguage = PlayerPrefs.GetInt("language");
        dialog = csv.text.Split(new string[] { "@", "\n" }, System.StringSplitOptions.None);
    }

    public void FirsWords()
    {
        easyText[0] = "Muhtar senmisin";
        easyText[1] = null;
        easyText[2] = null;
    }
    public void EB(int butonNo, int speecCount)//NPC nin verdi�i cevaplar
    {
        audioManager.playSound("talkBtn");
        easyText[3] = null;
        switch (speecCount)
        {
            case 4:
                switch (butonNo)
                {
                    case 1:
                        easyText[4] = "Yine kar��la�acag�z.";
                        speecCountOut = 5;
                        break;

                    case 2:
                        easyText[4] = "Peki";
                        speecCountOut = 10;
                        break;
                }
                break;        
        }
    }
    public void ForwardSpeech(int speecCount)//Sorular ve kar��l�kl� konusmalar
    {
        /*
        easyText[0] = "��eri�i ne?";
        easyText[1] = "Doland�r�lmak istemiyorum sa�ol.";
        easyText[2] = null;

        easyText[3] = // onemli not bu null olamaz, olursa forwardspeech deki ilk if in i�ine girmiyor kod
        easyText[4] = "...Ho�bulduk";
        speecCountOut = 1;
        npcTalkFirst = true;    
        */

        //pitClose
        //pitOpen
        //timeTravel

        easyText[3] = null;
        easyText[4] = null;
        switch (speecCount)
        {
            case 0:
                easyText[3] = dialog[0 * totalLanguage + currentLanguage];
                easyText[4] = dialog[1 * totalLanguage + currentLanguage];
                speecCountOut = 1;
                npcTalkFirst = true;
                break;

            case 1:
                easyText[3] = dialog[2 * totalLanguage + currentLanguage];
                easyText[4] = dialog[3 * totalLanguage + currentLanguage];
                speecCountOut = 2;
                npcTalkFirst = true;
                break;

            case 2:
                easyText[3] = dialog[4 * totalLanguage + currentLanguage];
                easyText[4] = dialog[5 * totalLanguage + currentLanguage];
                speecCountOut = 3;
                npcTalkFirst = true;
                break;

            case 3:
                easyText[3] = dialog[6 * totalLanguage + currentLanguage];
                easyText[4] = dialog[7 * totalLanguage + currentLanguage];
                speecCountOut = 4;
                npcTalkFirst = false;
                break;

            case 4:
                easyText[3] = "";
                easyText[4] = "";
                speecCountOut = 4;
                npcTalkFirst = false;
                audioManager.playSound("disappear");
                GameObject aa = Instantiate(paperPrefab);
                for (int i = 0; i < dialogg.Length; i++)
                {
                    aa.GetComponent<PaperDynamic>().loreText[i] = dialogg[i].language[PlayerPrefs.GetInt("language")];
                    aa.transform.position = transform.position;
                }
                transform.position = bahad�rSelfTalk.transform.position;                                            //zerlik�n yeri de�i�ir

                Vector3 positioner = bahad�rSelfTalk.transform.position;
                positioner.y = 2.2f;
                bahad�rSelfTalk.transform.position = positioner;
                break;
        }
    }
}

[System.Serializable]
public class Strings
{
    public string[] language;
}