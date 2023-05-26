using UnityEngine;

public class CameraStopFollow : MonoBehaviour
{
    public GameObject gizmo;
    public static GameObject gizmoS;
    public static bool canFollowStatic;

    [SerializeField]
    bool canFollow = true;

    private void Start()
    {
        gizmoS = gizmo;
        canFollowStatic = canFollow;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.transform.localScale.x > 0)
                gizmoS.transform.position = new Vector3(other.transform.position.x - 0.45f, other.transform.position.y, other.transform.position.z);
            else
                gizmoS.transform.position = new Vector3(other.transform.position.x + 0.45f, other.transform.position.y, other.transform.position.z);

            canFollow = false;
            gizmoS = gizmo;
            canFollowStatic = canFollow;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canFollow = true;
            gizmoS = gizmo;
            canFollowStatic = canFollow;
        }
    }
}
