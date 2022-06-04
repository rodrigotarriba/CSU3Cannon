using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField]
    private float maxYRotation = 130f;
    [SerializeField]
    private float minYRotation = 50f;
    [SerializeField]
    private float maxXRotation = 75f;
    [SerializeField]
    private float minXRotation = 15f;
    [Tooltip("The speed the cannon rotates at")]
    [SerializeField]
    private float rotationSpeed = 10f;

    [SerializeField]
    private Transform canonBarrelTransform;
    [SerializeField]
    private Transform cannonBaseTransform;


    // Start is called before the first frame update
    void Start()
    {
        return;
    }

    // Update is called once per frame
    void Update()
    {
        return;

    }

    private void AimCannon()
    {
        //rotate along the x axis
        float horizontal = Input.GetAxis("Mouse X");
        float newBaseRotation = cannonBaseTransform.localRotation.eulerAngles.y + rotationSpeed * horizontal; 


        //rotate along the y axis
        float vertical = Input.GetAxis("Mouse Y");

        //rotate along the 
        //stayed at the 1:11 minutes
    }


}
