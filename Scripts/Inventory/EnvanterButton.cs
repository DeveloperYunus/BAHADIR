using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnvanterButton : MonoBehaviour
{
    public GameObject infoPanel, beltedSwordBtn, HPElixir, EnergyElixir;
    public TMP_Text swordName, description, coinText;
    public GameObject swordImage;
    public EnvanterButtonNoSelect EBNS;
    public GameObject quickWCButton;
    AudioManager audioManager;

    [Header("Items")]
    public Item infoItem;
    public int forRemoveItemID;                                                    //bu ID ile remove edilecek itemin kacýndý item oldugunu hafýzada tutabilecez

    public Item lastSword;                                                         //save bilgileri
    public Item theWaeponBeforeLastWeapon;                                         //bir onecki silahý hýzla deðiþtirmek için hafýzada tutmaya yarayan deðiþken
    bool quickWeaponChange;
    public int lastSwordID, theWaeponBeforeLastWeaponID;
    public float ourCoin;

    public List<string> languageSwordDamageStr = new List<string>(); 
    public List<string> languageWandDamageStr = new List<string>();
    public List<string> languageEnergyStr = new List<string>();

    GameObject beltBtn, sellBtn, player, beltText, sellText;
    Image beltedSBtnImage, quickWCBtnImage;

    void OnEnable()
    {
        GetComponent<SaveLoad>().LoadItem();
        GetComponent<SaveLoad>().LoadSkill();
    }
    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        beltBtn = GameObject.Find("BeltButton");
        sellBtn = GameObject.Find("SellButton");
        player = GameObject.Find("Player");
        beltText = GameObject.Find("BeltText");
        sellText = GameObject.Find("SellText");
        beltedSBtnImage = beltedSwordBtn.transform.GetChild(0).GetComponent<Image>();
        quickWCBtnImage = quickWCButton.transform.GetChild(0).GetComponent<Image>();
        LastSwordCheck();
        LoadBeltSword();
        StartBeltSword();
        GameManager.GameM.DisplayItems();

        if (theWaeponBeforeLastWeapon && theWaeponBeforeLastWeapon != lastSword)
        {
            quickWeaponChange = true;
            if (quickWCButton)
                quickWCBtnImage.sprite = theWaeponBeforeLastWeapon.itemImage;
        }
        else quickWeaponChange = false;

        if (SceneManager.GetActiveScene().name != "Market")
        {
            if (quickWeaponChange)
            {
                quickWCButton.GetComponent<RectTransform>().DOScale(1, 0);
                quickWCButton.GetComponent<CanvasGroup>().DOFade(0.8f, 1f);
            }
            else
            {
                quickWCButton.GetComponent<RectTransform>().DOScale(0, 0);
                quickWCButton.GetComponent<CanvasGroup>().DOFade(0, 0f);
            }
        }       
    }
    void FixedUpdate()
    {
        if (Time.frameCount % 5 == 0) 
            coinText.text = ourCoin.ToString();
    }


    public void InfoOwnItem(int buttonID)//envanterdeki son týkladýgýmýz esyanýn bilgilerini verir
    {
        audioManager.playSound("itemInfo1");

        if (buttonID == -1) infoItem = lastSword;
        else infoItem = EBNS.GetThisItem(buttonID);

        forRemoveItemID = buttonID;
        if (GameManager.GameM.items.Count > buttonID) 
        {
            beltBtn.GetComponent<RectTransform>().DOScale(1, 0);
            sellBtn.GetComponent<RectTransform>().DOScale(1, 0);
            //panel yeni acýlýrken baska acýkken item deðiþince baska tepki veriyor
            if (infoPanel.GetComponent<CanvasGroup>().alpha == 0)
            {
                infoPanel.GetComponent<RectTransform>().DOScale(0.9f, 0f);                              
                infoPanel.GetComponent<RectTransform>().DOScale(1, 0.1f).SetEase(Ease.OutBack);
                infoPanel.GetComponent<CanvasGroup>().DOFade(1, 0.02f);
            }
            else 
            {
                infoPanel.GetComponent<RectTransform>().DOScale(0.9f, 0.02f);
                infoPanel.GetComponent<RectTransform>().DOScale(1, 0.1f).SetEase(Ease.OutBack);
                infoPanel.GetComponent<CanvasGroup>().DOFade(1, 0.02f);
            }

            swordName.text = infoItem.itemName[PlayerPrefs.GetInt("language")];
            //infoItem kýlýc veya iksire göre farklý tepki veriyor
            if (infoItem.itemClass == 3 | infoItem.itemClass == 4)
            {
                if(SceneManager.GetActiveScene().name=="Market")
                    beltBtn.GetComponent<RectTransform>().DOScale(0, 0);
                beltText.GetComponent<TextMeshProUGUI>().text = "Kullan";

                description.text = infoItem.description[PlayerPrefs.GetInt("language")];
            }
            else
            {
                beltText.GetComponent<TextMeshProUGUI>().text = "Kuþan";
                if(infoItem.itemClass == 1)//bu bir kýlýctýr
                {
                    description.text = languageSwordDamageStr[PlayerPrefs.GetInt("language")] + infoItem.physicalDamage + "\n" + 
                                       languageEnergyStr[PlayerPrefs.GetInt("language")] + infoItem.energyPrice + "\n\n" + infoItem.description[PlayerPrefs.GetInt("language")];
                }
                else
                    description.text = languageWandDamageStr[PlayerPrefs.GetInt("language")] + infoItem.magicalDamage + "\n" + 
                                       languageEnergyStr[PlayerPrefs.GetInt("language")] + infoItem.energyPrice + "\n\n" + infoItem.description[PlayerPrefs.GetInt("language")];
            }
            print(infoItem);
            if (SceneManager.GetActiveScene().name == "Market")
                sellText.GetComponent<TextMeshProUGUI>().text = (infoItem.itemPrice * 0.6f).ToString() + " Altýna Sat";
        }
        else
        {
            infoPanel.GetComponent<RectTransform>().DOScale(0, 0.1f);
            infoPanel.GetComponent<CanvasGroup>().DOFade(0, 0.02f);
        }
    }
    public void InfoEnchant(Item amulet)//týlsýmlarýn açýklamasý gözüksün diye
    {
        audioManager.playSound("itemInfo1");

        beltBtn.GetComponent<RectTransform>().DOScale(0, 0);
        sellBtn.GetComponent<RectTransform>().DOScale(0, 0);
        //panel yeni acýlýrken baska acýkken item deðiþince baska tepki veriyor
        if (infoPanel.GetComponent<CanvasGroup>().alpha == 0)
        {
            infoPanel.GetComponent<RectTransform>().DOScale(0.9f, 0f);
            infoPanel.GetComponent<RectTransform>().DOScale(1, 0.1f).SetEase(Ease.OutBack);
            infoPanel.GetComponent<CanvasGroup>().DOFade(1, 0.02f);
        }
        else
        {
            infoPanel.GetComponent<RectTransform>().DOScale(0.9f, 0.02f);
            infoPanel.GetComponent<RectTransform>().DOScale(1, 0.1f).SetEase(Ease.OutBack);
            infoPanel.GetComponent<CanvasGroup>().DOFade(1, 0.02f);
        }

        swordName.text = amulet.itemName[PlayerPrefs.GetInt("language")];
        description.text = amulet.description[PlayerPrefs.GetInt("language")];
    }
    public void BeltSwordCloseSellBtn()
    {
        if (forRemoveItemID == -1)
        {
            beltBtn.GetComponent<RectTransform>().DOScale(0, 0);
            sellBtn.GetComponent<RectTransform>().DOScale(0, 0);
        }
    }
    void StartBeltSword()
    {
        beltedSBtnImage.sprite = lastSword.itemImage;
        beltedSBtnImage.color = new Color(1, 1, 1, 1);
    }
    public void BeltSword()//kýlýclar icin kuþan iksirler için kullan(level kýsmýndan baska bir fonksiyonla halledimiþtir)
    {
        if (infoItem.itemClass == 1 | infoItem.itemClass == 2) 
        {
            audioManager.playSound("itemEquip1");
            GameManager.GameM.RemoveItemID(forRemoveItemID);
            GameManager.GameM.AddItem(lastSword);

            if (lastSword.itemClass == 1 && infoItem.itemClass == 2)// elimceki silah kýlýc ve giyeceðim silah asa ise
            {
                quickWeaponChange = true;
                theWaeponBeforeLastWeapon = lastSword;
                theWaeponBeforeLastWeaponID = lastSword.itemID;
                quickWCBtnImage.sprite = lastSword.itemImage;
            }
            else if (lastSword.itemClass == 2 && infoItem.itemClass == 1)//elimdeki silah asa ve giyeceðim silah kýlýc ise
            {
                quickWeaponChange = true;
                theWaeponBeforeLastWeapon = lastSword;
                theWaeponBeforeLastWeaponID = lastSword.itemID;
                quickWCBtnImage.sprite = lastSword.itemImage;
            }

            lastSword = infoItem;
            lastSwordID = infoItem.itemID;
            swordImage.GetComponent<SpriteRenderer>().sprite = lastSword.itemImage;
            swordImage.GetComponent<Image>().sprite = lastSword.itemImage;
            beltedSBtnImage.sprite = lastSword.itemImage;
            beltedSBtnImage.color = new Color(1, 1, 1, 1);
        }
        else
        {
            int a = Random.Range(0, 2);
            if (a == 0) audioManager.playSound("drinkPotion1");
            else audioManager.playSound("drinkPotion2");
        }

        infoItem = null;
        infoPanel.GetComponent<RectTransform>().DOScale(0f, 0.1f);
        infoPanel.GetComponent<CanvasGroup>().DOFade(0, 0.02f);
        if(player)
            player.GetComponent<SkillManaCost>().WriteManaCost();


        if (theWaeponBeforeLastWeapon && theWaeponBeforeLastWeapon != lastSword)//oyunun içindeyken silah deðiþince quickBtn aktif olsun diye
        {
            quickWeaponChange = true;
            if (quickWCButton)
                quickWCBtnImage.sprite = theWaeponBeforeLastWeapon.itemImage;
            print("asdadsss");
        }
        else quickWeaponChange = false;

        if (SceneManager.GetActiveScene().name != "Market")
        {
            GetComponent<PlayerSkillBtnShow>().CloseAndShowSkillBtn();                      //ayný kontrol üstte de vardý optimizasyon adýna kodu buraya aldýk

            if (quickWeaponChange)
            {
                quickWCButton.GetComponent<RectTransform>().DOScale(1, 0);
                quickWCButton.GetComponent<CanvasGroup>().DOFade(0.8f, 0.5f);
            }          
        }

        GetComponent<SaveLoad>().SaveSkill();
        GetComponent<SaveLoad>().SaveItem();
    }
    public void QuickWeaponChange()
    {
        if (theWaeponBeforeLastWeapon && !PlayerController.isTalking && !PlayerController.isDead)
        {
            bool onetime = true;
            audioManager.playSound("itemEquip1");
            int aaa = GameManager.GameM.items.Count;
            for (int i = 0; i < aaa; i++)
            {
                if (onetime && GameManager.GameM.items[i].itemID == theWaeponBeforeLastWeapon.itemID)
                {
                    onetime = false;
                    GameManager.GameM.RemoveItemID(i);
                }
            }
            quickWCBtnImage.sprite = lastSword.itemImage;

            GameManager.GameM.AddItem(lastSword);
            Item a = lastSword;
            int aa = lastSword.itemID;
            lastSword = theWaeponBeforeLastWeapon;
            lastSwordID = theWaeponBeforeLastWeapon.itemID;
            theWaeponBeforeLastWeapon = a;
            theWaeponBeforeLastWeaponID = aa;
            quickWeaponChange = true;

            swordImage.GetComponent<SpriteRenderer>().sprite = lastSword.itemImage;
            swordImage.GetComponent<Image>().sprite = lastSword.itemImage;
            beltedSBtnImage.sprite = lastSword.itemImage;
            beltedSBtnImage.color = new Color(1, 1, 1, 1);

            if (SceneManager.GetActiveScene().name != "Market")  
                GetComponent<PlayerSkillBtnShow>().CloseAndShowSkillBtn();
            if (player) 
                player.GetComponent<SkillManaCost>().WriteManaCost();
            GetComponent<SaveLoad>().SaveSkill();
            GetComponent<SaveLoad>().SaveItem();
        }
    }//hýzlca kýlýc ve asa arasýnda geciþ yapmayý saðlar
    public void LoadBeltSword()//iki adet resim atanmasýnýn sebebi: markette sprite rendere, levelde de image calýsmýyýo bu yuzden ýkýsýnýde kullandým.
    {
        swordImage.GetComponent<SpriteRenderer>().sprite = lastSword.itemImage;
        swordImage.GetComponent<Image>().sprite = lastSword.itemImage;
    }
    public void EnvanterButtonSave()//baska biyerden save yapmak ýstediðimizde bunu caðýracaz
    {
        GetComponent<SaveLoad>().SaveSkill();
        GetComponent<SaveLoad>().SaveItem();
    }                                                       
    public void LastSwordCheck()
    {
        int a = GameManager.GameM.LoadItems.Count;
        for (int i = 0; i < a; i++)                                                            //sahip oldugumuz eþyalarýn ininden herbirini tek tek kontrol eder
        {
            if (GameManager.GameM.LoadItems[i].itemID == lastSwordID)                          //esyalarýn adlarýný sýraylason kýlýcýn adýyla karsýlastýrýr. ayný olaný secer
            {
                lastSword = GameManager.GameM.LoadItems[i];
            }
        }

        int aa = GameManager.GameM.items.Count;
        for (int i = 0; i < aa; i++)                                                           //sahip oldugumuz eþyalarýn ininden herbirini tek tek kontrol eder
        {
            if (GameManager.GameM.items[i].itemID == theWaeponBeforeLastWeaponID)              //esyalarýn adlarýný sýrayla sondan önceki kýlýcýn adýyla karsýlastýrýr. ayný olaný secer
            {
                theWaeponBeforeLastWeapon = GameManager.GameM.items[i];
                return;
            }
            else
            {
                theWaeponBeforeLastWeapon = null;
            }
        }
    }
    public void ShowHPElixir(bool a)
    {
        bool b = false;
        int aa = GameManager.GameM.items.Count;
        for (int i = 0; i < aa; i++)
        {
            if (GameManager.GameM.items[i].itemClass == 3)
            {
                b = true;
                break;
            }
        }

        if (a && b)
        {
            HPElixir.GetComponent<CanvasGroup>().DOFade(0.8f, 0.5f);
            HPElixir.GetComponent<Button>().interactable = true;
        }
        else
        {
            HPElixir.GetComponent<CanvasGroup>().DOFade(0, 0.5f);
            HPElixir.GetComponent<Button>().interactable = false;
        }
    }
    public void ShowEnergyElixir(bool a)
    {
        bool b = false;
        int aa = GameManager.GameM.items.Count;
        for (int i = 0; i < aa; i++)
        {
            if (GameManager.GameM.items[i].itemClass == 4)
            {
                b = true;
                break;
            }
        }

        if (a & b)
        {
            EnergyElixir.GetComponent<CanvasGroup>().DOFade(0.8f, 0.5f);
            EnergyElixir.GetComponent<Button>().interactable = true;
        }
        else
        {
            EnergyElixir.GetComponent<CanvasGroup>().DOFade(0, 0.5f);
            EnergyElixir.GetComponent<Button>().interactable = false;
        }
    }

    private void OnApplicationQuit()//uygulama kapanmadan hemen once calýstýrýlýr
    {
        GetComponent<SaveLoad>().SaveSkill();
        GetComponent<SaveLoad>().SaveItem();
    }  
    public void denemeCoinGain()
    {
        ourCoin += 1000;
        GetComponent<SaveLoad>().SaveSkill();
        GetComponent<SaveLoad>().SaveItem();
    }
}
