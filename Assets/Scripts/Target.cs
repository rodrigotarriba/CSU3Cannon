using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// manages targets - destroys them and highlightes them when pointing
/// 
/// after C3L8 - instead of this class referencing the levelcontroller on setup, we will reference it to the service locator.
/// </summary>

namespace CannonApp
{
    public class Target : MonoBehaviour
    {
        private int m_cannonBallTriggerLayerIndex;
        private int m_waterTriggerLayerIndex;
        [SerializeField]
        private Material m_regularMaterial;
        [SerializeField]
        private Material m_redMaterial;

        private void Start()
        {
            Debug.Log($"addingonemoretarget");
            GameServices.GetService<LevelController>().RegisterTarget();
        }

        private void Awake()
        {
            m_cannonBallTriggerLayerIndex = LayerMask.NameToLayer("CannonBall");

            m_waterTriggerLayerIndex = LayerMask.NameToLayer("WaterTrigger");
        }


        private void Update()
        {
            //this could be optimized with a guard clause
            GetComponent<MeshRenderer>().material = m_regularMaterial;
        }


        // updates LevelController letting it know we are reducing one target
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == m_waterTriggerLayerIndex)
            {
               
                //tells the Level Controller service that a target has been destroyed. 
                GameServices.GetService<LevelController>().TargetDestroyed();

                //kill this object
                Destroy(gameObject); 
            }
        }



    }
}