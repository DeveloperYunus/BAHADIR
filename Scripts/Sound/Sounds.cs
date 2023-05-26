using UnityEngine;

[System.Serializable]
public class Sounds 
{
    [HideInInspector]
    public AudioSource source;
    public AudioClip clip;

    public string name;
    [Range(0f,1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;             //oynatma hýzý
    [HideInInspector] public float constantSound;     //ses ayarý yapýnca esas ses mýktarýný tutacak deðiþken
}
