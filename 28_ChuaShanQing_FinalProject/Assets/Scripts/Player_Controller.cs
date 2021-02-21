using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    //This is for the bullet count
    int bulletAmount = 10;
    int maxBullet = 10;
    public GameObject bulletPrefab;
    public GameObject bulletSpawn;
    public GameObject bulletText;

    //This is for walking 
    public float walkSpeed;
    public float rotateSpeed;
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

    //This is the life points of the player
    public float health;
    public GameObject healthPointText;

    //This is for the coin
    public GameObject coinText;
    public int coinCount;

    public float waitTime;


    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        healthPointText.GetComponent<Text>().text = "Health: " + health.ToString();
        coinText.GetComponent<Text>().text = "Coin: " + coinCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAlive == true && health > 0)
        {
            Movement();

            if (Input.GetKeyDown(KeyCode.Space) && bulletAmount>0 && canShoot == true) //Shoot
            {
                bulletAmount--;
                bulletText.GetComponent<Text>().text = "Bullet Left: " + bulletAmount.ToString();
                audioSource.PlayOneShot(AudioClipBGMArr[0]);
                audioSource.PlayOneShot(AudioClipBGMArr[1]);
                Instantiate(bulletPrefab, bulletSpawn.transform.position, transform.rotation);
                playerAnim.SetTrigger("ShootTrigger");
            }

            if (Input.GetKeyDown(KeyCode.R) && isRealoding == false) //Reloading
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

    IEnumerator Reload() //This is for the realoading of the gun
    {
        isRealoding = true;

        yield return new WaitForSeconds(reloadTime);
        bulletAmount = maxBullet;
        bulletText.GetComponent<Text>().text = "Bullet Left: " + bulletAmount.ToString();
        isRealoding = false;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Zombie") && IsAlive == true) //Getting hurt by the enemies
        {
            health -=1;
            healthPointText.GetComponent<Text>().text = "Health: " + health.ToString();
            audioSource.PlayOneShot(AudioClipBGMArr[4]);

            if (health == 0)
            {
                audioSource.PlayOneShot(AudioClipBGMArr[5]);
                healthPointText.GetComponent<Text>().text = "Health: " + health.ToString();
                print("You died");
                playerAnim.SetTrigger("DeathTrigger");
                StartCoroutine(WaitToChangeScene(waitTime));
            }
        }


        if (collision.gameObject.CompareTag("Coin"))
        {
            print("Got Coin");
            coinCount++;
            coinText.GetComponent<Text>().text = "Coin: " + coinCount.ToString();
            audioSource.PlayOneShot(AudioClipBGMArr[7]);
            Destroy(collision.gameObject);
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("HPRe")) //This is when player step into the healing area
        {
            Debug.Log("Healing");
            if (health < 10)
            {
                health++;
                audioSource.PlayOneShot(AudioClipBGMArr[3]);
                healthPointText.GetComponent<Text>().text = "Health: " + health.ToString();
            }
        }
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) //go forward
        {
            transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
            //transform.rotation = Quaternion.Euler(0, 0 + rotateSpeed, 0);
            playerAnim.SetFloat("RunSpeed", 10);

            //For the walking sound
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(AudioClipBGMArr[6]);
            }
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) // go backward
        {
            transform.Translate(Vector3.back * Time.deltaTime * walkSpeed);
            playerAnim.SetFloat("RunSpeed", 10);
            //For the walking sound
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(AudioClipBGMArr[6]);
            }
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // go leftside
        {
            transform.Translate(Vector3.left * Time.deltaTime * walkSpeed);
            //transform.rotation = Quaternion.Euler(0, -90 + rotateSpeed, 0);
            transform.Rotate(new Vector3(0, Time.deltaTime * -rotateSpeed, 0));
            playerAnim.SetFloat("RunSpeed", 10);
            //For the walking sound
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(AudioClipBGMArr[6]);
            }
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // go rightside
        {
            transform.Translate(Vector3.right * Time.deltaTime * walkSpeed);
            //transform.rotation = Quaternion.Euler(0, 90 + rotateSpeed, 0);
            transform.Rotate(new Vector3(0, Time.deltaTime * rotateSpeed, 0));
            playerAnim.SetFloat("RunSpeed", 10);
            //For the walking sound
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(AudioClipBGMArr[6]);
            }
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) // stop walking
        {
            playerAnim.SetFloat("RunSpeed", 0);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow)) // stop walking
        {
            playerAnim.SetFloat("RunSpeed", 0);
        }
    }

    //This is to wait for awhile  to change scene
    private IEnumerator WaitToChangeScene(float waitTime)
    {
        while (true)
        {
            IsAlive = false;

            yield return new WaitForSeconds(waitTime);
            SceneManager.LoadScene("You Lose");
        }
    }


    private void BuyBullets()
    {

    }

}
