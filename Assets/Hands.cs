using UnityEngine;

public class Hands : MonoBehaviour
{
    // Quick singleton for now
    public static Hands Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
