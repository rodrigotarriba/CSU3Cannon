using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch2InstantiateObjs : MonoBehaviour
{

    [SerializeField] GameObject m_cube;
    [SerializeField] GameObject m_mainCamera;
    [SerializeField] Transform m_emptyObjTransform;
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InstantiateCube();
    }



    void InstantiateCube()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(m_cube, new Vector3(0, 100, 0), Quaternion.Euler(0, 45, 0));
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Instantiate(m_cube, new Vector3(0, -10, 0), Quaternion.Euler(new Vector3(0, 45, 0) + m_mainCamera.transform.rotation.eulerAngles), m_mainCamera.transform);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Instantiate(m_cube, m_emptyObjTransform);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Vector3 randompos = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100));
            Instantiate(m_cube, randompos, Quaternion.identity);
        }




    }


}

