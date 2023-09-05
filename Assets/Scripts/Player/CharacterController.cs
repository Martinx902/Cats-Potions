//Martin Pérez Villabrille
//Cat & Potions
//Last Modification 24/03/2023

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour
{
    #region Inspector Variables

    [Header("RigidBody")]
    [Space(15)]
    [SerializeField]
    private Rigidbody rb;

    [Header("Movement Speeds")]
    [Space(15)]
    [SerializeField]
    [Range(1, 10)]
    private float walkingSpeed = 5;

    [SerializeField]
    [Range(0f, 1f)]
    private float catGravity = 1;

    [SerializeField]
    private bool canRun = true;

    [SerializeField]
    [Range(1, 15)]
    private float runningSpeed = 8;

    [SerializeField]
    [Range(0, 0.5f)]
    private float accelerationRate = 1f;

    [Header("Rotation Turn Speed")]
    [Space(15)]
    [SerializeField]
    [Tooltip("Turn Speed of the player towards another position, in degrees, 360 = 1sec")]
    private float turnSpeed = 540;

    [SerializeField]
    [Range(50, 100)]
    private float multiplyingSpeedFactor = 70f;

    [Header("Particles")]
    [Space(15)]
    [SerializeField]
    private ParticleSystem runningParticles;

    #endregion Inspector Variables

    #region Private Variables

    private float currrentSpeed = 5;

    private KeyCode runningKey = KeyCode.LeftShift;

    private Vector3 input;

    private Quaternion startRotation;
    private Vector3 endRotation;
    private Quaternion finalRotation;

    private bool rotating = false;

    private bool isRunning = false;
    private bool isMoving = false;
    private bool isWalking = false;

    private bool canMove = true;

    private Animator anim;

    #endregion Private Variables

    private void Start()
    {
        anim = transform.GetComponentInChildren<Animator>(true);
    }

    private void Update()
    {
        if (canMove)
        {
            GatherInput();
            Look();
        }

        ParticleAnim();
        UpdateAnim();

        if (!canMove)
        {
            isMoving = false;
            isRunning = false;
            isWalking = false;
            rb.velocity = Vector3.zero;
            return;
        }
    }

    private void FixedUpdate()
    {
        //Updating Physics
        if (canMove)
        {
            Move();
        }
    }

    private void GatherInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        //Quick input check
        if (input.magnitude > 0.1f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
            isWalking = false;
        }

        if (Input.GetKey(runningKey) && isMoving)
        {
            if (canRun)
            {
                //running

                isRunning = true;
                isWalking = false;

                currrentSpeed += accelerationRate;

                if (currrentSpeed >= runningSpeed)
                {
                    currrentSpeed = runningSpeed;
                }
            }
        }
        else
        {
            //walking

            if (isMoving)
            {
                isWalking = true;
            }

            isRunning = false;

            currrentSpeed -= accelerationRate;

            if (currrentSpeed <= walkingSpeed)
            {
                currrentSpeed = walkingSpeed;
            }

            currrentSpeed = walkingSpeed;
        }
    }

    private void Look()
    {
        if (input != Vector3.zero)
        {
            //TODO Gestionar bien las rotaciones, seguramente el problema sea que se actualiza la posición del player mientras se está girando
            //en vez de tener como referencia base sobre la que girar.

            StartRotation();

            finalRotation = Quaternion.LookRotation(endRotation, Vector3.up);

            transform.rotation = Quaternion.Slerp(startRotation, finalRotation, 50f * Time.deltaTime);

            //Apply the rotation
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }
    }

    private void StartRotation()
    {
        startRotation = transform.rotation;

        endRotation = (transform.position + input.ToIso()) - transform.position;

        rotating = true;
    }

    private void Move()
    {
        //Update position
        rb.velocity = ((transform.forward * input.normalized.magnitude) * currrentSpeed * multiplyingSpeedFactor * Time.fixedDeltaTime);

        rb.AddForce(new Vector3(0, -catGravity * 1000, 0), ForceMode.Force);
    }

    private void UpdateAnim()
    {
        anim.SetBool("walk", isWalking);
        anim.SetBool("run", isRunning);
    }

    private void ParticleAnim()
    {
        if (isRunning)
        {
            runningParticles.Play();
        }
        else
        {
            runningParticles.Stop();
        }
    }

    private void PlayerFallsAsleep()
    {
        //Make a coroutine that stops moving the player and show an animation of him falling asleep
        canMove = false;
        anim.SetTrigger("sleepEverywhere");
        PopUpUI.instance.Show(PopUpType.FellAsleep);
    }

    public void PlayerGoesToBed()
    {
        canMove = false;
        anim.SetTrigger("sleepBed");
    }

    public void CatTalking(bool isTalking)
    {
        anim.SetBool("c_Talk", isTalking);
    }

    public void CanMove(bool _canMove)
    {
        this.canMove = _canMove;
    }

    public void CanMove(float waitTime)
    {
        StartCoroutine(StopMovement(waitTime));
    }

    private IEnumerator StopMovement(float waitTime)
    {
        canMove = false;

        yield return new WaitForSeconds(waitTime);

        canMove = true;
    }

    #region Event Handlers

    private void OnEnable()
    {
        TimeController.pastHisBedtimeDelegate += PlayerFallsAsleep;
    }

    private void OnDisable()
    {
        TimeController.pastHisBedtimeDelegate -= PlayerFallsAsleep;
    }

    #endregion Event Handlers
}