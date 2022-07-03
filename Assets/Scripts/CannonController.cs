using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using CannonApp;

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
    

    [Header("Input Settings")]
    [SerializeField]
    private bool useKeyboard;
    private bool m_fireDisabled = false;
    private ICannonInputScheme inputScheme;




    private void Awake()
    {
        if (useKeyboard)
        {
            inputScheme = new CannonKeyboardInputScheme();
        }
        else
        {
            inputScheme = new CannonMouseInputScheme();
        }

        //Setup the number of elements per each balltype that will be created in the pool to help performance.
        pool.SetUp(20);


    }


    // Start is called before the first frame update
    void Start()
    {
        GameServices.GetService<LevelController>().levelEnded += OnLevelEnded;
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
        //Strategy Pattern
        //creating a nerw variable
        var input = inputScheme.AimInput();

        //

        
        //rotate along the x axis
        //float horizontal = input.x;
        float newBaseRotation = cannonBaseTransform.localRotation.eulerAngles.y + rotationSpeed * input.x; 


        //rotate along the y axis
        //float vertical = input.y;
        float newBarrelRotation = cannonBarrelTransform.localRotation.eulerAngles.x - rotationSpeed * input.y; //we put minus because when the mouse goes down, the barrel goes up, when the mouse goes up the barrel goes down.

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
        /*if(m_fireDisabled || !Input.GetButtonDown("Fire1"))
        {
            return;
        }

        */


        //Checks the interface - guard clause to see if the fire has been triggered.
        if(m_fireDisabled || !inputScheme.FireTriggered())
        {
            //Debug.Log($"FireTriggered");
            return;
        }

        CannonBall m_cannonBall = pool.GetCannonBall(m_cannonBallTypeShot);
        m_cannonBall.transform.position = projectileFirePoint.position;
        
        //you setup the cannonball to be launched, we are sending the pool object because it will use it when it is self destroyed and disabled
        m_cannonBall.SetUp(projectileFirePoint.forward * projectileShootingForce, pool); 


    }

    //Unlock the mouse when the fire will no longer be enabled
    //changed name from DisableFire to OnLevelEnded()
    public void OnLevelEnded()
    {

        m_fireDisabled = true;
        //OLD before implementing interface in U3L7 ==> Cursor.lockState = CursorLockMode.None;

        //Call the CannonMouseInputScheem class to disable the mouse lock
        inputScheme.Dispose();
    }


}
