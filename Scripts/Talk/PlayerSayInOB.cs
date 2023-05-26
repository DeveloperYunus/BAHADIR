using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerSayInOB : MonoBehaviour
{
    public TextMeshPro playerTalk;
    public Dialog[] OBDialog;

    void Start()
    {
        playerTalk.GetComponent<TextMeshPro>().DOFade(0, 0);

        int aa = OBDialog.Length;
        for (int i = 0; i < aa; i++)
        {
            if (OBDialog[i].whatIsLevelID == int.Parse(PlayerPrefs.GetString("nextLevelName")))
            {
                playerTalk.GetComponent<TextMeshPro>().DOFade(1, 1f).SetDelay(1.5f);
                playerTalk.text = OBDialog[i].whatPlayerSay[PlayerPrefs.GetInt("language")];
                playerTalk.GetComponent<TextMeshPro>().DOFade(0, 1f).SetDelay(OBDialog[i].ReadTime);
            }
        }
    } 
}

[System.Serializable]
public class Dialog
{
    public string[] whatPlayerSay;
    public int whatIsLevelID;                       //bir sonraki levelin numarasý
    public float ReadTime;
}