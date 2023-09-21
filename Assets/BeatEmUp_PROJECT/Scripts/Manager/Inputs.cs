using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    public Gamepad myPad;
    public Keyboard myKeyBoard;
    public Mouse myMouse;
    public Vector2 myLeftStick;
    public Vector2 myRightStick;
    public bool cross_tick;
    public bool cross_hold;
    public bool triangle_tick;
    public bool triangle_hold;
    public bool circle_tick;
    public bool circle_hold;
    public bool square_tick;
    public bool square_hold;

    void SetInputDevice()
    {
        if (myPad == null)
        {
            myPad = Gamepad.current;
        }
        if (myKeyBoard == null)
        {
            myKeyBoard = Keyboard.current;
        }
        if (myMouse == null)
        {
            myMouse = Mouse.current;
        }
    }

    void SimulateInputs()
    {
        SetInputDevice();
        #region LeftStick
        if (myKeyBoard != null && myPad != null)
        {
            myLeftStick = myPad.leftStick.ReadValue();
        }
        else if (myKeyBoard != null && myPad == null)
        {
            myLeftStick.x = myKeyBoard.aKey.isPressed && !myKeyBoard.dKey.isPressed ? -1 : !myKeyBoard.aKey.isPressed && myKeyBoard.dKey.isPressed ? 1 : 0;
            myLeftStick.y = myKeyBoard.sKey.isPressed && !myKeyBoard.wKey.isPressed ? -1 : !myKeyBoard.sKey.isPressed && myKeyBoard.wKey.isPressed ? 1 : 0;
        }
        else
        {
            myLeftStick = myPad.leftStick.ReadValue();
        }
        #endregion
        #region Right Stick
        if (myMouse != null && myPad != null)
        {
            myRightStick = myPad.rightStick.ReadValue();
        }
        else if (myMouse != null && myPad == null)
        {
            myRightStick = myMouse.delta.ReadValue();
        }
        else
        {
            myRightStick = myPad.rightStick.ReadValue();
        }
        #endregion
        #region Actions
        if (myPad != null)
        {
            cross_tick = myPad.buttonSouth.wasPressedThisFrame;
            cross_hold = myPad.buttonSouth.isPressed;
            triangle_tick = myPad.buttonNorth.wasPressedThisFrame;
            triangle_hold = myPad.buttonNorth.isPressed;
            circle_tick = myPad.buttonEast.wasPressedThisFrame;
            circle_hold = myPad.buttonEast.isPressed;
            square_tick = myPad.buttonWest.wasPressedThisFrame;
            square_hold = myPad.buttonWest.isPressed;
        }
        else
        {
            cross_tick = myKeyBoard.spaceKey.wasPressedThisFrame;
            cross_hold = myKeyBoard.spaceKey.isPressed;
            triangle_tick = myMouse.leftButton.wasPressedThisFrame;
            triangle_hold = myMouse.leftButton.isPressed;
        }
        #endregion
        #region Vibraor
        #endregion
    }

    private void Update()
    {
        SimulateInputs();
    }
}
