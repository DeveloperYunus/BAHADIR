using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "New Item")] 
public class Item : ScriptableObject
{
    public int itemID;
    // 1 = kilic, 2 = asa, 3 = hp iksiri, 4 = enerji iksiri (itemID için)
    public int itemClass;
    public Sprite itemImage;
    public List<string> itemName = new List<string>();
    public List<string> description = new List<string>();

    public float magicalDamage, physicalDamage, energyPrice;
    public int itemPrice;
}
//  iksirlerin can veya enerji artýrma degerleri cutDamage degerinden alýnacak
