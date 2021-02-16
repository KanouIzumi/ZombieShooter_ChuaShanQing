using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform_Controller : MonoBehaviour
{
    float speed = 4f;
    float zLimit = -1.04f;
    float zstart = -20.0f;
    bool AtZLimit = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < zLimit && AtZLimit)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed); //true
        }

        else if (transform.position.z > zstart && !AtZLimit)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * -speed); //false
        }

        else
        {
            AtZLimit = !AtZLimit;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = gameObject.transform;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        collision.gameObject.transform.parent = null;
    }

}
