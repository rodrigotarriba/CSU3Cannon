using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CannonBall : MonoBehaviour
{
    protected Rigidbody m_cannonBallRigidBody; //a protected variable is accesible to the children of the class they are at

    [SerializeField] protected Animator m_cannonBallAnimator;
    [SerializeField] private float explosionRadius = 9.0f;
    [SerializeField] private float explosionForce = 12.0f;
    [SerializeField] private float explosionUpwardsModifier = 1.0f; //what does this do?

    private static readonly int exploded = Animator.StringToHash("Exploded");

    public virtual CannonBallType ballType => CannonBallType.Normal; //this is how you assign a variable from an enum without it being a regular variable, it needs to be a function.

    protected CannonBallsPool pool; //


    private void Awake()
    {
        m_cannonBallRigidBody = GetComponent<Rigidbody>();
        m_cannonBallAnimator = GetComponent<Animator>();

    }



    // This is a virtual method that is called externally by the controller to instantiate/activate a new cannonball to be propelled - the children of this class add more functions to it
    public virtual void SetUp(Vector3 m_fireForce, CannonBallsPool objectPool)
    {
        //resetting initial inercia of the ball
        m_cannonBallRigidBody.angularVelocity = Vector3.zero;
        m_cannonBallRigidBody.velocity = Vector3.zero;
        m_cannonBallRigidBody.isKinematic = false;

        //referencing this objects pool
        pool = objectPool;


        // Adding a impulse force to the cannonball
        m_cannonBallRigidBody.AddForce(m_fireForce, ForceMode.Impulse);

        //Adding an angular velocity to the cannon ball, lets make it rotate. 
        m_cannonBallRigidBody.angularVelocity = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
         
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {

        // rotate around the collision contact point
        transform.rotation = Quaternion.FromToRotation(transform.up, collision.GetContact(0).normal) * transform.rotation;


        // reset inertia, velocity, angular velocity etc = 0
        m_cannonBallRigidBody.velocity = Vector3.zero;
        m_cannonBallRigidBody.angularVelocity = Vector3.zero;
        m_cannonBallRigidBody.isKinematic = true;


        // trigger the explosion animation inside the animator
        m_cannonBallAnimator.SetTrigger(exploded);


        // save the position of the explosion the moment we hit something
        Vector3 explosionPosition = transform.position;

        // get all the touching colliders
        // OverlapSphere returns an array of all the colliders colliding with a specific sphere a moment in time - you assing a position, the radius of the circle and the layer mask that will trigger collisions.
        Collider[] touchingColliders = Physics.OverlapSphere(explosionPosition, explosionRadius, LayerMask.GetMask("Targets"));

         
        foreach (Collider collider in touchingColliders)
        {

            Rigidbody collidedRigidbody = collider.GetComponent<Rigidbody>();
            if (collidedRigidbody != null)
            {
                collidedRigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, explosionUpwardsModifier, ForceMode.Impulse);
                /// Acceleartion - applies force over acceleration ignoring mass
                /// Force - add continues force to the rigidbody, using its mass . f = m.a
                /// Impulse - kinch impulse
                /// Velocity change - slowing it down, ignoring its mass
                /// If your collider is very small and you use a very hard force, you can breakthrough colliders.
                
               
            }
        }



    }


    // OnTriggerEnter takes a collider - we have access to other properties, such as Materials, Bounds, Attached rigid body and other features.
    private void OnTriggerEnter(Collider other)
    {
        // the moment the cannonball touches the water it doesnt explode so it triggers this without the animation

        //Now, when the cannonball needs to be destroyed, it will put itself back into the CannonBallsPool
        pool.ReleaseCannonBall(this, ballType);
    }


    public void OnFinishedExplosionAnimation()
    {
        //when the cannonball touches an object and needs to explode, it triggers the animation first, which then triggers this method at the end, releasing the instance back into the CannonBallsPool (this used to destroy the gameobject before pooling)

        pool.ReleaseCannonBall(this, ballType);

    }



}
