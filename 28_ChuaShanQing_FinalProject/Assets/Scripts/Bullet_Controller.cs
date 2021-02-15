using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Controller : MonoBehaviour
{
    public float bulletSpeed;

    float zLimit = 50f;
    float xLimit = 50f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 7);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;

        if (transform.position.x > xLimit)
        {
            Destroy(gameObject);
        }

        if (transform.position.x < -xLimit)
        {
            Destroy(gameObject);
        }

        if (transform.position.z > zLimit)
        {
            Destroy(gameObject);
        }

        if (transform.position.z < -zLimit)
        {
            Destroy(gameObject);
        }


    }
}
