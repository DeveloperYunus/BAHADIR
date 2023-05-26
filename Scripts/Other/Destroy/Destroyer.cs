using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour
{
    public bool onlyDestroy;                //sadece sure sonunda �ldurulmek istene bir obje ise bunu true yap
    public bool iseEnemyBullet;
    public float lifeTimer;

    void Start()
    {
        if (iseEnemyBullet)
            Invoke(nameof(DestroyTimer), lifeTimer);
        else if (!onlyDestroy)//bunu sonradan ekledik ve b�t�n destructible perfablar�na s�k�nt� c�kard� o yuzden b�yle ! koyduk ba��na
            Destroy(gameObject, lifeTimer);
        else
            StartCoroutine(Destroyy());
    }

    void DestroyTimer()
    {
        Instantiate(GetComponent<EnemyBullet>().explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator Destroyy()
    {
        yield return new WaitForSeconds(lifeTimer);
        GetComponent<Bullet>().Triggered();
        GetComponent<Bullet>().PlayerBulletProcedur(gameObject, false);
    }
}
