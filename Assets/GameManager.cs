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
    [SerializeField]
    GameObject conveyorBelt;
    private ConveyorBeltController conveyorBeltController;

    [Header("Game Variables")]
    float totalShiftTime = 0f;


    #region UnityMethods
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        conveyorBeltController = conveyorBelt.GetComponent<ConveyorBeltController>();
        //previousBeltSpeed = conveyorBeltController
    }

    // Update is called once per frame
    void Update()
    {
        // Update the shift clock
        UpdateShiftTimer();
    }
    #endregion UnityMethods

    #region TextRelated
    private void UpdateShiftTimer()
    {
        totalShiftTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(totalShiftTime / 60);
        int seconds = Mathf.FloorToInt(totalShiftTime % 60);
        shiftTimerTMP.text = string.Format("{00:00}:{01:00}", minutes, seconds);
    }
    private void UpdateBeltSpeed()
    {
        
    }
    #endregion TextRelated
}
