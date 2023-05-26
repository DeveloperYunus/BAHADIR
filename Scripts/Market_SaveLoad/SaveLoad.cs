using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public  class SaveLoad : MonoBehaviour
{
    private int a;
    EnvanterButton eb;
    public Skill playerData;

    private void Start()
    {
        eb = GetComponent<EnvanterButton>();
    }

    public void SaveItem()//itemlar�n save k�sm�
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + "Item.bytes");
        SaveManagement save = new SaveManagement();

        save.lastSwordID = eb.lastSwordID;                                 //son k�l�c� kaydeder
        save.quickWeaponChance = eb.theWaeponBeforeLastWeaponID;      //sondan �nceki k�l�c� kaydeder
        save.PlyrCoin = eb.ourCoin;                                        //param�z� kaydeder

        int aa = GameManager.GameM.items.Count;
        int aaa = GameManager.GameM.LoadItems.Count;
        for (int i = 0; i < aa; i++)
        {                    // slot say�s� kadar d�ncek
            for (int ii = 0; ii < aaa; ii++)
            {             //senin mevcut item say�n
                if (GameManager.GameM.items[i].itemName[PlayerPrefs.GetInt("language")] == GameManager.GameM.LoadItems[ii].itemName[PlayerPrefs.GetInt("language")])
                {
                    a = (ii) * 1000 + GameManager.GameM.itemNumber[i];
                    save.itemNameDep[i] = a;
                    break;
                }
            }
        }                                        //itemlerin say�s� ve kendilerini kaydeder     
        binary.Serialize(file, save);
        file.Close();
        print("Item save");
    }
    public void SaveResetItem()//itemlar�n save k�sm�
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + "Item.bytes");
        SaveManagement save = new SaveManagement();

        save.lastSwordID = 0;                          //son k�l�c� kaydeder
        save.PlyrCoin = 0;                                           //param�z� kaydeder

        int d = GameManager.GameM.items.Count;
        for (int i = 0; i < d; i++)
        {                    // slot say�s� kadar d�ncek
            save.itemNameDep = null;
        }                                        //itemlerin say�s� ve kendilerini kaydeder     
        binary.Serialize(file, save);
        file.Close();
        print("Item save");
    }
    public void SaveSkill()//skill lerin save k�sm�
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + "Skill.bytes");
        SaveManagement save = new SaveManagement();
        
        save.PlyrHealth = playerData.can;
        save.PlyrEnergy = playerData.enerji;
        save.PlyrArmour = playerData.z�rh;
        save.PlyrMagicDmg = playerData.buyuHasari;
        save.PlyrPhysicalDmg = playerData.fizikselHasar;
        save.PlyrLifeeLeech = playerData.canCalma;
        save.PlayerSkillLevel = playerData.skillLevel;

        save.PlyrHPRegen = playerData.canYenilenmesi;
        save.PlyrHPRegenPassive = playerData.canYenilenmesiPasif;
        save.PlyrEnergyRegen = playerData.enerjiYenilenmesi;
        save.PlyrEnergyRegenPassive = playerData.enerjiYenilenmesiPasif;
        
        binary.Serialize(file, save);
        file.Close();
        print("Skill save");
    }

    public void LoadItem()//itemlerin y�klendi�i k�s�m
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/" + "Item.bytes", FileMode.Open);
        SaveManagement save = (SaveManagement)binary.Deserialize(file);                                 
        file.Close();

        if (!eb)
            eb = GetComponent<EnvanterButton>();
        eb.ourCoin = save.PlyrCoin;
        eb.lastSwordID = save.lastSwordID;
        eb.theWaeponBeforeLastWeaponID = save.quickWeaponChance;      //sondan �nceki k�l�c� geri y�kler

        int aa = save.itemNameDep.Length;
        for (int i = 0; i < aa; i++)
        {
            a = save.itemNameDep[i];

            GameManager.GameM.items.Add(GameManager.GameM.LoadItems[a / 1000]);
            GameManager.GameM.itemNumber.Add(a % 1000);
            GameManager.GameM.DisplayItems();
        }                                               // slot say�s� kadar d�ncek
        print("Item load");
    }
    public void LoadSkill()//skill lerin y�klendi�i k�s�m
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/" + "Skill.bytes", FileMode.Open);
        SaveManagement save = (SaveManagement)binary.Deserialize(file);
        file.Close();

        playerData.can = save.PlyrHealth;
        playerData.enerji = save.PlyrEnergy;
        playerData.z�rh = save.PlyrArmour;
        playerData.buyuHasari = save.PlyrMagicDmg;
        playerData.fizikselHasar = save.PlyrPhysicalDmg;
        playerData.canCalma = save.PlyrLifeeLeech;
        playerData.skillLevel = save.PlayerSkillLevel;

        playerData.canYenilenmesi = save.PlyrHPRegen;
        playerData.canYenilenmesiPasif = save.PlyrHPRegenPassive;
        playerData.enerjiYenilenmesi = save.PlyrEnergyRegen;
        playerData.enerjiYenilenmesiPasif = save.PlyrEnergyRegenPassive;
        print("Skill load");
    }
}
        
   

[System.Serializable]
public class SaveManagement
{
    public int lastSwordID, quickWeaponChance;
    public float PlyrCoin;

    public float PlyrHealth, PlyrEnergy, PlyrArmour, PlyrMagicDmg, PlyrPhysicalDmg, PlyrLifeeLeech;
    public float PlyrHPRegen, PlyrEnergyRegen, PlyrHPRegenPassive, PlyrEnergyRegenPassive;
    public int PlayerSkillLevel;

    public int[] itemNameDep= new int[GameManager.GameM.items.Count];
}
