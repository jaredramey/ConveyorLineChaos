using TMPro;
using UnityEngine;

public class PackageManager : MonoBehaviour
{
    [SerializeField]
    private int numObjectsPerBox = 6;
    [SerializeField]
    private int curNumObjectsInBox = 0;

    private int numCompletedBoxes = 0;

    AudioSource itemInPackage;

    public int NumCompletedBoxes
    {
        get 
        { 
            return numCompletedBoxes; 
        }
        private set 
        { 
            numCompletedBoxes = value; 
        }
    }

    [SerializeField]
    TextMeshPro numItemsText;

    #region UnityMethods
    private void Start()
    {
        itemInPackage = GetComponent<AudioSource>();
    }

    private void OnValidate()
    {
        if (numItemsText == null)
        {
            Debug.LogError("[PackageManager.cs] NumItemsText not set!");
        }
    }
    #endregion UnityMethods

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ConveyorBeltItem")
        {
            itemInPackage.Play();
            Destroy(collision.gameObject);
            curNumObjectsInBox++;

            if(curNumObjectsInBox >= numObjectsPerBox)
            {
                curNumObjectsInBox = 0;
                numCompletedBoxes++;
            }

            UpdateFloatingText();
        }
    }

    private void UpdateFloatingText()
    {
        numItemsText.text = string.Format("{00}/{01}", curNumObjectsInBox, numObjectsPerBox);
    }
}
