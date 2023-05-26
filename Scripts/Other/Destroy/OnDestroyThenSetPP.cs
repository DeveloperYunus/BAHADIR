using UnityEngine;

public class OnDestroyThenSetPP : MonoBehaviour
{
    [Multiline(2)]
    public string PPrefsName;

    private void OnDestroy()
    {
        PlayerPrefs.SetInt(PPrefsName, 1);
    }
}
