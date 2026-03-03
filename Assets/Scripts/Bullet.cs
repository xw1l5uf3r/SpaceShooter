using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1f;
    [SerializeField]
    private GameObject hitEffectPrefab;

    Vector2 direction;

    public int damage = 1;
    private Rigidbody rb;

    [SerializeField]
    private bool reverseZ = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (hitEffectPrefab != null && !reverseZ)
            {
                GameObject vfx = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
                Destroy(vfx, 2f);
            }
            
        }
        else if (other.CompareTag("Player") && reverseZ)
        {
            if (hitEffectPrefab != null)
            {
                GameObject vfx = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
                Destroy(vfx, 2f);
            }
            
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
    public void Shoot(float xPosition)
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = new Vector3(speed * xPosition, 0f, reverseZ ? -(speed) : speed);
    }
}
