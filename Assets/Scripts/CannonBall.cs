using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private Rigidbody m_cannonBallRigidBody;
    private Animator m_cannonBallAnimator; 

    private static readonly int exploded = Animator.StringToHash("Exploded");
    

    private void Awake()
    {
        m_cannonBallRigidBody = GetComponent<Rigidbody>();
        m_cannonBallAnimator = GetComponent<Animator>();

    }


    public void SetUp(Vector3 m_fireForce)
    {
        // Adding a impulse force to the cannonball
        m_cannonBallRigidBody.AddForce(m_fireForce, ForceMode.Impulse);

        //Adding an angular velocity to the cannon ball, lets make it rotate. 
        m_cannonBallRigidBody.angularVelocity = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
         
    }

    private void OnCollisionEnter(Collision collision)
    {
        // rotate around the collision contact point
        transform.rotation = Quaternion.FromToRotation(transform.up, collision.GetContact(0).normal) * transform.rotation;


        //reset inertia, velocity, angular velocity etc = 0
        m_cannonBallRigidBody.velocity = Vector3.zero;
        m_cannonBallRigidBody.angularVelocity = Vector3.zero;
        m_cannonBallRigidBody.isKinematic = true;


        //trigger the explosion
        m_cannonBallAnimator.SetTrigger(exploded);


    }


    // OnTriggerEnter takes a collider - we have access to other properties, such as Materials, Bounds, Attached rigid body and other features.
    private void OnTriggerEnter(Collider other)
    {
        // the moment the cannonball touches the water, it will be destroyed
        Destroy(gameObject);
    }



    public void OnFinishedExplosionAnimation()
    {
        // triggered by the animation, destroying instance when the animations finishes
        Destroy(gameObject);
    }

    public void OnAnimationMiddlePoint()
    {
        Debug.Log($"it reached the center");
    }
}
