using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Vector3 spd = transform.position;
        //spd.z *= -speed;
        GetComponent<Rigidbody>().linearVelocity = new Vector3(0f, 0f, -speed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 50f * Time.deltaTime, 0f);
    }
}
