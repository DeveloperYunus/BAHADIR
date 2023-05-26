using UnityEngine;

public class IsPlayerInCave : MonoBehaviour
{
    public static bool isPlayerInCave;
    public bool PlayerInCave;
    public bool velocityRight, velocityDown;

    private void Start()
    {
        isPlayerInCave = PlayerInCave;    
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && velocityRight && other.GetComponent<Rigidbody2D>().velocity.x > 0)         //player magaraya girdi     
            isPlayerInCave = true;
        else if (other.CompareTag("Player") && velocityRight && other.GetComponent<Rigidbody2D>().velocity.x < 0)    //player magaradan cýktý          
            isPlayerInCave = false;


        if (other.CompareTag("Player") && !velocityRight && other.GetComponent<Rigidbody2D>().velocity.x < 0)        //player magaraya girdi         
            isPlayerInCave = true;
        else if (other.CompareTag("Player") && !velocityRight && other.GetComponent<Rigidbody2D>().velocity.x > 0)   //player magaradan cýktý       
            isPlayerInCave = false;


        if (other.CompareTag("Player") && velocityDown && other.GetComponent<Rigidbody2D>().velocity.y < 0)    //player magaraya girdi           
            isPlayerInCave = true;
    }
}
