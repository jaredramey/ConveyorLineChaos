using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UIElements;
//using UnityEditor.Rendering.LookDev;

[RequireComponent(typeof(CharacterController))]
public class FPController : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float maxSpeed => sprintInput ? sprintSpeed : walkSpeed;
    public float acceleration = 15f;

    [SerializeField]
    public float walkSpeed = 3.5f;
    [SerializeField]
    public float sprintSpeed = 8f;

    public Vector3 currentVelocity { get; private set; }
    public float currentSpeed { get; private set; }

    public bool Sprinting
    {
        get
        {
            return sprintInput && currentSpeed > 0.1f;
        }
    }


    [Header("Looking Parameters")]
    public Vector2 lookSensitivity = new Vector2(0.1f, 0.1f);
    public float pitchLimit = 85f;

    [SerializeField]
    float currentPitch = 0f;

    public float CurrentPitch
    {
        get => currentPitch;

        set
        {
            currentPitch = Mathf.Clamp(value, -pitchLimit, pitchLimit);
        }
    }

    [Header("CameraParameters")]
    [SerializeField]
    float cameraNormalFOV = 60f;
    [SerializeField]
    float cameraSprintFOV = 80f;
    [SerializeField]
    float cameraFOVSmoothing = 3f;

    float TargetCameraFOV
    {
        get
        {
            return Sprinting ? cameraSprintFOV : cameraNormalFOV;
        }
    }

    [Header("Input")]
    public Vector2 moveInput;
    public Vector2 lookInput;
    public bool sprintInput;

    [Header("Components")]
    [SerializeField]
    CharacterController characterController;
    [SerializeField]
    Camera mainCamera;

    private void OnValidate()
    {
        if(characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }
    }

    #region UnityDefaults
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveUpdate();
        LookUpdate();
        CameraUpdate();
    }
    #endregion UnityDefaults


    #region ControllerMethods
    private void MoveUpdate()
    {
        Vector3 motion = transform.forward * moveInput.y + transform.right * moveInput.x;
        motion.y = 0.0f;
        motion.Normalize();

        if(motion.sqrMagnitude >= 0.1f)
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, motion * maxSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, acceleration * Time.deltaTime);
        }

        float verticalVelocity = Physics.gravity.y * 20f * Time.deltaTime;

        Vector3 fullVelocity = new Vector3(currentVelocity.x, verticalVelocity, currentVelocity.z);

        characterController.Move(fullVelocity * Time.deltaTime);

        // Update Speed
        currentSpeed = currentVelocity.magnitude;
    }

    private void LookUpdate()
    {
        Vector2 input = new Vector2(lookInput.x * lookSensitivity.x, lookInput.y * lookSensitivity.y);

        // Looking up & down
        CurrentPitch -= input.y;

        mainCamera.transform.localRotation = Quaternion.Euler(CurrentPitch, 0f, 0f);

        // Looking left & right
        transform.Rotate(Vector3.up * input.x);
    }

    private void CameraUpdate()
    {
        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, TargetCameraFOV, cameraFOVSmoothing * Time.deltaTime);
    }
    #endregion ControllerMethods
}
