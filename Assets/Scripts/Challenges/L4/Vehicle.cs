using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This is the parent class for any moving vehicle, allows to go back and forth per WASD inputs
/// </summary>


public class Vehicle : MonoBehaviour
{
    [Tooltip("Meter/Second")]
    [SerializeField] protected float m_forwardSpeed;



    protected virtual void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * m_forwardSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * m_forwardSpeed * Time.deltaTime);
        }
    }
}
