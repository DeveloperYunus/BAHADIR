using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CSVReader : MonoBehaviour
{
    public TextAsset languageAsset;
    int dil;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            dil = 0;
            ReadCSV();
        }
        if (Input.GetMouseButtonDown(1))
        {
            dil = 1;
            ReadCSV();
        }
    }


    void ReadCSV()
    {
        string[] talks = languageAsset.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
   
        print(talks[2 + dil]);        
    }
}
