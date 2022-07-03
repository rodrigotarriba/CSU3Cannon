using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a straight class, a class that exists in our project and we can call whenever we want.
/// </summary>




namespace CannonApp
{

    [System.Serializable]
    public class ObjectPool
    {
        //all the possible cannon balls to be used
        [SerializeField]
        private List<MonoBehaviour> possibleObjects;


        private Dictionary<PoolObjectId, List<MonoBehaviour>> pools;
        // focused in the cannonball cashing only


        //we need to create a method that will instantiate these cannonballs right at the beginning, whenever we shoot, instead of the cannon controller. 
        private MonoBehaviour Instantiate(MonoBehaviour myobject)
        {
            var newObject = Object.Instantiate(myobject); //object is a unity class for objects 
            newObject.gameObject.GetComponent<IPoolObject>().Deactivate(); //we deactivate it so it can be stored in the dictionary

            return newObject;
        }


        //pre-warm count the number of prefabs we want to create and activate in the scene - there are tradeoffs, because you could always just do a List of pooled objects, you dont have to set up a dictionary - with a forloop and its done. 
        //The benefit of this pattern, you have a higher loading time, not only takes time, we use the SetUp functions beacuse of the way Unity serialization works, but often times it works as a constructor.


        //creates the dictionary
        public void SetUp(int preWarmCount)
        {
            pools = new Dictionary<PoolObjectId, List<MonoBehaviour>>(possibleObjects.Count);
        

            foreach (var objectPrefab in possibleObjects)
            {
                var pool = new List<MonoBehaviour>(preWarmCount);

                for (int i = 0; i < preWarmCount; i++)
                {
                    //create those prefabs and add them to the pools dictionary, so we can keep track
                    pool.Add(Instantiate(objectPrefab));
                }

                pools[objectPrefab.GetComponent<IPoolObject>().PoolId] = pool; //this is how you grab the value of an enum and assign it to a key in a dictionary


            }
        }

        //retrieves the next ball to be grabbed
        public MonoBehaviour GetObject(PoolObjectId objectType)
        {
            var pool = pools[objectType];
            MonoBehaviour myObject;

            //expression to assign a new instance to the dictionary if it ran out, or if it doesnt have enough instances.
            if(pool.Count == 0)
            {
                //if it doesnt have the value, instance a new ball
                myObject = Instantiate(possibleObjects.Find(theobject => theobject.GetComponent<IPoolObject>().PoolId == objectType));
            }
            else
            {
                //if the list inside the dictionary does have enough instances, remove the first value and activate it.
                myObject = pool[0];
                pool.RemoveAt(0);

            }
            myObject.gameObject.GetComponent<IPoolObject>().Activate();
            return myObject;
        }

        //once the cannonball has been used, it can return back to the dictionary for storage
        public void ReleaseObject(MonoBehaviour myObject, PoolObjectId objectType)
        {
            //gets added back into the List<cannonBall> 
            pools[objectType].Add(myObject);

            //returns to its deactivated state
            myObject.gameObject.GetComponent<IPoolObject>().Deactivate();


        }
    }
}
