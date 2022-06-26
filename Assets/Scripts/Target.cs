using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// manages targets - destroys them and highlightes them when pointing
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

        private LevelController m_levelController;


        private void Awake()
        {
            m_cannonBallTriggerLayerIndex = LayerMask.NameToLayer("CannonBall");

            m_waterTriggerLayerIndex = LayerMask.NameToLayer("WaterTrigger");
        }


        // Called from outside to give this target object a reference to the level controller - the script will help reduce the number of elements remaining when this is destroyed
        public void SetUp(LevelController controller)
        {
            m_levelController = controller;
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
                //tells Level Controller a target has been destroyed
                m_levelController.TargetDestroyed();

                //kill this object
                Destroy(gameObject); 
            }
        }



    }
}