using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    public float VerticalInput => verticalInput;
    public float HorizontalInput => horizontalInput;
    public static Action OnPrimaryDownAction;
    public static Action OnInteractDownAction;
    public static Action OnPrimaryUpAction;
    public static Action OnSecondaryDownAction;
    public static Action OnSecondaryUpAction;
    public static Action OnPrimaryHoldAction;
    public static Action OnDownSprint;
    public static Action OnUpSprint;
    public static Action OnReloadAction;
    public static Action OnJumpAction;
    public static Action OnDownCrouch;
    public static Action OnUpCrouch;

    private void Update()
    {
        HandleInput();
        BusyActions();
    }

    private void HandleInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }
    private void BusyActions()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            OnInteractDownAction?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnPrimaryDownAction?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            OnPrimaryUpAction?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            OnSecondaryDownAction?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            OnSecondaryUpAction?.Invoke();
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            OnPrimaryHoldAction?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnReloadAction?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpAction?.Invoke();
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            OnPrimaryHoldAction?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            OnDownSprint?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            OnUpSprint?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            OnDownCrouch?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            OnUpCrouch?.Invoke();
        }
    }

    public void ResetInputs()
    {
        horizontalInput = 0;
        verticalInput = 0;
    }
}
