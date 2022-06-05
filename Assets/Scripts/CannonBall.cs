using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private Rigidbody m_cannonBallRigidBody;


    private void Awake()
    {
        m_cannonBallRigidBody = GetComponent<Rigidbody>();
    }


    public void SetUp(Vector3 m_fireForce)
    {
        // Adding a impulse force to the cannonball
        m_cannonBallRigidBody.AddForce(m_fireForce, ForceMode.Impulse);

        //Adding an angular velocity to the cannon ball, lets make it rotate. 
        m_cannonBallRigidBody.angularVelocity = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));

    }

}
