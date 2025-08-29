using TMPro;
using UnityEngine;

public class ItemsPerSeondSignUpdater : MonoBehaviour
{
    [SerializeField]
    GameObject itemSpawner;
    ConveyorBeltItemSpawner itemSpawnerManager;
    [SerializeField]
    TextMeshPro itemsPerSecondSign;
    private float previousTimeBetweenSpawns;
    private float itemsPerSecond;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemSpawnerManager = itemSpawner.GetComponent<ConveyorBeltItemSpawner>();
        previousTimeBetweenSpawns = itemSpawnerManager.spawningCooldownTime;
        CalculateItemsPerSecond();
        UpdateItemsPerSecondSign();
    }

    // Update is called once per frame
    void Update()
    {
        if(previousTimeBetweenSpawns > itemSpawnerManager.spawningCooldownTime)
        {
            previousTimeBetweenSpawns = itemSpawnerManager.spawningCooldownTime;
            CalculateItemsPerSecond();
            UpdateItemsPerSecondSign();
        }
    }

    private void CalculateItemsPerSecond()
    {
        itemsPerSecond = (60 / previousTimeBetweenSpawns) / 60;
    }

    private void UpdateItemsPerSecondSign()
    {
        itemsPerSecondSign.text = itemsPerSecond.ToString("0.00");
    }
}
