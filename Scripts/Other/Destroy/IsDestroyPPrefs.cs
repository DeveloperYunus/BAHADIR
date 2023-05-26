using UnityEngine;

public class IsDestroyPPrefs : MonoBehaviour
{
    public string pPrefsName;
    public string pPrefsString;

    public void Die()
    {
        print("yokedildi");
        PlayerPrefs.SetString(pPrefsName, pPrefsString);
    }
}
