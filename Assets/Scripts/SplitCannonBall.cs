using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using CannonApp;

/// <summary>
/// This object is attached to a variant of CannonBall
/// </summary>

public class SplitCannonBall : CannonBall
{
    private static readonly int specialAvailableHash = Animator.StringToHash("SpecialAvailable");
    private static readonly int specialUsedHash = Animator.StringToHash("SpecialUsed");

    private float remainingSplitTime;
    public float splitTime = 0.7f;
    public float splitAngle = 20.0f;     //when it split it will at 20 degree angle
    


    [SerializeField] protected CannonBall simpleCannonBall; //this is the regular cannonball that will be instanciated once the split cannon ball time goes off

    // we are overriding the typ of cannoball inherited from CannonBall
    public override PoolObjectId PoolId => PoolObjectId.SplitCannonBall;


    // Update is called once per frame
    void Update()
    {
        // countdown time to split the cannonballs
        remainingSplitTime -= Time.deltaTime;
        if (remainingSplitTime <= 0)
        {
            Debug.Log($"splitting");
            SpawnSplitCannonBalls();
        }
    }


    private void SpawnSplitCannonBalls()
    {
        // set spawn position and the forward dirction of the split cannon balls
        // creating a parent gameobject and instantiating 2 things that are moving away from it. we have the overall forward directoin and the individual balls.
        var position = transform.position;
        var forward = m_cannonBallRigidBody.velocity;


        // we need to set the forward directions the split cannon balls that are being created will have at an angle
        var ball1Forward = Quaternion.AngleAxis(-splitAngle, Vector3.up) * forward;
        var ball2Forward = Quaternion.AngleAxis(splitAngle, Vector3.up) * forward;


        //instantiate the splitted cannonballs by grab the balls from the pool, - these will be of Normal type and explode the regular way
        var ball1 = pool.GetObject(PoolObjectId.DefaultCannonBall);
        var ball2 = pool.GetObject(PoolObjectId.DefaultCannonBall);


        //  propell the balls forward and set the angular velocity (call setup)
        ball1.GetComponent<CannonBall>().SetUp(ball1Forward, pool);
        ball2.GetComponent<CannonBall>().SetUp(ball1Forward, pool);


        // trigger the special used animation
        m_cannonBallAnimator.SetTrigger(specialUsedHash);
        enabled = false;

    }


    public override void SetUp(Vector3 m_fireForce, ObjectPool objectPool)
    {
        // calling what its already in the base class 
        base.SetUp(m_fireForce, objectPool);

        //it allows us to trigger the special available animation
        m_cannonBallAnimator.SetTrigger(specialAvailableHash);
        remainingSplitTime = splitTime;
        enabled = true;

    }


    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        enabled = false;
    }
}
