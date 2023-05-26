using UnityEngine;

public class AfterCreditsSliderStop : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Credits")
        {
            collision.GetComponent<AfterCredit>().up = false;
            collision.GetComponent<AfterCredit>().CloseShow(5f);
        }
    }
}
