using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5MageTrigger : MonoBehaviour
{
    public WitchDialog5 ww;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            ww.ForTWNPC5();
    }
}
