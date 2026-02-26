using System;
using UnityEngine;

public class StandartGun : MonoBehaviour
{
    public PlayerController myParent;

    public GameObject bulletPrefab;

    public float fireRate = 1.0f;

    private float timer = 0f;

    [NonSerialized]
    public int oneEnemyScore = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        myParent = GetComponentInParent<PlayerController>();
    }
    public virtual void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            Shoot();
            timer = 0f;
        }
    }
    public virtual void Shoot()
    {
        GameObject straightBullet = Instantiate(bulletPrefab, myParent.firePoint.position, myParent.firePoint.rotation);
        straightBullet.GetComponent<Bullet>().Shoot(0f);
        //if (oneEnemyScore != 0 && myParent.totalScore > oneEnemyScore * 10) // 2 дополнительных снаряда при успешном уничтожении 10 противников
        //{
        //    foreach (var bulletPosition in bulletPositions)
        //    {
        //        GameObject diagonalBullet = Instantiate(bulletPrefab, myParent.firePoint.position, Quaternion.identity);
        //        diagonalBullet.GetComponent<Bullet>().Shoot(bulletPosition);
        //    }
        //}
    }
}
