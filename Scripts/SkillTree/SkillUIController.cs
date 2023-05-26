using UnityEngine;
using TMPro;
using DG.Tweening;

public class SkillUIController : MonoBehaviour
{
    public TMP_Text header, stats, statsValue;
    public GameObject statsPanel;
    public Skill data;
    public bool isOpenStatsPanel;

    AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        isOpenStatsPanel = false;
        statsPanel.GetComponent<CanvasGroup>().DOFade(0, 0f);
        statsPanel.GetComponent<RectTransform>().DOScale(0, 0f);
    }


    public void ShowStats()
    {
        audioManager.playSound("button1");

        if (!isOpenStatsPanel)
        {
            isOpenStatsPanel = true;
            statsPanel.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
            statsPanel.GetComponent<RectTransform>().DOScale(1, 0f);

            header.text = "Bahadýrýn Deðerleri";
            stats.text = "Can" + "\n" + "Enerji" + "\n" + "Zýrh" + "\n\n" + "Fiziksel Hasar" + "\n" + "Büyü Hasarý" + "\n" + "Can Çalma" + "\n\n" +
                         "Can Yeilenmesi" + "\n" + "Enerji Yenilenmesi";
            statsValue.text = "% " + data.can + "\n" + "% " +  data.enerji + "\n" + "% " + data.zýrh + "\n\n" + "+% " + data.fizikselHasar + "\n" + "+% " + data.buyuHasari + "\n" +
                             "+% " + data.canCalma + "\n\n" + "x " + data.canYenilenmesi + "\n" + "x " + data.enerjiYenilenmesi;
        }
        else
        {
            isOpenStatsPanel = false;
            statsPanel.GetComponent<CanvasGroup>().DOFade(0, 0.2f);
            statsPanel.GetComponent<RectTransform>().DOScale(0, 0f).SetDelay(0.2f);
        }
    }
}
