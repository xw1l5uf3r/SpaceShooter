using Unity.VisualScripting;
using UnityEngine;

public class Shotgun : StandartGun
{
//    public Shotgun(StandartGun obj)
//    {
//        this.bulletPrefab = obj.bulletPrefab;
//        this.myParent = obj.myParent;
//        this.oneEnemyScore = obj.oneEnemyScore;
//    }
    public override void Shoot()
    {
        float[] bulletPositions = { -1f, -0.75f, -0.5f, -0.25f, 0f, 0.25f, 0.5f, 0.75f, 1f };

        for(int i = 1; i < bulletPositions.Length - 1; i++)
        {
            GameObject diagonalBullet = Instantiate(bulletPrefab, myParent.firePoint.position, Quaternion.identity);
            diagonalBullet.GetComponent<Bullet>().speed *= 2f; 
            diagonalBullet.GetComponent<Bullet>().Shoot(Random.Range(bulletPositions[i - 1], bulletPositions[i]));
        }
    }
}
