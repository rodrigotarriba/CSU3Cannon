using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// We are using this class from the interface to controll the kind of input we are using.
/// </summary>

public class CannonMouseInputScheme : ICannonInputScheme
{
    //This is a constructor for the calss (similar to __init__ in python) that is called whenever the class is created or initialized.
    public CannonMouseInputScheme()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    //Our aim input was walso in the cannonController - instead of using Input.GetAxis(CannonMouseInputScheme Y) which we have in cannonController, we create and manage it here.
    public Vector2 AimInput()
    {
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    //Disable mouse so you can easily get out of the app
    public void Dispose()
    {
        Cursor.lockState = CursorLockMode.None;

    }

    //Reference to whether the fire has been triggered, which is a boolean in our cannon controller - returns the boolean (Input.GetButtonDown("Fire1"))) - this is not a reference to cannonController, but it is checking in the same input.
    public bool FireTriggered()
    {
        return Input.GetButtonDown("Fire1");

    }
}
