using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;

    public float tiltAmount = 30f; // максимальный наклон
    public float tiltSpeed = 5f; // скорость наклона

    public float fireRate = 1.0f;
    public int maxHealth = 100;

    private float horizontal;
    private float vertical;

    private float nextFireTime = 0f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private float xMin = -30f, xMax = 30f;
    private float zMin = -50f, zMax = 30f;

    public int totalScore = 0;

    [SerializeField]
    private TextMeshProUGUI hpText;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private float timer = 0f;
    private int oneEnemyScore = 0;

    [SerializeField]
    private float[] bulletPositions = { -1f, -0.5f, 0.5f, 1f };


    void Start()
    {
        hpText.text = maxHealth.ToString();
    }

    public void AddScore(int score)
    {
        if (oneEnemyScore == 0) 
            oneEnemyScore = score;
        
        totalScore += score;
        scoreText.text = "Score: " + totalScore;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                maxHealth -= enemy.collisionDamage;
                enemy.collisionDamage = 0;
                if (maxHealth <= 0)
                {
                    GameData.totalScore = totalScore;
                    Lose();
                }
                Destroy(enemy);

                hpText.text = maxHealth.ToString();
            }
        }
        else if (other.CompareTag("EnemyBullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                maxHealth -= bullet.damage;
                if (maxHealth <= 0)
                {
                    GameData.totalScore = totalScore;
                    Lose();
                }
                hpText.text = maxHealth.ToString(); 
            }
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Heart"))
        {
            Heart heart = other.GetComponent<Heart>();
            if (heart != null) 
            {
                maxHealth += heart.hpPerHeart;
                hpText.text = maxHealth.ToString();
            }
            Destroy(other.gameObject);
        }
    }
    private void Lose()
    {
        SceneManager.LoadScene(2);
    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            Shoot();
            timer = 0f;
        }
    }
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        // наклон
        float tiltZ = -horizontal * tiltAmount;

        Quaternion targetRotation = Quaternion.Euler(0, 0, tiltZ);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * tiltSpeed
        );

        float clampedX = Mathf.Clamp(transform.position.x, xMin, xMax);
        float clampedZ = Mathf.Clamp(transform.position.z, zMin, zMax);

        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }
    void Shoot()
    {
        GameObject straightBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        straightBullet.GetComponent<Bullet>().Shoot(0f);
        if (oneEnemyScore != 0 && totalScore > oneEnemyScore * 10) // 2 дополнительных снаряда при успешном уничтожении 10 противников
        {
            foreach (var bulletPosition in bulletPositions)
            {
                GameObject diagonalBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                diagonalBullet.GetComponent<Bullet>().Shoot(bulletPosition);
            }
        }
    }
}
