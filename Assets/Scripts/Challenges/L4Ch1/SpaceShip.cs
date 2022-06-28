using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : Vehicle
{

    [Tooltip("Meter/Second")]
    [SerializeField] protected float m_turnSpeed;

    protected override void Update()
    {
        base.Update();

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * m_turnSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * m_turnSpeed * Time.deltaTime);
        }
    }

}
