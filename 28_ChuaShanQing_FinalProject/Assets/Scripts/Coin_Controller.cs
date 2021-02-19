using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin_Controller : MonoBehaviour
{
    float SpinSpeed;
    // Start is called before the first frame update
    void Start()
    {
        SpinSpeed = Random.Range(60.0f, 90.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(SpinSpeed * Time.deltaTime, 0, 0));
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Got Coin");
            Destroy(gameObject);
        }
    }
}
