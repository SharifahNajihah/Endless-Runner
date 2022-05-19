using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the ground platform does not contain any power up, double up object 
/// enemy patrolling 
/// tagged kill box to the enemy
/// </summary>
public class GroundPlaneGenerator : MonoBehaviour
{
    [SerializeField] GameObject thePlatform;
    [SerializeField] Transform generationPoint;
    [SerializeField] float distanceBetween;

    private float platformWidth;

    public float distanceBetweenMin;
    public float distanceBetweenMax;

    private int platformSelector;
    private float[] platformWidths;

    [Header("Platform Items")]
    public ObjectPooler[] theObjectPools;

    private float minHeight;
    public Transform maxHeightpoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;

    [Header("Coin Generator")]
    private CoinGenerator theCoinGenerator;
    public float randomCoinThreshold;

    
    public float randomSpikeThreshold;
    public ObjectPooler spikePool;

    public float powerupHeight;
    public ObjectPooler powerupPool;
    public float powerupThreshold;

    void Start()
    {

        platformWidths = new float[theObjectPools.Length];

        for (int i = 0; i < theObjectPools.Length; i++)
        {
            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightpoint.position.y;

        theCoinGenerator = FindObjectOfType<CoinGenerator>();

    }

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
}
