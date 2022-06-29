using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public interface ICannonInputScheme
{
    //..
    bool FireTriggered();

    //Describe the aim input
    Vector2 AimInput();

    //a method, controlling the cursor state, if its locked into the screen or not.
    void Dispose();

}
