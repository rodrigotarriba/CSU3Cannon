using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch2InstantiateObjs : MonoBehaviour
{

    [SerializeField] GameObject m_cube;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            //InstantiateCube(DrandomPosition);
        }
    }


    enum InstanceTypes
    {
        AnoParent,
        BchildOfCamera,
        CchildOfTransform,
        DrandomPosition,

    }

//    void InstantiateCube(InstanceTypes type)
//    {
//        switch (type)
//        {
//            case InstanceTypes.AnoParent:
//                Instantiate(m_cube, new Vector3(0, 100, 0), //Quaternion.Euler(0, 45, 0));
//                break;
//            case InstanceTypes.BchildOfCamera:
//                Instantiate(m_cube, )
//                break;
//            case InstanceTypes.CchildOfTransform:
//                break;
//            case InstanceTypes.DrandomPosition:
//                break;
//            default:
//                break;
//        }





    }


}

