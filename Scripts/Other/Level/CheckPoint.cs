using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Transform rebornTransform;
    public int number;
    public static int staticNumber;

    private void Start()
    {
        staticNumber = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && number > staticNumber) 
        {
            rebornTransform.position = transform.position;
            staticNumber = number;
        }
    }

}
