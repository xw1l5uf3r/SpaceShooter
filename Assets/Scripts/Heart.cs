using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    public int hpPerHeart = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * -(speed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, 50f * Time.deltaTime);
    }
}
