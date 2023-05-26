using UnityEngine;

public class StopVillagersWalk : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.CompareTag("UnEffectVillager") && other.GetComponent<Rigidbody2D>())
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            other.GetComponent<Animator>().SetFloat("speedx", 0);
        }
    }
}
