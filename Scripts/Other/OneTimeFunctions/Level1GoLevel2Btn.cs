using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1GoLevel2Btn : MonoBehaviour
{
    public Achievement1 achv1;
    public Button button;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        button.interactable = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        button.interactable = false;
    }

    public void BtnGoLevel2()
    {
        achv1.YouAccept();
    }
}
