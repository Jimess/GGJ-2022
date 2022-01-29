using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFallController : MonoBehaviour
{
    public float horizontalForceModifier;
    public float verticalForceModifier;
    public float maxVelocity;
    public float controlDelayAfterCollision = 2f;

    private float horizontal;
    private float vertical;
    private bool controlsEnabled = true;

    Vector2 inputDirection;
    Rigidbody2D rigidBody;

    public delegate void OnVelocityChange(Vector2 velocityChange);
    public static OnVelocityChange onVelocityChange;

    private void Awake()
    {
        MobOnCollisionTrigger.onCollision += PostCollisionVelocityChange;
        CollisionManager.onCollision += OnFallCollision;
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
        HandleInput();
    }

    private void FixedUpdate()
    {
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

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

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
        print("VELOCITY: " + rigidBody.velocity.magnitude);
        //rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity, maxVelocity);
        //rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Clamp(rigidBody.velocity.y, -maxVelocity, maxVelocity));


    }

    public void OnFallCollision()
    {
        StartCoroutine(TriggerDisableControls());
    }

    public IEnumerator TriggerDisableControls()
    {
        controlsEnabled = false;
        yield return new WaitForSeconds(controlDelayAfterCollision);
        controlsEnabled = true;
    }
}
