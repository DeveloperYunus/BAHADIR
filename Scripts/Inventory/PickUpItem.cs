using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Item itemData;
    AudioManager audioManager;


    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.GameM.items.Count < GameManager.GameM.slots.Length)
            {
                audioManager.playSound("itemPick1");
                
                Destroy(gameObject);
                GameManager.GameM.AddItem(itemData);
            }
            else
            {
                print("daha fazla esya alamazsýn"); 
            }
        }
    }
}
