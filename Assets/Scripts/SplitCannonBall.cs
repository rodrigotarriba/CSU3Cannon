using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SplitCannonBall : CannonBall
{
    private static readonly int specialAvailableHash = Animator.StringToHash("SpecialAvailable");
    private static readonly int specialUsedHash = Animator.StringToHash("SpecialUsed");

    public float splitTime = 0.7f;
    public float splitAngle = 20.0f;
    //when it split it will at 20 degree angle

    [SerializeField] protected CannonBall simpleCannonBall; //this is the regular cannonball that will be instanciated once the split cannon ball time goes off


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // countdown time to split the cannonballs
        splitTime -= Time.deltaTime;


        if (splitTime <= 0)
        {
            SpawnSplitCannonBalls();
        }
    }


    private void SpawnSplitCannonBalls()
    {
        // set spawn position and the forward dirction of the split cannon balls
        // creating a parent gameobject and instantiating 2 things that are moving away from it. we have the overall forward directoin and the individual balls.
        var position = transform.position;
        var forward = m_cannonBallRigidBody.velocity;


        // now that we have two of those balls, we need to set the forward directions of the split cannon balls that are created at an angle
        var ball1Forward = Quaternion.AngleAxis(-splitAngle, Vector3.up) * forward;
        var ball2Forward = Quaternion.AngleAxis(splitAngle, Vector3.up) * forward;


        //instantiate the split cannonballs
        var ball1 = Instantiate(simpleCannonBall, position, Quaternion.identity);
        var ball2 = Instantiate(simpleCannonBall, position, Quaternion.identity);


        // propell the balls forward and set the angular velocity (call setup)
        ball1.SetUp(ball1Forward);
        ball2.SetUp(ball2Forward);


        // trigger the special used animation
        m_cannonBallAnimator.SetTrigger(specialUsedHash);
        enabled = false;



    }


    public override void SetUp(Vector3 m_fireForce)
    {
        // calling what its already in the base class 
        base.SetUp(m_fireForce);

        //it allows us to trigger the special available animation
        m_cannonBallAnimator.SetTrigger(specialAvailableHash);
    }


    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        enabled = false;
    }
}
