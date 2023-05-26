using UnityEngine;

public class PickUpAmulet : MonoBehaviour
{
    public AmuletShow amuletShow;                   //saðlýk = 150, dayanýklýlýk = 151, fiziksel h. = 152, büyü h. = 153, altýn sütü = 154;
    public PlayerController pc;
    public int amuletCarpaný;                       //saðlýk *3, fiziksel ve büyü hasarý *2 , dayanýklýlýk *1.5 (ama %100 yüzü geçmesin.)

    AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        PlayerPrefs.SetInt(amuletShow.amuletItemID, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))                          //dayanýklýlýk týlsýmýný amuletCarpaný = 50 olarak kaydettik. zýrhý heaplarken 50 kullan.
        {
            audioManager.playSound("amuletAchievement");
            PlayerPrefs.SetInt(amuletShow.amuletItemID, amuletCarpaný);
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
