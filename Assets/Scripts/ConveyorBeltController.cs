using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConveyorBeltController : MonoBehaviour
{
    [SerializeField]
    public float ItemSpeed
    { 
        get
        {
            return itemSpeed;
        }

        set
        {
            itemSpeed = value;
        }
    }

    public float ConveyorSpeed
    {
        get 
        { 
            return conveyorSpeed; 
        }
        set 
        { 
            conveyorSpeed = value; 
        }
    }

    public float MaxItemSpeed
    {
        get
        {
            return maxItemSpeed;
        }

        set 
        {
            maxItemSpeed = value;
        }
    }

    [SerializeField]
    private float itemSpeed, maxItemSpeed, conveyorSpeed;
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private List<GameObject> onBelt;

    private Material beltMaterial;


    #region UnityMethods
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beltMaterial = GetComponent<Material>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Move the texture of the belt so it looks like the belt is moving
        GetComponent<MeshRenderer>().material.mainTextureOffset -= new Vector2(0, 1) * conveyorSpeed * Time.deltaTime;

        // The max speed will slowly increment so the itemSpeed will eventually need to catch up
        if (maxItemSpeed > itemSpeed)
        {
            itemSpeed = maxItemSpeed;
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < onBelt.Count; i++)
        {
            // Juuuust in case the item gets deleted before its considered "off the belt"
            if (onBelt[i] != null)
            {
                // Calculate the force to add
                Vector3 forceToAdd = itemSpeed * direction;
                // Get the components and information we'll need
                Rigidbody itemRigidBody = onBelt[i].GetComponent<Rigidbody>();
                Vector3 currentVelocity = itemRigidBody.linearVelocity;

                // Add force to the item to move it down the belt
                itemRigidBody.AddForce(forceToAdd);
                // Clamp the velocity if needed
                if (itemRigidBody.linearVelocity.magnitude > maxItemSpeed)
                {
                    itemRigidBody.linearVelocity = Vector3.ClampMagnitude(itemRigidBody.linearVelocity, maxItemSpeed);
                }
            }
            else
            {
                // Remove the gameobject from the list if it's null
                onBelt.Remove(onBelt[i]);
            }
        }
    }
    #endregion UnityMethods

    #region CollisionMethods
    private void OnCollisionEnter(Collision collision)
    {
        onBelt.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }
    #endregion CollisionMethods
}
