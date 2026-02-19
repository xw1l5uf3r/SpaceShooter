using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // доп. условия, для того чтобы понять каким снарядам нужно двигаться по диагонали
        rb.linearVelocity = new Vector3(transform.rotation.x == 0f ? 0f : (transform.rotation.x > 0 ? speed / 2f : -(speed / 2f)), 0f, reverseZ ? -(speed) : speed);
    }
}
