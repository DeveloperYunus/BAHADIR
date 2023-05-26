using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MarketSystem : MonoBehaviour
{
    public GameObject ownedItem;                                    
    public TMP_Text description;
    public TMP_Text swordName;
    public GameObject traderCanvas, traderPanel;
    public EnvanterButton EB;

    Item whichItem;
    AudioManager audioManager;
    GameObject BuyText, BuyBtn;

    public TextAsset csv;
    string[] dialog;
    [HideInInspector] public int totalLanguage, currentLanguage;  //0 = türkçe, 1 = eng             dialog[0 * totalLanguage + currentLanguage];

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        BuyText = GameObject.Find("BuyText");
        BuyBtn = GameObject.Find("BuyButton");

        PlayerPrefs.SetString("whereFrom", "Market");
        traderCanvas.GetComponent<RectTransform>().DOScale(0, 0f);
        traderPanel.GetComponent<Transform>().DOScale(0, 0f);
        traderCanvas.GetComponent<CanvasGroup>().DOFade(0, 0f);
        traderPanel.GetComponent<SpriteRenderer>().DOFade(0, 0f);

        totalLanguage = PlayerPrefs.GetInt("totalLanguage");
        currentLanguage = PlayerPrefs.GetInt("language");
        dialog = csv.text.Split(new string[] { "@", "\n" }, System.StringSplitOptions.None);
    }

    public void Buy()
    {
        if (whichItem && EB.ourCoin >= whichItem.itemPrice && GameManager.GameM.items.Count < GameManager.GameM.slots.Length)
        {
            int a = Random.Range(0, 2);
            if (a == 0) audioManager.playSound("coinBuy1");
            else audioManager.playSound("coinBuy2");

            EB.ourCoin -= whichItem.itemPrice;
            GameManager.GameM.AddItem(whichItem);

            if (whichItem.itemClass == 1 || whichItem.itemClass == 2) //kýlýc yada asa ise tekrar alýnmasýn diye button u kapat
            {
                BuyText.GetComponent<TextMeshProUGUI>().text = "";
                BuyBtn.GetComponent<Button>().interactable = false;
                GiveMeAFeedback();
            }
        }
        else if (whichItem && EB.ourCoin >= whichItem.itemPrice && whichItem.itemClass == 3 | whichItem.itemClass == 4) 
        {
            if (GameManager.GameM.items.Contains(whichItem))
            {
                int a = Random.Range(0, 2);
                if(a==0) audioManager.playSound("coinBuy1");
                else audioManager.playSound("coinBuy2");

                EB.ourCoin -= whichItem.itemPrice;
                EB.EnvanterButtonSave();
                GameManager.GameM.AddItem(whichItem);

                BuyText.GetComponent<TextMeshProUGUI>().text = "";
                BuyBtn.GetComponent<Button>().interactable = false;
            }
            else
            {
                SwordStatsShow();
                swordName.text = null;
                description.enableWordWrapping = enabled;
                description.text = dialog[0 * totalLanguage + currentLanguage];
            }
        }
        else if (GameManager.GameM.items.Count >= GameManager.GameM.slots.Length)
        {
            SwordStatsShow();
            swordName.text = null;
            description.enableWordWrapping = enabled;
            description.text = dialog[0 * totalLanguage + currentLanguage];
        }
        else
        {
            SwordStatsShow();
            swordName.text = null;
            description.enableWordWrapping = enabled;
            description.text = dialog[1 * totalLanguage + currentLanguage];
        }
        EB.EnvanterButtonSave();
    }
    public void SellItem()
    {
        audioManager.playSound("coinSell1");

        //satýlan eþyalar envanterden cýkacak ve ücreti paramýza eklanýp kaydedilecek
        EB.ourCoin += EB.infoItem.itemPrice * 0.6f;
        GameManager.GameM.RemoveItemID(EB.forRemoveItemID);
        EB.infoItem = null;

        EB.infoPanel.GetComponent<RectTransform>().DOScale(0f, 0.1f);
        EB.infoPanel.GetComponent<CanvasGroup>().DOFade(0, 0.02f);
        EB.EnvanterButtonSave();
    }

    public void ShowStat(Item item)
    {
        audioManager.playSound("itemInfo1");

        //hangi itemi sectiðimizi kaydeder
        whichItem = item;
        swordName.text = whichItem.itemName[PlayerPrefs.GetInt("language")];

        if (whichItem.itemClass == 3 | whichItem.itemClass == 4)                                                                                                      //bu eþya kýlýc deðil
        {
            description.text = whichItem.description[PlayerPrefs.GetInt("language")];
        }
        else
        {
            if (whichItem.itemClass == 1)//bu bir kýlýctýr            
                description.text = EB.languageSwordDamageStr[PlayerPrefs.GetInt("language")] + whichItem.physicalDamage + "\n" +
                    EB.languageEnergyStr[PlayerPrefs.GetInt("language")] + whichItem.energyPrice + "\n\n" + whichItem.description[PlayerPrefs.GetInt("language")];            
            else//bu bir asadýr
                description.text = EB.languageWandDamageStr[PlayerPrefs.GetInt("language")] + whichItem.magicalDamage + "\n" +
                    EB.languageEnergyStr[PlayerPrefs.GetInt("language")] + whichItem.energyPrice + "\n\n" + whichItem.description[PlayerPrefs.GetInt("language")];

            ownedItem.GetComponent<SpriteRenderer>().sprite = whichItem.itemImage;
            ownedItem.GetComponent<Image>().sprite = whichItem.itemImage;
        }

        SwordStatsShow();
        BuyText.GetComponent<TextMeshProUGUI>().text = item.itemPrice.ToString();
        BuyBtn.GetComponent<Button>().interactable = true;
    }
    public void CloseStatsTouchPanel()// ekrana týlayýnca ona seyler sýmdý panele týklayýn olacak
    {
        SwordStatsClose();
        EB.LoadBeltSword();
        BuyText.GetComponent<TextMeshProUGUI>().text = "";
        BuyBtn.GetComponent<Button>().interactable = false;
    }

    private void SwordStatsShow()
    {
        traderCanvas.GetComponent<RectTransform>().DOScale(1, 0.2f);
        traderPanel.GetComponent<Transform>().DOScaleY(1, 0.2f);
        traderPanel.GetComponent<Transform>().DOScaleX(-1, 0.2f);
        traderCanvas.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
        traderPanel.GetComponent<SpriteRenderer>().DOFade(1, 0.2f);
    }                               //tüccarýn bilgilendirme paneli açýlýþý
    private void SwordStatsClose()
    {
        traderCanvas.GetComponent<RectTransform>().DOScale(0, 0.2f);
        traderPanel.GetComponent<Transform>().DOScale(0, 0.2f);
        traderCanvas.GetComponent<CanvasGroup>().DOFade(0, 0.2f);
        traderPanel.GetComponent<SpriteRenderer>().DOFade(0, 0.2f);
    }

    void GiveMeAFeedback()
    {
        swordName.text = dialog[2 * totalLanguage + currentLanguage];
        description.text = dialog[3 * totalLanguage + currentLanguage];
    }
}