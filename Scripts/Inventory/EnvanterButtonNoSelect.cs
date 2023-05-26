using UnityEngine;

public class EnvanterButtonNoSelect : MonoBehaviour
{
    //public int buttonID;
    private Item thisItem;    

    public Item GetThisItem(int buttonID)
    {
        int a = GameManager.GameM.items.Count;
        for (int i = 0; i < a; i++)
        {
            if (buttonID == i)
            {
                thisItem = GameManager.GameM.items[i];
            }   
        }
        
        return thisItem;
    }
}
