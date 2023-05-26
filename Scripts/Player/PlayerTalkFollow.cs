using UnityEngine;

public class PlayerTalkFollow : MonoBehaviour
{   
    public Transform target;    
    public float textSpeed;

    void FixedUpdate()
    {
        if (target)
            transform.position = Vector3.Slerp(transform.position, new Vector3(target.position.x, target.position.y, -1f), textSpeed);
    }   
}
