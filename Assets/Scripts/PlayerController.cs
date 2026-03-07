using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;

    public float tiltAmount = 30f; // максимальный наклон
    public float tiltSpeed = 5f;   // скорость наклона

    public int maxHealth = 100;

    private float horizontal;
    private float vertical;

    private float xMin = -30f, xMax = 30f;
    private float zMin = -50f, zMax = 30f;

    [NonSerialized]
    public int totalScore = 0;

    [NonSerialized]
    public bool isShielded = false;

    [SerializeField]
    private TextMeshProUGUI hpText;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private GameObject standartGunPrefab;
    [SerializeField]
    private GameObject shotgunPrefab;

    [SerializeField]
    private RawImage shieldIcon;

    public Transform firePoint;

    [NonSerialized]
    public StandartGun currentGun;

    void Start()
    {
        hpText.text = maxHealth.ToString();
        currentGun = Instantiate(standartGunPrefab, firePoint.position, Quaternion.identity, transform).GetComponent<StandartGun>();
        shieldIcon.enabled = false;
    }

    public void AddScore(int score)
    {
        if (currentGun.oneEnemyScore == 0)
            currentGun.oneEnemyScore = score;
        
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
                GetDamage(enemy.collisionDamage);
                Destroy(enemy);
            }
        }
        else if (other.CompareTag("EnemyBullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                GetDamage(bullet.damage);
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
        else if (other.CompareTag("Shotgun"))
        {
            if(currentGun.GetType() != typeof(Shotgun))
            {
                Destroy(currentGun);
                currentGun = Instantiate(shotgunPrefab, firePoint.position, Quaternion.identity, transform).GetComponent<Shotgun>();
                //currentGun.myParent = GetComponent<PlayerController>();
            }
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Shield"))
        {
            if(!isShielded)
            {
                isShielded = true;
                shieldIcon.enabled = true;
            }
            Destroy(other.gameObject);
        }
    }
    private void GetDamage(int damage)
    {
        if (isShielded)
        {
            isShielded = false;
            shieldIcon.enabled = false;
            return;
        }
        maxHealth -= damage;
        if (maxHealth <= 0)
        {
            GameData.totalScore = totalScore;
            Lose();
        }
        hpText.text = maxHealth.ToString();
    }
    private void Lose()
    {
        SceneManager.LoadScene(2);
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
}