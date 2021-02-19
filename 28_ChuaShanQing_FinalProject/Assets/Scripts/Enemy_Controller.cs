using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy_Controller : MonoBehaviour
{
    public static Enemy_Controller instance;

    float enemyHP = 1;
    public GameObject scoreText;
    public static int score;
    public float dist;
    public Animator EnemyAnim;

    public GameObject coinPrefab;
    public GameObject coinSpawn;

    private AudioSource audioSource;
    private NavMeshAgent navMeshAgent;
    private GameObject character;
    private BoxCollider boxCollider;

    bool coinDrop;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        EnemyAnim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        scoreText = GameObject.Find("Score");
        character = GameObject.FindGameObjectWithTag("Player");
        coinSpawn = GameObject.FindGameObjectWithTag("CoinSpawn");
        coinPrefab = GameObject.FindGameObjectWithTag("Coin");

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(character.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(character.transform.position);

        dist = Vector3.Distance(character.transform.position, transform.position);
        if (dist <= 10)
        {

            navMeshAgent.SetDestination(character.transform.position);
            EnemyAnim.SetBool("NearPlayer", true);
        }

        else if (dist >= 10)
        {
            navMeshAgent.SetDestination(this.transform.position);
            EnemyAnim.SetBool("NearPlayer", false);

        }

        if (enemyHP == 0)
        {
            navMeshAgent.speed = 0f;
            boxCollider.enabled = false;
            coinDropper();
        }

        scoreText.GetComponent<Text>().text = "Score: " + score.ToString();
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            score += 1;
            enemyHP -= 1;
            audioSource.Play();
            Destroy(collision.gameObject);
            EnemyAnim.SetTrigger("ZombieDead");
            Destroy(gameObject, 3.5f);
        }
    }

    private void coinDropper()
    {
        if(enemyHP <=0 && coinDrop ==false)
        {
            Instantiate(coinPrefab, coinSpawn.transform.position, Quaternion.identity);
            coinDrop = true;
        }
    }
}
