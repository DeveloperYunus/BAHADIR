using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float summerT1, summerT2, windT, caveT, desertT, nightT;
    
    float windTimer, summerTimer, caveTimer, desertTimer, nightTimer;
    float soundForest, soundCave, soundTime, zaman;
    int oneBGMusic;
    float ppSoundAmount;

    public Sounds[] sounds;

    private void Awake()
    {
        foreach(Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.constantSound = s.volume;
        }
    }
    private void Start()
    {
        SetSoundPP();

        if (summerT2 == 0) oneBGMusic = 1;
        else oneBGMusic = 2;

        soundTime = 0.023f;                         //lerp fonksiyonu için
        if (IsPlayerInCave.isPlayerInCave)          //magaranýn ýcýndeyim
        {
            soundForest = 0;
            soundCave = 1;
        }           
        else //dýsardayým
        {
            soundForest = 1;
            soundCave = 0;
        }

        summerTimer = -1;
        windTimer = -1;
        caveTimer = -1;
        desertTimer = -1;

        BackGroundMusic();
    }
    private void Update()
    {
        if (Time.time >= zaman)//optimizasyon icin
        {
            zaman = Time.time + 0.5f;
            if (nightTimer >= 0f)
            {
                nightTimer -= Time.deltaTime + 0.5f;
            }
            else BackGroundMusic();

            if (desertTimer >= 0f)
            {
                desertTimer -= Time.deltaTime + 0.5f;
            }
            else BackGroundMusic();

            if (windTimer >= 0f)
            {
                windTimer -= Time.deltaTime + 0.5f;
            }
            else BackGroundMusic();

            if (summerTimer >= 0f)
            {
                summerTimer -= Time.deltaTime + 0.5f;
            }
            else BackGroundMusic();

            if (caveTimer >= 0f)
            {
                caveTimer -= Time.deltaTime + 0.5f;
            }
            else BackGroundMusic();


            if (IsPlayerInCave.isPlayerInCave) //magaranýn ýcýndeyim (soundForest = 0, soundCave = 1)
            {
                soundCave = Mathf.Lerp(soundCave, 1, soundTime);
                soundForest = Mathf.Lerp(soundForest, 0, soundTime);


                setSound("summer1", soundForest * 0.55f * ppSoundAmount);
                setSound("summer2", soundForest * 0.45f * ppSoundAmount);
                setSound("wind1", soundForest * 0.8f * ppSoundAmount);
                setSound("desertWind", soundForest * 0.6f * ppSoundAmount);
                setSound("nightBG", soundForest * 1f * ppSoundAmount);
                setSound("cave1", soundCave * 0.65f * ppSoundAmount);
            }
            else//dýsarýdayým (soundForest = 1, soundCave = 0)
            {
                soundCave = Mathf.Lerp(soundCave, 0, soundTime);
                soundForest = Mathf.Lerp(soundForest, 1, soundTime);

                setSound("summer1", soundForest * 0.55f * ppSoundAmount);
                setSound("summer2", soundForest * 0.45f * ppSoundAmount);
                setSound("wind1", soundForest * 0.8f * ppSoundAmount);
                setSound("desertWind", soundForest * 0.6f * ppSoundAmount);
                setSound("nightBG", soundForest * 1f * ppSoundAmount);
                setSound("cave1", soundCave * 0.65f * ppSoundAmount);
            }
        }
    }

    void BackGroundMusic()
    {
        int a = UnityEngine.Random.Range(0, oneBGMusic);

        if (desertT == 0 && nightT == 0)
        {
            if (a == 0 && summerTimer < 0f)
            {
                summerTimer = summerT1;
                playSound("summer1");
                setSound("summer1", soundForest * 0.55f * ppSoundAmount);
                stopSound("desertWind");
                stopSound("nightBG");
                stopSound("summer2");
            }
            else if (a == 1 && summerTimer < 0f)
            {
                summerTimer = summerT2;
                playSound("summer2");
                setSound("summer2", soundForest * 0.45f * ppSoundAmount);
                stopSound("desertWind");
                stopSound("nightBG");
                stopSound("summer1");
            }
        }
        else if (desertT != 0)
        {
            if (desertTimer < 0f)
            {
                desertTimer = desertT;
                playSound("desertWind");
                setSound("desertWind", soundForest * 0.8f);
                stopSound("summer1");
                stopSound("summer1");
                stopSound("nightBG");
            }
        }
        else if (nightT != 0)
        {
            if (nightTimer < 0f)
            {
                nightTimer = nightT;
                playSound("nightBG");
                setSound("nightBG", soundForest * 1f);
                stopSound("summer1");
                stopSound("summer2");
                stopSound("desertWind");
            }
        }


        if (windTimer < 0f)
        {
            windTimer = windT;
            playSound("wind1");
            setSound("wind1", soundForest * 0.8f * ppSoundAmount);
        }
        if (caveTimer < 0f )
        {
            caveTimer = caveT;
            playSound("cave1");
            setSound("cave1", soundCave * 0.65f);
        }
    }

    public void playSound(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
       
        setSound(name, s.constantSound * ppSoundAmount);
        s.source.Play();
    }
    public void stopSound(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Stop();
    }
    public void setSound(string name, float volumeAmout)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.volume = volumeAmout;
    }
    public void SetSoundPP()
    {
        ppSoundAmount = 0.1f * PlayerPrefs.GetInt("soundSliderAmount", 5);         //pp de tuttugumus ses bilgisini sürekli aramasýn diye deðiþlkene atýyoz

        Sounds s = Array.Find(sounds, sound => sound.name == "summer1");
        if (s == null) return;
        s.source.volume = s.constantSound * ppSoundAmount;
    }
}
