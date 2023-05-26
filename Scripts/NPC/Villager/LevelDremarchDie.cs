using UnityEngine;

public class LevelDremarchDie : MonoBehaviour
{
    AudioManager a;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bullet"))
        {
            PlayerPrefs.SetString("L6BadDremarchDead","yes");
            a = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            a.playSound("manScream2");
            GetComponent<Animator>().Play("Dead");
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
