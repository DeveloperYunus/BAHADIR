using UnityEngine;

public class PSSimulatedSpeed : MonoBehaviour
{
    public float speeed;
    ParticleSystem ps;

    void Start()
    {
        Speed();
    }

    void Speed()
    {
        ps = GetComponent<ParticleSystem>();
        var main = ps.main;
        main.simulationSpeed = speeed;
    }
}
