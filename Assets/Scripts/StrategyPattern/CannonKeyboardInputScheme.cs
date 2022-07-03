using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CannonApp;

public class CannonKeyboardInputScheme : ICannonInputScheme
{
    //This is a constructor for the calss (similar to __init__ in python) that is called whenever the class is created or initialized.

    //Our aim input is also in the CannonMouseInputScheme - but here we are switching things to be done with the keyboard.
    public Vector2 AimInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    //Disable mouse so you can easily get out of the app
    public void Dispose()
    {
        Cursor.lockState = CursorLockMode.None;

    }

    //Reference to whether the fire has been triggered, which is a boolean in our cannon controller - returns the boolean (Input.GetButtonDown("Fire1"))) - this is not a reference to cannonController, but it is checking in the same input.
    public bool FireTriggered()
    {
        return Input.GetKeyDown(KeyCode.Space);

    }
}
