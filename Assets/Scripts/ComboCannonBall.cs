using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CannonApp;


public class ComboCannonBall : CannonBall
{


    public float splitAngle = 20.0f;

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        enabled = false;
        SpawnUpwardsCannonBalls();

    }

    public override PoolObjectId PoolId => PoolObjectId.ComboCannonBall;

    private void SpawnUpwardsCannonBalls()
    {

        var position = transform.position;
        var shootingDirection = Vector3.up;



        // now that we have two of those balls, we need to set the forward directions of the split cannon balls that are created at an angle
        var ball1Upwards = Quaternion.AngleAxis(-splitAngle, Vector3.forward) * new Vector3(10, 10, 10);
        var ball2Upwards = Quaternion.AngleAxis(splitAngle, Vector3.forward) * new Vector3(10, 10, 10);


        //instantiate the split cannonballs
        var ball1 = pool.GetObject(PoolObjectId.DefaultCannonBall);
        var ball2 = pool.GetObject(PoolObjectId.DefaultCannonBall);


        // propell the balls forward and set the angular velocity (call setup)
        ball1.gameObject.GetComponent<CannonBall>().SetUp(ball1Upwards, pool);
        ball2.gameObject.GetComponent<CannonBall>().SetUp(ball2Upwards, pool);

    }

    public override void SetUp(Vector3 m_fireForce, ObjectPool pool)
    {
        // calling what its already in the base class 
        base.SetUp(m_fireForce, pool);
    }


}
