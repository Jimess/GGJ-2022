using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFallController : MonoBehaviour
{
    [Header("Joystick")]
    [SerializeField] Joystick joystick;

    public float horizontalForceModifier;
    public float verticalForceModifier;
    public float maxVelocity;
    public float controlDelayAfterCollision = 2f;

    private float horizontal;
    private float vertical;
    private bool controlsEnabled = true;
    private bool controlsInverted = false;

    Vector2 inputDirection;
    Rigidbody2D rigidBody;

    public delegate void OnVelocityChange(Vector2 velocityChange);
    public static OnVelocityChange onVelocityChange;

    private Coroutine cooldownCoroutine;

    private bool disabled = false;

    private void Awake()
    {
        MobOnCollisionTrigger.onCollision += PostCollisionVelocityChange;
        CollisionManager.onCollision += OnFallCollision;
        WorldSpinManager.OnCameraRotation += InvertControls;
    }

    private void OnDestroy()
    {
        MobOnCollisionTrigger.onCollision -= PostCollisionVelocityChange;
        CollisionManager.onCollision -= OnFallCollision;
    }

    public void PostCollisionVelocityChange(IMob collisionMob)
    {
        rigidBody.AddForce(new Vector2(0, collisionMob.GetSpeedAdjustment()));
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        //Time.timeScale = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (disabled) {
            return;
        }
        HandleInput();
    }

    private void FixedUpdate()
    {
        if (disabled) {
            return;
        }
        HandleMovementUpdate();
        onVelocityChange(rigidBody.velocity);
        ClampVelocity();
    }

    private void HandleInput()
    {
        if (controlsEnabled == false)
        {
            horizontal = 0f;
            vertical = 0f;
            return;
        }

        

        horizontal = !controlsInverted ? joystick.Horizontal/*Input.GetAxis("Horizontal")*/ : -joystick.Horizontal /*-Input.GetAxis("Horizontal")*/;
        vertical = !controlsInverted ? joystick.Vertical : -joystick.Vertical /*Input.GetAxis("Vertical") : -Input.GetAxis("Vertical")*/;

        inputDirection = new Vector2(horizontal * horizontalForceModifier, vertical * verticalForceModifier);
    }

    private void HandleMovementUpdate()
    {
        if (controlsEnabled == false)
        {
            return;
        }

        rigidBody.AddForce(inputDirection);
    }

    private void ClampVelocity()
    {
        //print("VELOCITY: " + rigidBody.velocity.magnitude);
        //rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity, maxVelocity);
        //rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Clamp(rigidBody.velocity.y, -maxVelocity, maxVelocity));


    }

    public void OnFallCollision()
    {
        if (cooldownCoroutine != null) {
            StopCoroutine(cooldownCoroutine);
        }

        cooldownCoroutine = StartCoroutine(TriggerDisableControls());
    }

    public void InvertControls() {
        controlsInverted = !controlsInverted;
    }

    public IEnumerator TriggerDisableControls()
    {
        controlsEnabled = false;
        yield return new WaitForSeconds(controlDelayAfterCollision);
        controlsEnabled = true;
    }

    public void DisableCharacter() {
        disabled = true;
        //rigidBody.isKinematic = true;
        //rigidBody.v
    }
}
