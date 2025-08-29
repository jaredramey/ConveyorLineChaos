using UnityEngine;

public class ItemDestroyerController : MonoBehaviour
{
    [SerializeField]
    private bool shouldCountMissedPackages;

    private int numMissedPackages = 0;

    [SerializeField]
    private int maxMissedPackages = 10;

    public int MaxMissedPackages
    {
        get {  return maxMissedPackages; }
    }

    public int NumMissedPackages
    {
        get { return numMissedPackages; }
    }

    AudioSource alarm;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        alarm = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ConveyorBeltItem")
        {
            Destroy(other.gameObject);

            if (shouldCountMissedPackages)
            {
                alarm.Play();
                numMissedPackages++;
            }
        }
    }
}
