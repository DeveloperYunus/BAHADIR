using DG.Tweening;
using UnityEngine;

public class Achievenmt3 : MonoBehaviour
{
    public SpriteRenderer sword;
    public GameObject swordBtn, beltedSwordInventory;

    void Start()
    {
        if (PlayerPrefs.GetString("kiliciVerdimi") == "verdi")
        {
            sword.DOFade(0, 0f);
            swordBtn.GetComponent<CanvasGroup>().DOFade(0, 0f);
            beltedSwordInventory.GetComponent<RectTransform>().DOScale(0, 0f);
            swordBtn.SetActive(false);
        }
    }
    public void ZerlikGiveSwordLevel3()
    {
        swordBtn.SetActive(true);
        swordBtn.GetComponent<CanvasGroup>().DOFade(0.8f, 0.5f);
        beltedSwordInventory.GetComponent<RectTransform>().DOScale(1, 0f);
        sword.DOFade(1, 0.5f);
    }
}
