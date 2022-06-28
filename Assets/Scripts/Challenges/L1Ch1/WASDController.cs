using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script moves the vehicle front, back, left and right using WASD
/// </summary>

public class WASDController
    : MonoBehaviour
{

    [SerializeField] Transform m_cubeTransform;
    [SerializeField] float m_cubeSpeed;


    // Update is called once per frame
    void Update()
    {
        MoveCube();
    }


    void MoveCube()
    {
        if (Input.GetKey(KeyCode.W))
        {
            m_cubeTransform.Translate(new Vector3(0, 0, m_cubeSpeed) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            m_cubeTransform.Translate(new Vector3(0, 0, -m_cubeSpeed) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            m_cubeTransform.Translate(new Vector3(-m_cubeSpeed, 0, 0) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            m_cubeTransform.Translate(new Vector3(+m_cubeSpeed, 0, 0) * Time.deltaTime);
        }
    }

}
