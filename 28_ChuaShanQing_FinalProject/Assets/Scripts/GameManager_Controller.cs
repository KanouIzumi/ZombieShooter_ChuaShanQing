using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_Controller : MonoBehaviour
{
    public static GameManager_Controller instance;

    public GameObject EnermyPrefab;
    public GameObject coinPrefab;
    public int numberOfSpawn;
    public float spwanInterval;

    public static GameObject coinText;
    public static int coinCount = 0;

    float winScore = 30;

    public Transform[] spwanPointArr;

    //Timer
    int timeCountInt;
    float timerCount = 60;
    bool isStartCount;
    public GameObject timerTextGO;

    // Start is called before the first frame update
    void Start()
    {
        isStartCount = true;

        if (instance == null)
        {
            instance = this;
        }


        //This code is for the random spawn
        for (int i = 0; i < numberOfSpawn; i++)
        {
            //Vector3 randomPos = new Vector3(Random.Range(-50, 50), 0.5f, Random.Range(-50, 50));
            int randomIndex = Random.Range(0, spwanPointArr.Length);
            Vector3 randomPos = spwanPointArr[randomIndex].position;

            // A chance of 50% //
            //if (Random.Range(0, 2) < 1)
            //{
            //    Instantiate(EnermyPrefab, randomPos, Quaternion.identity);
            //}

            StartCoroutine(WaitAndSpawn(spwanInterval));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartCount == true)
        {
            timerCountDown();
        }

        //Win conditions
        if (Enemy_Controller.score == winScore  || timeCountInt==0)
        {
            Debug.Log("You Win");
        }

    }

    private void timerCountDown()
    {
        if (timerCount > 0)
        {
            timerCount -= Time.deltaTime;
            timeCountInt = Mathf.RoundToInt(timerCount);
            timerTextGO.GetComponent<Text>().text = "Timer: " + timeCountInt;
        }

        else if (timerCount < 1)
        {
            timerCount = 60.0f;
            isStartCount = false;
        }
    }

    private IEnumerator WaitAndSpawn(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            int randomIndex = Random.Range(0, spwanPointArr.Length);
            Vector3 randomPos = spwanPointArr[randomIndex].position;
            Instantiate(EnermyPrefab, randomPos, Quaternion.identity);
        }
    }
}
