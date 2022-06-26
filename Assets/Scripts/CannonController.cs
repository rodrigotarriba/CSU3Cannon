using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    private Transform projectileFirePoint; // this is where the projectile comes off from, if hit Vectr3.forward, it is where you are aiming
    [SerializeField]
    private float projectileShootingForce;
    [SerializeField]
    private CannonBallType m_cannonBallTypeShot;
    



    [Header("Aiming Settings")]
    [SerializeField]
    private Material m_redBarrelMaterial;
    [SerializeField]
    private Material m_defaultBarrelMaterial;


    [Header("Object Pooling")]
    [SerializeField]
    private CannonBallsPool pool; //this is the very first instance of the pooling, from here is where it gets passed around to others.

    //Private Variables
    private RaycastHit[] m_aimingHits;
    private bool m_fireDisabled = false;





    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pool.SetUp(20);


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
        TryFireCannon();


    }


    private void LateUpdate()
    {
        // this one turns any object far away red at aim
        AimingTurningRed();
    }

    // this one turns any object far away red at aim
    private void AimingTurningRed()
    {
        // activate a new array with a permissible number
        RaycastHit[] m_aimingHits = new RaycastHit[5];


        // use raycast non alloc to determine which element is being hit
        var numberOfHits = Physics.RaycastNonAlloc(projectileFirePoint.position, projectileFirePoint.forward, m_aimingHits, Mathf.Infinity, ~0);

        // guard clause - make sureraycast its hitting something
        if (numberOfHits < 1)
        {
            return;
        }

        // go through all raycasts and detect which ones are targets
        for (var i = 0; i < numberOfHits; i++)
        {
           // if layer is targets, turn red
            if (m_aimingHits[i].collider.gameObject.layer == LayerMask.NameToLayer("Targets"))
            {
                m_aimingHits[i].collider.gameObject.GetComponent<MeshRenderer>().material = m_redBarrelMaterial;
            }

        }

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

    private void TryFireCannon()
    {
        // do we have that left mouse button that can be pushed

        // guard clause - save resources - if our fire is disabled or we dont have a button just break the function
        if(m_fireDisabled || !Input.GetButtonDown("Fire1"))
        {
            return;
        }

        CannonBall m_cannonBall = pool.GetCannonBall(m_cannonBallTypeShot);
        m_cannonBall.transform.position = projectileFirePoint.position;
        
        //you setup the cannonball to be launched, we are sending the pool object because it will use it when it is self destroyed and disabled
        m_cannonBall.SetUp(projectileFirePoint.forward * projectileShootingForce, pool); 


    }

    //Unlock the mouse when the fire will no longer be enabled
    public void DisableFire()
    {
        m_fireDisabled = true;
        Cursor.lockState = CursorLockMode.None;
    }


}
