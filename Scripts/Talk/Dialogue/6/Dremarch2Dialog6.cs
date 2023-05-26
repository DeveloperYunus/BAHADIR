using UnityEngine;

public class Dremarch2Dialog6 : MonoBehaviour
{
    // [0] = buton1, [1] = buton2, [2] = buton3, [3] = player, [4] = Npc
    [HideInInspector] public string[] easyText = new string[5];
    [HideInInspector] public int speecCountOut;
    [HideInInspector] public bool npcTalkFirst;
    [HideInInspector] public string stringForOut;                                   //disariya bir cumle aktarmak gerekti�inde bunu kullanacaz
    AudioManager audioManager;

    //silinecekler buran�n alt�nda yaz�lacak
    [HideInInspector] public int threeQuestion;                                   //3 soruyuda sordugu zaman h�c mant�kl� de�il der bahad�r.

    public TextAsset csv;
    [HideInInspector] public string[] dialog;
    [HideInInspector] public int totalLanguage, currentLanguage;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        speecCountOut = 0;
        threeQuestion = 0;

        totalLanguage = PlayerPrefs.GetInt("totalLanguage");
        currentLanguage = PlayerPrefs.GetInt("language");
        dialog = csv.text.Split(new string[] { "@", "\n" }, System.StringSplitOptions.None);
    }

    public void FirsWords()
    {
        easyText[0] = dialog[0 * totalLanguage + currentLanguage];
        easyText[1] = null;
        easyText[2] = null;
    }
    public void EB(int butonNo, int speecCount)//NPC nin verdi�i cevaplar
    {
        audioManager.playSound("talkBtn");
        easyText[3] = null;
        switch (speecCount)
        {
            case 0:
                switch (butonNo)
                {
                    case 1:
                        easyText[3] = dialog[1 * totalLanguage + currentLanguage];
                        easyText[4] = dialog[2 * totalLanguage + currentLanguage];
                        speecCountOut = 1;
                        break;

                    case 2:
                        easyText[4] = dialog[3 * totalLanguage + currentLanguage];
                        speecCountOut = 1;
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


        easyText[3] = null;
        easyText[4] = null;
        switch (speecCount)
        {
            case 0:
                easyText[3] = dialog[4 * totalLanguage + currentLanguage];
                easyText[4] = dialog[5 * totalLanguage + currentLanguage];
                speecCountOut = 1;
                npcTalkFirst = false;
                break;

            case 1:
                easyText[3] = dialog[6 * totalLanguage + currentLanguage];
                easyText[4] = dialog[7 * totalLanguage + currentLanguage];
                speecCountOut = 100;
                npcTalkFirst = false;
                break;

        }
    }  
}