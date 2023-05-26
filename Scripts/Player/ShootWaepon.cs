using UnityEngine;

public class ShootWaepon : MonoBehaviour
{
    public Transform muzzle;
    public GameObject fireBallPrefab, iceBallPrefab, flyDamageText;

    [Header("For Enemy")]
    public float enemyFireSpeed;
    public float enemyIceSpeed;
    [HideInInspector]public Vector2 directionn;
    float rotationZ;

    public void ShootFireBallForL6()
    {
        GameObject a = Instantiate(fireBallPrefab, muzzle.position, muzzle.rotation);
        a.GetComponent<Destroyer>().lifeTimer = 3;
    }

    public void ShootFireBall()
    {
        Instantiate(fireBallPrefab, muzzle.position, muzzle.rotation);
    }
    public void ShootIceBall()
    {
        Instantiate(iceBallPrefab, muzzle.position, muzzle.rotation);
    }

    public void ThrowFireForEnemy()
    {
        EnemyThrowCalculate();
        var fireBall = Instantiate(fireBallPrefab, muzzle.position, Quaternion.Euler(0f, 0f, rotationZ));
        fireBall.GetComponent<EnemyBullet>().damage = GetComponent<EnemyOther>().damage;
        fireBall.GetComponent<EnemyBullet>().damageType = GetComponent<EnemyOther>().damageTypeS;
        fireBall.GetComponent<EnemyBullet>().direction = directionn;
        fireBall.GetComponent<Rigidbody2D>().velocity = directionn * enemyFireSpeed;
    }
    public void ThrowIceForEnemy()
    {
        EnemyThrowCalculate();
        var iceBall = Instantiate(iceBallPrefab, muzzle.position, Quaternion.Euler(0f, 0f, rotationZ));
        iceBall.GetComponent<EnemyBullet>().damage = GetComponent<EnemyOther>().damage;
        iceBall.GetComponent<EnemyBullet>().damageType = GetComponent<EnemyOther>().damageTypeS;
        iceBall.GetComponent<EnemyBullet>().direction = directionn;
        iceBall.GetComponent<Rigidbody2D>().velocity = directionn * enemyIceSpeed;
    }

    void EnemyThrowCalculate()
    {
        Vector2 difference = GetComponent<EnemyRangedAII>().target.position + new Vector3(0f, 0.5f, 0f) - muzzle.position;
        rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        float distance = difference.magnitude;
        Vector2 direction = difference / distance;
        direction.Normalize();
        directionn = direction;
    }
}