using System.Collections;
using UnityEngine;

public class _2GolemTalk : MonoBehaviour
{
    // [0] = buton1, [1] = buton2, [2] = buton3, [3] = player, [4] = Npc
    public string[] easyText = new string[5];
    [HideInInspector] public int speecCountOut;
    [HideInInspector] public bool npcTalkFirst;
    public Collider2D colliderr;

    /*silinecek*/
    bool silahiBiraktik;
    PlayerController player;
    AudioManager audioManager;

    public TextAsset csv;
    [HideInInspector] public string[] dialog;
    [HideInInspector] public int totalLanguage, currentLanguage;  //0 = türkçe, 1 = eng

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        Physics2D.IgnoreLayerCollision(0, 7, false);      //false demek iki layer birbiriyle etkileþebilir demek
        Physics2D.IgnoreLayerCollision(0, 2, false);
        silahiBiraktik = false;
        speecCountOut = 0;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        PlayerPrefs.SetString("kiliciVerdimi", "vermedi");

        totalLanguage = PlayerPrefs.GetInt("totalLanguage");
        currentLanguage = PlayerPrefs.GetInt("language");
        dialog = csv.text.Split(new string[] { "@", "\n" }, System.StringSplitOptions.None);
    }

    public void FirsWords()
    {
        easyText[0] = "Burasý neresi ?";
        easyText[1] = "Buraya nasýl geldim ?";
        easyText[2] = "Sen... kimsin, nesin ?";
    }
    public void EB(int butonNo, int speecCount)//NPC nin verdiði cevaplar
    {
        audioManager.playSound("talkBtn");
        switch (speecCount)
        {
            case 2:
                switch(butonNo)
                {
                    case 1:
                        easyText[4] = dialog[2 * totalLanguage + currentLanguage];
                        speecCountOut = 3;
                        GetComponent<_2GolemAchivment>().OtherEnemyTalkBurnedVillage(0f);
                        break;
                    case 2:
                        easyText[4] = dialog[3 * totalLanguage + currentLanguage];
                        speecCountOut = 4;
                        GetComponent<_2GolemAchivment>().OtherEnemyTalkBurnedVillage(0f);
                        break;
                    
                }
                break;

            case 3:
                switch (butonNo)
                {
                    case 1://kýlýcý verdi
                        easyText[4] = dialog[4 * totalLanguage + currentLanguage];
                        speecCountOut = 25;
                        EnemyAII.canIChasePlayer = false;
                        silahiBiraktik = true;
                        PlayerPrefs.SetString("kiliciVerdimi", "verdi");
                        GetComponent<_2GolemAchivment>().PlayerGiveSword();
                        GetComponent<_2GolemAchivment>().lightUp = true;
                        GetComponent<_2GolemAchivment>().GiveSwordLight();
                        break;
                    case 2://kýlýcý vermedi
                        easyText[4] = dialog[5 * totalLanguage + currentLanguage];
                        speecCountOut = 25;
                        break;
                }
                StartCoroutine(EndOfTalk());
                break;

            case 5:
                switch (butonNo)
                {
                    case 1://kýlýcý verdi
                        easyText[4] = dialog[6 * totalLanguage + currentLanguage];
                        speecCountOut = 25;
                        EnemyAII.canIChasePlayer = false;
                        silahiBiraktik = true;
                        PlayerPrefs.SetString("kiliciVerdimi","verdi");
                        GetComponent<_2GolemAchivment>().PlayerGiveSword();
                        GetComponent<_2GolemAchivment>().lightUp = true;
                        GetComponent<_2GolemAchivment>().GiveSwordLight();
                        //bir sonraki bölümde zerlik ona kýlýcýný versýn
                        break;
                    case 2://kýlýcý vermedi
                        easyText[4] = dialog[7 * totalLanguage + currentLanguage];
                        speecCountOut = 25;
                        break;
                }
                StartCoroutine(EndOfTalk());
                break;


        }
    }
    public void ForwardSpeech(int speecCount)//Sorular ve karþýlýklý konusmalar
    {
    /*  easyText[0] = "Ýçeriði ne?";
        easyText[1] = "Dolandýrýlmak istemiyorum saðol.";
        easyText[2] = null;

        easyText[3] = "Hoþgeldin yolcu.";
        easyText[4] = "...Hoþbulduk";
        speecCountOut = 1;
        npcTalkFirst = true;    */

        easyText[3] = null;
        easyText[4] = null;
        switch (speecCount)
        {
            case 0:
                easyText[3] = dialog[8 * totalLanguage + currentLanguage];
                easyText[4] = dialog[9 * totalLanguage + currentLanguage];
                speecCountOut = 1;
                npcTalkFirst = true;
                break;

            case 1:
                easyText[3] = dialog[10 * totalLanguage + currentLanguage];
                easyText[4] = dialog[11 * totalLanguage + currentLanguage];
                speecCountOut = 2;
                npcTalkFirst = true;
                break;

            case 2:
                easyText[0] = dialog[12 * totalLanguage + currentLanguage];
                easyText[1] = dialog[13 * totalLanguage + currentLanguage];
                easyText[2] = null;
                GetComponent<_2GolemAchivment>().OtherEnemyTalkBurnedVillage(1f);
                break;

            case 3:
                easyText[0] = dialog[14 * totalLanguage + currentLanguage];
                easyText[1] = dialog[15 * totalLanguage + currentLanguage];
                easyText[2] = null;
                break;

            case 4:
                easyText[3] = dialog[16 * totalLanguage + currentLanguage];
                easyText[4] = dialog[17 * totalLanguage + currentLanguage];
                speecCountOut = 5;
                npcTalkFirst = false;
                break;

            case 5:
                easyText[0] = dialog[18 * totalLanguage + currentLanguage];
                easyText[1] = dialog[19 * totalLanguage + currentLanguage];
                easyText[2] = null;
                break;
        }
    }

    IEnumerator EndOfTalk()
    {
        yield return new WaitForSeconds(2f);
        if (silahiBiraktik)
        {
            Physics2D.IgnoreLayerCollision(0, 7);
            Physics2D.IgnoreLayerCollision(0, 2);
        }
        colliderr.enabled = false;
        GetComponent<TalkWithNPC2>().FinishedTalk();
        player.GoLeft(false);
        player.GoRight(false);
    }

    private void OnDestroy()//yoksa bunun bu þekilde kalma olasýlýðý var
    {
        Physics2D.IgnoreLayerCollision(0, 7, false);
        Physics2D.IgnoreLayerCollision(0, 2, false);
    }
}