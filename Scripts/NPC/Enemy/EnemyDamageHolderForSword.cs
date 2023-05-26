using System.Collections;
using UnityEngine;

public class EnemyDamageHolderForSword : MonoBehaviour
{
    public EnemyOther EO;
    [HideInInspector]
    public float damage;
    public bool canGiveDamage;

    void Start()
    {
        canGiveDamage = true;
        damage = EO.damage;
    }

    public void CanGiveDamageTimer()
    {
        StartCoroutine(CanGiveDamage());
    }
    public IEnumerator CanGiveDamage()
    {
        yield return new WaitForSeconds(EO.attackSpeed);
        canGiveDamage = true;
    }
}
