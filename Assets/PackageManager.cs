using UnityEngine;

public class PackageManager : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ConveyorBeltItem")
        {
            Destroy(collision.gameObject);
        }
    }
}
