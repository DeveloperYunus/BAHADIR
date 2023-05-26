using System.Collections;
using UnityEngine;

public class VillagerAttack : MonoBehaviour
{
    public static bool playerIsNear = false;
    Animator a;

    private void Start()
    {
        a = GetComponent<Animator>();
        InvokeRepeating("Aaa", 0, 5);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }

    void Aaa()
    {
        if(playerIsNear)
            StartCoroutine(VillagerAttackk());
    }

    IEnumerator VillagerAttackk()
    {
        float aa = Random.Range(0.3f, 2.3f);
        yield return new WaitForSeconds(aa);
        PlayerController.damageType = 2;
        GetComponent<ShootWaepon>().ShootFireBallForL6();
        a.Play("manAttack");
    }
}
