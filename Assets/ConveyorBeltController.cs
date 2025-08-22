using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltController : MonoBehaviour
{
    [SerializeField]
    private float speed, conveyorSpeed;
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private List<GameObject> onBelt;

    private Material beltMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beltMaterial = GetComponent<Material>();
    }

    // Update is called once per frame
    private void Update()
    {
        GetComponent<MeshRenderer>().material.mainTextureOffset -= new Vector2(0, 1) * conveyorSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        for(int i = 0; i < onBelt.Count; i++)
        {
            Vector3 forceToAdd = speed * direction;

            Debug.Log($"Item {onBelt[i].name} is getting [{forceToAdd.x}, {forceToAdd.y}, {forceToAdd.z}] added to it");
            onBelt[i].GetComponent<Rigidbody>().AddForce(speed * direction);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        onBelt.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }
}
