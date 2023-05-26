using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GameM;
    public bool isPaused;

    public List<Item> LoadItems = new List<Item>();         //t�m e�yalar�n tutulacag� oyun bas�nda kontrol edilecek save item class �.

    public List<Item> items = new List<Item>();             //kac cesit itemim var
    public List<int> itemNumber = new List<int>();          //kac tane itemim var
    public GameObject[] slots;

    AudioManager audioManager;
    public Text txt;
    public float yenilenme;
    float zaman;

    private void Awake()                                    //GameManager i DontDestroyOnLoad a al�r
    {
        if (GameM==null)        
            GameM = this;        
        else
        {
            if (GameM != this)            
                Destroy(gameObject);            
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        Application.targetFrameRate = 245;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        PlayerPrefs.SetInt("totalLanguage", 2);
        //if (txt)
        //  InvokeRepeating("FPS", 0.5f,yenilenme);
    }
    void FPS()
    {
        zaman += (Time.deltaTime - zaman) * 0.1f;
        txt.text = (1 / zaman).ToString();
    }

    public void DisplayItems()
    {
        int aa = slots.Length;
        int aaa = items.Count;
        for (int i = 0; i < aa; i++)
        {
            if (i < aaa) 
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);                                 //slot remini g�ncelle
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].itemImage;

                slots[i].transform.GetChild(1).GetComponent<Text>().color = new Color(1, 1, 1, 1);                                  //slot textini guncelle
                if (itemNumber[i] > 1) 
                    slots[i].transform.GetChild(1).GetComponent<Text>().text = itemNumber[i].ToString();         //e�ya say�s�n� 1 den fazla ise g�ster 
                else 
                    slots[i].transform.GetChild(1).GetComponent<Text>().text = null;
            }
            else//baz� kald�r�lacak e�ylalar
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);                                 //slot remini g�ncelle
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;

                slots[i].transform.GetChild(1).GetComponent<Text>().color = new Color(1, 1, 1, 0);                                  //slot textini guncelle
                slots[i].transform.GetChild(1).GetComponent<Text>().text = null;                                                    //e�ya say�s�n� 1 den fazla ise g�ster 
            }   
        }
    }
    public void AddItem(Item _item)
    {
        if (_item.itemClass == 3 | _item.itemClass == 4)                    //bu bir iksir ve depolanabilir demektir
        {
            if (items.Count < slots.Length)
            {
                if (!items.Contains(_item))
                {
                    items.Add(_item);
                    itemNumber.Add(1);
                }
                else                
                    for (int i = 0; i < items.Count; i++)
                        if (_item == items[i])
                            itemNumber[i]++; 
            }
            else           
                for (int i = 0; i < items.Count; i++)               
                    if (_item == items[i])                    
                        itemNumber[i]++;                                   
        }
        else
        {
            if (items.Count < slots.Length)
            {
                items.Add(_item);
                itemNumber.Add(1);
            }
            else            
                print("Cantan doldu");           
        }
        DisplayItems();
    }
    public void RemoveItemID(int butonID)
    {
        if (items[butonID])
        {
            itemNumber[butonID]--;
            if (itemNumber[butonID] <= 0)                              //bu e�yay� kald�rmam�z laz�m
            {
                items.RemoveAt(butonID);
                itemNumber.Remove(itemNumber[butonID]);               
            }
          
        }
        else        
            print("burda " + items[butonID] + "e�yas� yok");
        
        DisplayItems();
    }
    public void RemoveItem(Item _item)
    {
        if (items.Contains(_item))
        {
            int aa = items.Count;
            for (int i = 0; i < aa; i++)
            {
                if (_item == items[i])
                {
                    itemNumber[i]--;
                    if (itemNumber[i] == 0)
                    {
                        items.Remove(items[i]);
                        itemNumber.Remove(itemNumber[i]);
                    }
                    break;
                }
            }
        }
        else
            print("burda " + _item + "e�yas� yok");

        DisplayItems();
    }

    //bunlar buttonlarda kullan�l�yor
    public void GoMarket()
    {
        audioManager.playSound("button1");
        StartCoroutine(LevelPass("Market", 0.8f));
    }
    public void Go1()
    {
        //oyun bas�nda hangi levele gidece�imizi anlamak i�in yapt�g�m�z PP yi olusturur.
        if (!PlayerPrefs.HasKey("nextLevelName") | PlayerPrefs.GetString("nextLevelName") == "0")
        {
            PlayerPrefs.SetString("nextLevelName", "1");
        }
        audioManager.playSound("button1");
            
        PlayerPrefs.SetString("whereFrom", "Adventure");//karakter maceradan donunce market yada k�taptan c�kmas�n
        StartCoroutine(LevelPass(PlayerPrefs.GetString("nextLevelName"), 0.8f));
    }
    public void GoSkillMenu()
    {
        audioManager.playSound("button1");
        StartCoroutine(LevelPass("SkillTree", 0.8f));
    }
    public void BaseOfOperation()
    {
        audioManager.playSound("button1");
        StartCoroutine(LevelPass("BaseOfOperation", 0.8f));
    }   
    public void GoManinMenu()//oyunu s�f�rlamak i�in kOB de kulland�g�m�z fonks�yon(ses e gerek yok)
    {
        StartCoroutine(LevelPass("MainMenu", 0.8f));
    }

    IEnumerator LevelPass(string a, float time)
    {
        GameObject.Find("LevelLoaderFade").GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(a);
        Destroy(gameObject);
    }

    public void GoLevelBugFixing(int a) 
    {
        audioManager.playSound("button1");
        StartCoroutine(LevelPass(a.ToString(), 0.8f));
    }

    public void L9GoOB(string a)
    {
        SceneManager.LoadScene(a);
        Destroy(gameObject);
    }
}
