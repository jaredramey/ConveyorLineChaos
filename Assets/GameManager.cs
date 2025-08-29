using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Text Components")]
    [SerializeField]
    TextMeshPro shiftTimerTMP;
    [SerializeField]
    TextMeshPro beltSpeedTMP;
    [SerializeField]
    TextMeshPro packagesFinishedTMP;

    [Header("Conveyor Belt components")]
    [SerializeField]
    float beltSpeedIncrement = 0.2f;
    float previousBeltSpeed = 0f;
    float previousItemSpeed = 0f;
    [SerializeField]
    private float beltSpeedIncrementTime = 10f;
    private float nextIncrementTime = 0f;
    [SerializeField]
    GameObject conveyorBelt;
    private ConveyorBeltController conveyorBeltController;

    [Header("Package Components")]
    [SerializeField]
    GameObject package;
    PackageManager packageManager;
    int previousNumCompletedBoxes = 0;

    [Header("Item Spawner Components")]
    [SerializeField]
    GameObject itemSpawner;
    ConveyorBeltItemSpawner conveyorBeltItemSpawner;
    [SerializeField]
    float itemSpawnTimeReduction = 0.05f;

    [Header("Item Destroyer components")]
    [SerializeField]
    GameObject itemDestroyer;
    ItemDestroyerController itemDestroyerController;

    [Header("End Screen components")]
    GameOverScreen gameOverScreen;
    [SerializeField]
    TextMeshPro missedPackagesTMP;
    private int previousMissedPackages = 0;

    [Header("Game Variables")]
    float totalShiftTime = 0f;


    #region UnityMethods
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        conveyorBeltController = conveyorBelt.GetComponent<ConveyorBeltController>();
        packageManager = package.GetComponent<PackageManager>();

        conveyorBeltItemSpawner = itemSpawner.GetComponent<ConveyorBeltItemSpawner>();

        itemDestroyerController = itemDestroyer.GetComponent<ItemDestroyerController>();

        gameOverScreen = gameObject.GetComponent<GameOverScreen>();
    }

    // Update is called once per frame
    void Update()
    {
        // TEXT RELATED
        // Update the shift clock
        UpdateShiftTimerText();

        //Update num completed boxes
        UpdateCompletedBoxesText();

        // Update num missed items
        UpdateMissedPackages();

        // GAME LOGIC RELATED
        nextIncrementTime += Time.deltaTime;

        if(nextIncrementTime > beltSpeedIncrementTime)
        {
            nextIncrementTime = 0f;
            IncreaseBeltSpeed();
        }
    }

    private void FixedUpdate()
    {
        // Update the belt speed text. Shouldn't have to do this often.
        UpdateBeltSpeedText();

        CheckForFailState();
    }
    #endregion UnityMethods

    #region TextRelated
    private void UpdateShiftTimerText()
    {
        totalShiftTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(totalShiftTime / 60);
        int seconds = Mathf.FloorToInt(totalShiftTime % 60);
        shiftTimerTMP.text = string.Format("{00:00}:{01:00}", minutes, seconds);
    }
    private void UpdateBeltSpeedText()
    {
        if (conveyorBeltController.ConveyorSpeed > previousBeltSpeed)
        {
            previousBeltSpeed = conveyorBeltController.ConveyorSpeed;
            previousItemSpeed = conveyorBeltController.ItemSpeed;
            string formattedSpeed = previousBeltSpeed.ToString("0.00");
            Debug.Log(formattedSpeed);
            beltSpeedTMP.text = formattedSpeed;
        }
    }

    private void UpdateCompletedBoxesText()
    {
        if(previousNumCompletedBoxes < packageManager.NumCompletedBoxes)
        {
            previousNumCompletedBoxes = packageManager.NumCompletedBoxes;
            packagesFinishedTMP.text = previousNumCompletedBoxes.ToString();
        }
    }

    private void UpdateMissedPackages()
    {
        if(itemDestroyerController.NumMissedPackages > previousMissedPackages)
        {
            previousMissedPackages = itemDestroyerController.NumMissedPackages;
            missedPackagesTMP.text = previousMissedPackages.ToString();
        }
    }
    #endregion TextRelated

    #region GameLogic
    private void IncreaseBeltSpeed()
    {
        // Increase the spawn rate of items
        if (conveyorBeltItemSpawner.spawningCooldownTime - itemSpawnTimeReduction > 0.5)
        {
            conveyorBeltItemSpawner.spawningCooldownTime -= itemSpawnTimeReduction;
            // If the spawn rate gets too high then we need to increase the belt speed
            // We only want to do this when the spawn rate is at least 1 item per second and
            // only when its a whole number.
            // Won't lie this is a hacky solution at best.
            // TODO: Clean this mess up
            if(conveyorBeltItemSpawner.spawningCooldownTime <= 1 &&
                (int)conveyorBeltItemSpawner.spawningCooldownTime == conveyorBeltItemSpawner.spawningCooldownTime)
            {
                conveyorBeltController.ConveyorSpeed += beltSpeedIncrement;
                conveyorBeltController.MaxItemSpeed += 4;
            }
        }
    }

    private void CheckForFailState()
    {
       if(itemDestroyerController.NumMissedPackages >= itemDestroyerController.MaxMissedPackages)
        {
            gameOverScreen.EndGame();
        }
    }
    #endregion GameLogic
}
