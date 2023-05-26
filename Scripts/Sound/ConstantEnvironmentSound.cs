using UnityEngine;

public class ConstantEnvironmentSound : MonoBehaviour
{
    public float[] volume;

    private void Start()
    {
        int aa = GetComponents<AudioSource>().Length;
        for (int i = 0; i < aa; i++)
        {
            if (GetComponents<AudioSource>()[i]) 
            {
                GetComponents<AudioSource>()[i].volume = volume[i] * PlayerPrefs.GetInt("soundSliderAmount") * 0.1f;
            }
        }
    }
}
