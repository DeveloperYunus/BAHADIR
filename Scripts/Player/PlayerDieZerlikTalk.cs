using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlayerDieZerlikTalk : MonoBehaviour
{
    public TextMeshProUGUI Text;

    public void ZerlikTalk(bool specialTalk,string specialText)
    {
        int a;
        if (PlayerPrefs.GetInt("1.ilk.konusma") == 1)
            a = Random.Range(1, 5);//5 dahil deðil
        else
            a = Random.Range(1, 4);

        Text.transform.GetComponent<RectTransform>().DOScale(1, 0f);
        Text.transform.GetComponent<CanvasGroup>().DOFade(1, 1f);
        Text.transform.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetDelay(5f);
        Text.transform.GetComponent<RectTransform>().DOScale(0, 0f).SetDelay(5.5f);

        if (specialTalk)// özel bir þey yazdýrmak ýstersek diye istemessek (false,"") yaparýz
        {
            Text.text = "Zerlik Han: " +  specialText;
            if (specialText == null)
                Text.text = null;
        }
        else
        {
            switch (a)
            {
                case 1:
                    Text.text = "Zerlik Han: Hadi ama Bahadýr yapabilirsin.";
                    break;
                case 2:
                    Text.text = "Zerlik Han: Olmadý gibi tekrar denesen daha iyi.";
                    break;
                case 3:
                    Text.text = "Zerlik Han: Baþtan baþla bu görevi gecmen lazým.";
                    break;
                case 4:
                    Text.text = "Zerlik Han: Sormasaydýn, hahahahaha";
                    break;
            }
        }
    }
}
