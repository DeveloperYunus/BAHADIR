using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightSetWithCollider : MonoBehaviour
{
    public Color hedefRenk;
    public static bool isDark;                                              //hava açýkmý karanlýkmý
    public bool isDarkForStatic, goRigthForDark;

    Light2D lightt;
    bool goDark;

    private void Start()
    {
        isDark = isDarkForStatic;
        lightt = GetComponentInParent<Light2D>();
    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            /*if (goRigthForDark)
            {
                if (other.transform.localScale.x < 0) goDark = true;
                else goDark = false;
            }
            else
            {
                if (other.transform.localScale.x < 0) goDark = false;
                else goDark = true;
            }*/

            goDark = true;
            StartCoroutine(LightChange(2, isDark));
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            goDark = false;
            StartCoroutine(LightChange(2, isDark));
        }
    }

    IEnumerator LightChange(float time, bool a)
    {
        float b, r, g;
        if (!a)
        {
            b = lightt.color.b - hedefRenk.b;
            r = lightt.color.r - hedefRenk.r;
            g = lightt.color.g - hedefRenk.g;
            isDark = !isDark;


            for (int i = 0; i < 35; i++)
            {
                if (goDark)
                {
                    yield return new WaitForSeconds(time / 35);
                    lightt.color = new Color(lightt.color.r - r / 35, lightt.color.g - g / 35, lightt.color.b - b / 35);

                    if (i == 34)
                        lightt.color = hedefRenk;
                }
            }
        }
        else
        {
            b = lightt.color.b - 1;
            r = lightt.color.r - 1;
            g = lightt.color.g - 1;
            isDark = !isDark;
            
            for (int i = 0; i < 35; i++)
            {
                if (!goDark)
                {
                    yield return new WaitForSeconds(time / 35);
                    lightt.color = new Color(lightt.color.r - r / 35, lightt.color.g - g / 35, lightt.color.b - b / 35);

                    if (i == 34)
                        lightt.color = Color.white;
                }
            }     
        }
    }
}
