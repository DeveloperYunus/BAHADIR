using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target;
    public float cameraSpeed;


    private void FixedUpdate()
    {
        if (target != null)
        {
            if (CameraStopFollow.canFollowStatic)
            {
                if (target.transform.localScale.x < 0)
                    transform.position = Vector3.Slerp(transform.position, new Vector3(target.position.x + 1f, target.position.y + 2f, -10f), cameraSpeed);
                else
                    transform.position = Vector3.Slerp(transform.position, new Vector3(target.position.x - 1f, target.position.y + 2f, -10f), cameraSpeed);
            }
            else
            {
                if (target.transform.localScale.x < 0)
                    transform.position = Vector3.Slerp(transform.position, new Vector3(CameraStopFollow.gizmoS.transform.position.x + 0.5f, target.position.y + 2f,
                                                                                   -10f), cameraSpeed);
                else
                    transform.position = Vector3.Slerp(transform.position, new Vector3(CameraStopFollow.gizmoS.transform.position.x - 0.5f, target.position.y + 2f,
                                                                                   -10f), cameraSpeed);
            }
        }
    }
}
