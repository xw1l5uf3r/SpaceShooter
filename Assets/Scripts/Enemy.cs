using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    public int health = 5;

    public int collisionDamage = 5; // урон получаемый игроком при столкновении

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private float fireRate = 1f;

    private GameObject playerSpaceCraft;

    public int scoreValue = 25;

    void Start()
    {
        InvokeRepeating("Shoot", 0f, fireRate);
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
        playerSpaceCraft = GameObject.Find("PlayerSpacecraft");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();

            if (bullet != null)
                health -= bullet.damage;

            Destroy(other.gameObject);

            if (health <= 0)
            {
                playerSpaceCraft.GetComponent<PlayerController>().AddScore(scoreValue);
                Destroy(gameObject);
            }
        }
        else if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
                player.maxHealth -= collisionDamage;

            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
    void Shoot()
    {
        GameObject enemyBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        enemyBullet.GetComponent<Bullet>().Shoot(0f);
    }
}
