using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Controller : MonoBehaviour
{
    private AudioSource audioSource;
    private float volumeChangeSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            audioSource.Stop();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            audioSource.Play();
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            audioSource.volume += Time.deltaTime * volumeChangeSpeed;
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            audioSource.volume -= Time.deltaTime * volumeChangeSpeed;
        }
    }
}

