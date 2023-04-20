using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Controller controls;

    [Header("Basic Params")]
    public int playerID;
    public Animator targetAnimator;

    [Header("Movement Params")]
    public float moveSpeed;
    public float jumpForce;
    public Vector2 move;
    private Vector3 moveDirection;
    public bool isGrounded;

    [Header("Physics Params")]
    public Rigidbody hip;
    public ConfigurableJoint joint;
    protected Rigidbody rb;
    public float regularBalance;
    public float layDownBalance;

    [Header("Hands Params")]
    public GrabAndDrop handLeft;
    public GrabAndDrop handRight;

    public bool isPunching;
    public bool isLaying;
    private bool interactDetected;
    public bool inLayDownEvent;
    public bool resetInteract;
    public bool inImportantTrigger;
    public bool blockLayDown;


    private void Awake()
    {
        //playerInput = GetComponent<PlayerInput>();
        //controls = new Controller();

        //Debug.Log(playerInput.playerIndex);

        //if (playerInput.playerIndex == playerID)
        //{
        //    controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        //    controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;

        //    controls.Gameplay.Jump.performed += ctx => Jump();
        //}
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        targetAnimator.SetBool("IsGrounded", isGrounded);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveDirection = new Vector3(move.x, 0, move.y).normalized;

        if (moveDirection.magnitude >= 0.1f && !isLaying)
        {
            Move();
            Rotate();

            targetAnimator.SetBool("IsWalking", true);
        }
        else
        {
            targetAnimator.SetBool("IsWalking", false);

        }

        //Punch();
    }

    private void Move()
    {
        hip.AddForce(moveDirection * moveSpeed);
    }

    private void Rotate()
    {
        float targetAngle = Mathf.Atan2(moveDirection.z, moveDirection.x) * Mathf.Rad2Deg;

        joint.targetRotation = Quaternion.Euler(0f, targetAngle+90f, 0f);
    }
    public void Jump()
    {
        if (isGrounded && !isLaying)
        {
            hip.AddForce(new Vector3(0, jumpForce, 0));
            isGrounded = false;
        }
    }

    void Punch()
    {
        targetAnimator.SetBool("IsPunching", isPunching);
    }
    
    //private void OnEnable()
    //{
    //    controls.Gameplay.Enable();
    //}
    //private void OnDisable()
    //{
    //    controls.Gameplay.Disable();
    //}

    public void DealWithLayDown()
    {
        if (inLayDownEvent) return;
        isLaying = !isLaying;
        targetAnimator.SetBool("IsLaying", isLaying);

        JointDrive jointXDrive = joint.angularXDrive;
        jointXDrive.positionSpring = isLaying ? layDownBalance : regularBalance;
        joint.angularXDrive = jointXDrive;

        JointDrive jointYZDrive = joint.angularYZDrive;
        jointYZDrive.positionSpring = isLaying ? layDownBalance : regularBalance;
        joint.angularYZDrive = jointYZDrive;
    }

    public void Interact()
    {
        if (inImportantTrigger)
        {
            interactDetected = true;
            StartCoroutine(ResetInteract());
        }
    }

    public bool GetInteractState()
    {
        return interactDetected;
    }
    IEnumerator ResetInteract()
    {
        yield return new WaitUntil(() => resetInteract == true);
        interactDetected = false;
        resetInteract = false;
    }

    public IEnumerator ResetPlayerLayDown()
    {
        yield return new WaitUntil(()=>GameManager.Instance.layDownEventFinished==true);
        inLayDownEvent = false;
        Debug.Log("inLayDownEvent");
    }
}
