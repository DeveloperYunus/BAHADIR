using UnityEngine;

public class PickUpAmulet : MonoBehaviour
{
    public AmuletShow amuletShow;                   //sa�l�k = 150, dayan�kl�l�k = 151, fiziksel h. = 152, b�y� h. = 153, alt�n s�t� = 154;
    public PlayerController pc;
    public int amuletCarpan�;                       //sa�l�k *3, fiziksel ve b�y� hasar� *2 , dayan�kl�l�k *1.5 (ama %100 y�z� ge�mesin.)

    AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        PlayerPrefs.SetInt(amuletShow.amuletItemID, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))                          //dayan�kl�l�k t�ls�m�n� amuletCarpan� = 50 olarak kaydettik. z�rh� heaplarken 50 kullan.
        {
            audioManager.playSound("amuletAchievement");
            PlayerPrefs.SetInt(amuletShow.amuletItemID, amuletCarpan�);
            Switchh();
            amuletShow.ActiveBtn();
            Destroy(gameObject);
        }
    }

    public void Switchh()
    {
        if (amuletShow.amuletItemID == "150") 
            pc.HealthAndEnergyControl();
    }
}
