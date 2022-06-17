using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private int m_cannonBallTriggerLayerIndex;
    private int m_waterTriggerLayerIndex;
    [SerializeField]
    private Material m_regularMaterial;
    [SerializeField]
    private Material m_redMaterial;

    private void Awake()
    {
        m_cannonBallTriggerLayerIndex = LayerMask.NameToLayer("CannonBall");

        m_waterTriggerLayerIndex = LayerMask.NameToLayer("WaterTrigger");
    }




    private void Update()
    {
        GetComponent<MeshRenderer>().material = m_regularMaterial;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == m_waterTriggerLayerIndex)
        {
            Destroy(gameObject);
        }
    }



}
