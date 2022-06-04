using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [Header("Cannon Rotation")]
    
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

    [Header("Cannon Rotation Transforms")]
    [SerializeField]
    private Transform cannonBarrelTransform;
    [SerializeField]
    private Transform cannonBaseTransform;

    [Header("Projectile Settings")]
    [SerializeField]
    private CannonBall projectilePrefab;
    [SerializeField]
    private Transform projectileFirePoint;
    [SerializeField]
    private float projectileShootingForce;





    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        return;
    }

    // Update is called once per frame
    void Update()
    {
        AimCannon();
        FireCannon();

    }

    private void AimCannon()
    {
        //rotate along the x axis
        float horizontal = Input.GetAxis("Mouse X");
        float newBaseRotation = cannonBaseTransform.localRotation.eulerAngles.y + rotationSpeed * horizontal; 


        //rotate along the y axis
        float vertical = Input.GetAxis("Mouse Y");
        float newBarrelRotation = cannonBarrelTransform.localRotation.eulerAngles.x - rotationSpeed * vertical; //we put minus because when the mouse goes down, the barrel goes up, when the mouse goes up the barrel goes down.

        // limit the rotation in both axiss
        newBaseRotation = Mathf.Clamp(newBaseRotation, minYRotation, maxYRotation);
        newBarrelRotation = Mathf.Clamp(newBarrelRotation, minXRotation, maxXRotation);

        // apply the rotation
        cannonBaseTransform.localRotation = Quaternion.Euler(0, newBaseRotation, 0);
        cannonBarrelTransform.localRotation = Quaternion.Euler(newBarrelRotation, 0, 0);

        //rotate along the 
        //stayed at the 1:11 minutes


    }

    private void FireCannon()
    {
        // do we have that left mouse button that can be pushed

        // guard clause - save resources
        if(!Input.GetButtonDown("Fire1"))
        {
            return;
        }


        Instantiate(projectilePrefab, projectileFirePoint.position, Quaternion.identity);





    }


}
