using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(FPController))]
public class FPPlayer : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private FPController FPcontroller;

    #region InputHandling
    void OnMove(InputValue value)
    {
        FPcontroller.moveInput = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        FPcontroller.lookInput = value.Get<Vector2>();
    }

    void OnSprint(InputValue value)
    {
        FPcontroller.sprintInput = value.isPressed;
    }
    #endregion InputHandling

    #region UnityMethods
    private void OnValidate()
    {
        if (FPcontroller == null)
        {
            FPcontroller = GetComponent<FPController>();
        }
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    #endregion UnityMethods
}
