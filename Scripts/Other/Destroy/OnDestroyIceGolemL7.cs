using UnityEngine;

public class OnDestroyIceGolemL7 : MonoBehaviour
{
    public static int howManyIceGolemDie;

    void Start()
    {
        howManyIceGolemDie = 0;
    }

    private void OnDestroy()
    {
        howManyIceGolemDie++;
    }
}
