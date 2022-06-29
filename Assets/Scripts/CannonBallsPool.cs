using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a straight class, a class that exists in our project and we can call whenever we want.
/// </summary>

[System.Serializable]



public class CannonBallsPool
{
    //all the possible cannon balls to be used
    [SerializeField]
    private List<CannonBall> possibleCannonBallPrefabs;

    private Dictionary<CannonBallType, List<CannonBall>> pools;
    // focused in the cannonball cashing only


    //we need to create a method that will instantiate these cannonballs right at the beginning, whenever we shoot, instead of the cannon controller. 
    private CannonBall Instantiate(CannonBall prefab)
    {
        var newBall = Object.Instantiate(prefab); //object is a unity class for objects 
        newBall.gameObject.SetActive(false); //we deactivate it so it can be stored in the dictionary

        return newBall;
    }


    //pre-warm count the number of prefabs we want to create and activate in the scene - there are tradeoffs, because you could always just do a List of pooled objects, you dont have to set up a dictionary - with a forloop and its done. 
    //The benefit of this pattern, you have a higher loading time, not only takes time, we use the SetUp functions beacuse of the way Unity serialization works, but often times it works as a constructor.


    //creates the dictionary
    public void SetUp(int preWarmCount)
    {
        pools = new Dictionary<CannonBallType, List<CannonBall>>(possibleCannonBallPrefabs.Count);
        

        foreach (var ballPrefab in possibleCannonBallPrefabs)
        {
            var pool = new List<CannonBall>(preWarmCount);

            for (int i = 0; i < preWarmCount; i++)
            {
                //create those prefabs and add them to the pools dictionary, so we can keep track
                pool.Add(Instantiate(ballPrefab));
            }

            pools[ballPrefab.ballType] = pool; //this is how you grab the value of an enum and assign it to a key in a dictionary


        }
    }

    //retrieves the next ball to be grabbed
    public CannonBall GetCannonBall(CannonBallType ballType)
    {
        var pool = pools[ballType];
        CannonBall ball;

        //expression to assign a new instance to the dictionary if it ran out, or if it doesnt have enough instances.
        if(pool.Count == 0)
        {
            //if it doesnt have the value, instance a new ball
            ball = Instantiate(possibleCannonBallPrefabs.Find(prefab => prefab.ballType == ballType));
        }
        else
        {
            //if the list inside the dictionary does have enough instances, remove the first value and activate it.
            ball = pool[0];
            pool.RemoveAt(0);

        }
        ball.gameObject.SetActive(true);
        return ball;
    }

    //once the cannonball has been used, it can return back to the dictionary for storage
    public void ReleaseCannonBall(CannonBall ball, CannonBallType ballType)
    {
        //gets added back into the List<cannonBall> 
        pools[ballType].Add(ball);
        
        //returns to its deactivated state
        ball.gameObject.SetActive(false);


    }
}
