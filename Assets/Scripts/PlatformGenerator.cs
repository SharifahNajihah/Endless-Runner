﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{

    public GameObject thePlatform;
    public Transform generationPoint;
    public float distanceBetween;

    private float platformWidth;

    public float distanceBetweenMin;
    public float distanceBetweenMax;

    //public GameObject[] thePlatforms;
    private int platformSelector;
    private float[] platformWidths;

    public ObjectPooler[] theObjectPools;

    private float minHeight;
    public Transform maxHeightpoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;

    private CoinGenerator theCoinGenerator;
    public float randomCoinThreshold;

    public float randomSpikeThreshold;
    public ObjectPooler spikePool;

    public float powerupHeight;
    public ObjectPooler powerupPool;
    public float powerupThreshold;

    //[Header("Enemy Stuff")]
    //public float scrollSpeed = 5.0f;
    //public GameObject[] challenges;
    //public float frequency = 0.5f;
    //float counter = 0.0f;
    //public Transform challlengesSpawnPoint;
    //bool isGameOver = false;
    //public DeathMenu theDeathScreen;

    // Start is called before the first frame update
    void Start()
    {

        //platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;

        platformWidths = new float[theObjectPools.Length];

        for (int i = 0; i < theObjectPools.Length; i++)
        {
            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightpoint.position.y;

        theCoinGenerator = FindObjectOfType<CoinGenerator>();

        //GenerateRandomChallenge();
    }

    /// <summary>
    /// this is update per frame
    /// might use another method since it is not a suitable method to use 
    /// </summary>
    void Update()
    {

        if (transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            platformSelector = Random.Range(0, theObjectPools.Length);

            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

            if (heightChange > maxHeight)
            {
                heightChange = maxHeight;
            }
            else if (heightChange < minHeight)
            {
                heightChange = minHeight;
            }

            if (Random.Range(0f, 100f) < powerupThreshold)
            {
                GameObject newPowerup = powerupPool.GetPooledObject();

                newPowerup.transform.position = transform.position + new Vector3(distanceBetween / 2f, Random.Range(powerupHeight / 2f, powerupHeight), 0f);

                newPowerup.SetActive(true);

            }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, heightChange, transform.position.z);



            //Instantiate(/* thePlatform */ thePlatforms[platformSelector] , transform.position, transform.rotation);

            GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);


            if (Random.Range(0f, 100f) < randomCoinThreshold)
            {
                theCoinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
            }

            if (Random.Range(0f, 100f) < randomSpikeThreshold)
            {
                GameObject newSpike = spikePool.GetPooledObject();

                float spikeXPosition = Random.Range(platformWidths[platformSelector] / 2 - 1f, -platformWidths[platformSelector] / 2 + 1f);

                Vector3 spikePosition = new Vector3(spikeXPosition, 0.5f, 0f);

                newSpike.transform.position = transform.position + spikePosition;
                newSpike.transform.rotation = transform.rotation;
                newSpike.SetActive(true);
            }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);

        }

        //if (isGameOver) return;

        ////GenerateObjects
        //if (counter <= 0.0f)
        //{
        //    GenerateRandomChallenge();
        //}
        //else
        //{
        //    counter -= Time.deltaTime * frequency;
        //}

        //Scrolling
        GameObject currentChild;
        for (int i = 0; i < transform.childCount; i++)
        {
            currentChild = transform.GetChild(i).gameObject;
            //ScrollChallenge(currentChild);
            if (currentChild.transform.position.x <= -15.0f)
            {
                Destroy(currentChild);
            }
        }

    }
    //void ScrollChallenge(GameObject currentChallenge)
    //{
    //    currentChallenge.transform.position -= Vector3.right * (scrollSpeed * Time.deltaTime);
    //}

    //void GenerateRandomChallenge()
    //{
    //    GameObject newChallenge = Instantiate(challenges[Random.Range(0, challenges.Length)], challlengesSpawnPoint.position, Quaternion.identity) as GameObject;
    //    newChallenge.transform.parent = transform;
    //    counter = 1.0f;
    //}

    //public void GameOver()
    //{
    //    theDeathScreen.gameObject.SetActive(true);
    //    transform.GetComponent<GameManager>().Reset();
    //}

}
