using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltItemSpawner : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> spawnableItems;
    [SerializeField]
    public float spawningCooldownTime;

    private float nextSpawnTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nextSpawnTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (spawningCooldownTime < nextSpawnTime)
        {
            nextSpawnTime = 0;
            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        int randomItemToSpawn = Random.Range(0, (spawnableItems.Count - 1));

        Instantiate(spawnableItems[randomItemToSpawn], gameObject.transform.position, gameObject.transform.rotation);
    }
}
