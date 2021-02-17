using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    int bulletAmount = 10;
    int maxBullet = 10;
    public float walkSpeed;
    public float rotateSpeed;
    public float health;
    public float damageRate;

    bool IsAlive = true;

    //Ammo
    float reloadTime = 2.0f;
    bool canShoot = true;
    bool isRealoding;

    private AudioSource audioSource;
    public AudioClip[] AudioClipBGMArr;

    public Animator playerAnim;
    public Rigidbody playerRb;

    public GameObject bulletPrefab;
    public GameObject bulletSpawn;
    public GameObject bulletText;
    public GameObject healthPointText;


    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        healthPointText.GetComponent<Text>().text = "Health: " + health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAlive == true && health > 0)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) //go forward
            {
                transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
                //transform.rotation = Quaternion.Euler(0, 0 + rotateSpeed, 0);
                playerAnim.SetFloat("RunSpeed", 10);
            }
            
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) // go backward
            {
                transform.Translate(Vector3.back * Time.deltaTime * walkSpeed);
                playerAnim.SetFloat("RunSpeed", 10);
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // go leftside
            {
                transform.Translate(Vector3.left * Time.deltaTime * walkSpeed);
                //transform.rotation = Quaternion.Euler(0, -90 + rotateSpeed, 0);
                transform.Rotate(new Vector3(0, Time.deltaTime * -rotateSpeed, 0));
                playerAnim.SetFloat("RunSpeed", 10);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // go rightside
            {
                transform.Translate(Vector3.right * Time.deltaTime * walkSpeed);
                //transform.rotation = Quaternion.Euler(0, 90 + rotateSpeed, 0);
                transform.Rotate(new Vector3(0, Time.deltaTime * rotateSpeed, 0));
                playerAnim.SetFloat("RunSpeed", 10);
            }

            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) // stop walking
            {
                playerAnim.SetFloat("RunSpeed", 0);
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow)) // stop walking
            {
                playerAnim.SetFloat("RunSpeed", 0);
            }

            if (Input.GetMouseButtonDown(0) && bulletAmount>0 && canShoot == true) //Attack
            {
                bulletAmount--;
                bulletText.GetComponent<Text>().text = "Bullet Left: " + bulletAmount.ToString();
                audioSource.PlayOneShot(AudioClipBGMArr[0]);
                audioSource.PlayOneShot(AudioClipBGMArr[1]);
                Instantiate(bulletPrefab, bulletSpawn.transform.position, transform.rotation);
                playerAnim.SetTrigger("ShootTrigger");
            }

            if (Input.GetKeyDown(KeyCode.R) && isRealoding == false)
            {
                StartCoroutine(Reload());
                audioSource.PlayOneShot(AudioClipBGMArr[2]);
                return;
            }
           
        }

        else
        {
            IsAlive = false;
        }

    }

    IEnumerator Reload()
    {
        isRealoding = true;

        yield return new WaitForSeconds(reloadTime);
        bulletAmount = maxBullet;
        bulletText.GetComponent<Text>().text = "Bullet Left: " + bulletAmount.ToString();
        isRealoding = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("Hello");
        }

        if (collision.gameObject.CompareTag("Zombie") && IsAlive == true)
        {
            health -=1;
            healthPointText.GetComponent<Text>().text = "Health: " + health.ToString();

            if (health == 0)
            {
                healthPointText.GetComponent<Text>().text = "Health: 0" + health.ToString();
                IsAlive = false;
                playerAnim.SetTrigger("DeathTrigger");

                SceneManager.LoadScene("You Lose");
            }
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("HPRe"))
        {
            Debug.Log("Healing");
            if (health < 10)
            {
                health++;
                healthPointText.GetComponent<Text>().text = "Health: " + health.ToString();
            }
        }
    }


}
