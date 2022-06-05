using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private int m_cannonBallTriggerLayerIndex;
    private int m_waterTriggerLayerIndex;


    private void Awake()
    {
        m_cannonBallTriggerLayerIndex = LayerMask.NameToLayer("CannonBall");

        m_waterTriggerLayerIndex = LayerMask.NameToLayer("WaterTrigger");
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"entered");
        if (other.gameObject.layer == m_waterTriggerLayerIndex)
        {
            Destroy(gameObject);
        }
        Debug.Log($"it was destroyed");
    }



}
