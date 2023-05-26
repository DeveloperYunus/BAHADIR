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
            a = Random.Range(1, 5);//5 dahil de�il
        else
            a = Random.Range(1, 4);

        Text.transform.GetComponent<RectTransform>().DOScale(1, 0f);
        Text.transform.GetComponent<CanvasGroup>().DOFade(1, 1f);
        Text.transform.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetDelay(5f);
        Text.transform.GetComponent<RectTransform>().DOScale(0, 0f).SetDelay(5.5f);

        if (specialTalk)// �zel bir �ey yazd�rmak �stersek diye istemessek (false,"") yapar�z
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
                    Text.text = "Zerlik Han: Hadi ama Bahad�r yapabilirsin.";
                    break;
                case 2:
                    Text.text = "Zerlik Han: Olmad� gibi tekrar denesen daha iyi.";
                    break;
                case 3:
                    Text.text = "Zerlik Han: Ba�tan ba�la bu g�revi gecmen laz�m.";
                    break;
                case 4:
                    Text.text = "Zerlik Han: Sormasayd�n, hahahahaha";
                    break;
            }
        }
    }
}
