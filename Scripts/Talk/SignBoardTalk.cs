using UnityEngine;
using TMPro;

public class SignBoardTalk : MonoBehaviour
{
    public GameObject oldMan;
    public string[] bilgilendirme;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))        
            oldMan.GetComponent<TextMeshPro>().text = bilgilendirme[PlayerPrefs.GetInt("language")];       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))        
            oldMan.GetComponent<TextMeshPro>().text = null;        
    }
}
