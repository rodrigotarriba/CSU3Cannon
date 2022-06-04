using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController
    : MonoBehaviour
{

    [SerializeField] Transform m_cubeTransform;
    [SerializeField] float m_cubeSpeed;




    private void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

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
