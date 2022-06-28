using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This is the child of Vehicle, which will rotate the car similar to how cars do it
/// </summary>


public class Car : Vehicle
{

    [Tooltip("Angles/Second")]
    [SerializeField] protected float m_rotationSpeed;

    protected override void Update()
    {
        base.Update();

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.down * m_rotationSpeed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * m_rotationSpeed * Time.deltaTime);
        }


    }
}
