using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy_Controller : MonoBehaviour
{
    public static Enemy_Controller instance;

    public GameObject scoreText;
    public static int score;
    public float dist;
    public Animator EnemyAnim;

    private NavMeshAgent navMeshAgent;
    private GameObject character;


    // Start is called before the first frame update
    void Start()
    {
        EnemyAnim = GetComponent<Animator>();

        scoreText = GameObject.Find("Score");
        character = GameObject.FindGameObjectWithTag("Player");

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

        scoreText.GetComponent<Text>().text = "Score: " + score.ToString();
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            score += 1;
            EnemyAnim.SetTrigger("ZombieDead");

            Destroy(collision.gameObject);
            Debug.Log("Hit");
            navMeshAgent.speed = 0f;
            Destroy(gameObject, 2);
        }
    }
}
