using UnityEngine;

public class Shotgun : StandartGun
{
    protected float[] bulletPositions = { -1f, -0.5f, 0f, 0.5f, 1f };

    public Shotgun(StandartGun obj)
    {
        this.bulletPrefab = obj.bulletPrefab;
        this.myParent = obj.myParent;
        this.oneEnemyScore = obj.oneEnemyScore;
    }
    // Update is called once per frame
    public override void Shoot()
    {
        foreach (var bulletPosition in bulletPositions)
        {
            GameObject diagonalBullet = Instantiate(bulletPrefab, myParent.firePoint.position, Quaternion.identity);
            diagonalBullet.GetComponent<Bullet>().Shoot(bulletPosition);
        }
    }
}
