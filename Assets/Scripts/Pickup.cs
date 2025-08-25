using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pickup : MonoBehaviour
{
    bool isHolding = false;

    [SerializeField]
    float throwForce = 600f;
    [SerializeField]
    float maxDistance = 3f;
    float distance;

    Hands hands;
    Rigidbody rigidBody;

    Vector3 objectPosition;

    #region UnityMethods
    private void OnValidate()
    {
        if(rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody>();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody> ();
        hands = Hands.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(isHolding)
        {
            Hold();
            rigidBody.linearVelocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
        }
    }
    #endregion UnityMethods

    #region ObjectMovement
    private void GetDistance()
    {
        distance = Vector3.Distance(this.transform.position, hands.transform.position);
    }

    private void Hold()
    {
        GetDistance();

        if (distance >= maxDistance)
        {
            Drop();
        }

        rigidBody.linearVelocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;

        // If holding and the player presses right click
        if(Input.GetMouseButtonDown(1))
        {
            // throw
            rigidBody.AddForce(hands.transform.forward * throwForce);
            Drop();
        }
    }

    private void Drop()
    {
        if(isHolding)
        {
            isHolding = false;
            objectPosition = this.transform.position;
            this.transform.position = objectPosition;
            this.transform.SetParent(null);
            rigidBody.useGravity = true;
        }
    }

    private void OnMouseDown()
    {
        // pickup
        if (hands != null)
        {
            GetDistance();

            if (distance <= maxDistance)
            {
                isHolding = true;
                rigidBody.useGravity = false;
                rigidBody.detectCollisions = true;

                this.transform.SetParent(hands.transform);
            }
        }
        else
        {
            Debug.LogError("[Pickup.cs] Hands object not found!");
        }
    }

    private void OnMouseUp()
    {
        // drop
        Drop();
    }

    private void OnMouseExit()
    {
        // drop
        //Drop();
    }
    #endregion ObjectMovement
}
