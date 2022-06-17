using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCannonBall : CannonBall
{
    [SerializeField] protected CannonBall simpleCannonBall; //this is the regular cannonball that will be instanciated once the large cannon ball goes off

    public float splitAngle = 20.0f;

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        enabled = false;
        SpawnUpwardsCannonBalls();
        
    }


    private void SpawnUpwardsCannonBalls()
    {

        var position = transform.position;
        var shootingDirection = Vector3.up;



        // now that we have two of those balls, we need to set the forward directions of the split cannon balls that are created at an angle
        var ball1Forward = Quaternion.AngleAxis(-splitAngle, Vector3.forward) * new Vector3(10,10,10);
        var ball2Forward = Quaternion.AngleAxis(splitAngle, Vector3.forward) * new Vector3(10, 10, 10);


        //instantiate the split cannonballs
        Debug.Log($"instantiating the cannons");
        var ball1 = Instantiate(simpleCannonBall, position, Quaternion.identity);
        var ball2 = Instantiate(simpleCannonBall, position, Quaternion.identity);


        // propell the balls forward and set the angular velocity (call setup)
        ball1.SetUp(ball1Forward);
        ball2.SetUp(ball2Forward);




    }

    public override void SetUp(Vector3 m_fireForce)
    {
        // calling what its already in the base class 
        base.SetUp(m_fireForce);

    }


}
