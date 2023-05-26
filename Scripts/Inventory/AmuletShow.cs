using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AmuletShow : MonoBehaviour
{
    public string amuletItemID;
    public Image image;
    public Button btn;

    void Start()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        btn = GetComponent<Button>();

        if(PlayerPrefs.GetString(amuletItemID) != "yes")        //týlsýmý almadýysak
        {
            image.DOFade(0,0);
            btn.interactable = false;
        }
        else
        {
            image.DOFade(1, 0);
            btn.interactable = true;
        }
    }

    public void ActiveBtn()
    {
        image.DOFade(1, 0);
        btn.interactable = true;
    }
}
