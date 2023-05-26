using UnityEngine;
using DG.Tweening;

public class OperationBase : MonoBehaviour
{
    [Header("SceneLoad")]
    public GameObject btnMarket;
    public GameObject btnSkill;
    public int whichOne;


    private void Start()
    {
        Time.timeScale = 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (whichOne == 1)
            {
                btnMarket.GetComponent<RectTransform>().DOScale(1, 0f);
                btnMarket.GetComponent<CanvasGroup>().DOFade(1, 0.4f);
            }
            if (whichOne == 2)
            {
                btnSkill.GetComponent<RectTransform>().DOScale(1, 0f);
                btnSkill.GetComponent<CanvasGroup>().DOFade(1, 0.4f);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (whichOne == 1)
            {
                btnMarket.GetComponent<RectTransform>().DOScale(1, 0f);
                btnMarket.GetComponent<CanvasGroup>().DOFade(1, 0.4f);
            }
            if (whichOne == 2)
            {
                btnSkill.GetComponent<RectTransform>().DOScale(1, 0f);
                btnSkill.GetComponent<CanvasGroup>().DOFade(1, 0.4f);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (whichOne == 1)
            {
                btnMarket.GetComponent<RectTransform>().DOScale(0, 0f).SetDelay(0.4f);
                btnMarket.GetComponent<CanvasGroup>().DOFade(0, 0.4f);
            }
            if (whichOne == 2)
            {
                btnSkill.GetComponent<RectTransform>().DOScale(0, 0f).SetDelay(0.4f);
                btnSkill.GetComponent<CanvasGroup>().DOFade(0, 0.4f);
            }
        }
    }
}
