using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script instantiates cubes in different positions of the game every time certain keys are pressed 
/// </summary>

public class Ch2InstantiateObjs : MonoBehaviour
{

    [SerializeField] GameObject m_cube;
    [SerializeField] GameObject m_mainCamera;
    [SerializeField] Transform m_emptyObjTransform;


    void Update()
    {
        //Listen for inputs every frame
        InstantiateCube();
    }

    //Listen for inputs depending on the key pressed, instances different memebers accordingly.
    void InstantiateCube()
    {
        //A puts the cube in the 0,100,0 position
        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(m_cube, new Vector3(0, 100, 0), Quaternion.Euler(0, 45, 0));
        }

        //B puts the cube inside a parent object at a certain distance from it
        if (Input.GetKeyDown(KeyCode.B))
        {
            Instantiate(m_cube, new Vector3(0, -10, 0), Quaternion.Euler(new Vector3(0, 45, 0) + m_mainCamera.transform.rotation.eulerAngles), m_mainCamera.transform);
        }


        //C positions the cube inside an empty object transform
        if (Input.GetKeyDown(KeyCode.C))
        {
            Instantiate(m_cube, m_emptyObjTransform);
        }

        //D positions a random cube in the environment at its regular rotation
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 randompos = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100));
            Instantiate(m_cube, randompos, Quaternion.identity);
        }


    }


}

