using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFallController : MonoBehaviour
{
    public float horizontalForceModifier;
    public float verticalForceModifier;
    public float maxVelocity;

    private float horizontal;
    private float vertical;

    Vector2 inputDirection;
    Rigidbody2D rigidBody;

    public delegate void OnVelocityChange(Vector2 velocityChange);
    public static OnVelocityChange onVelocityChange;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        Time.timeScale = 0.5f;
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
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        inputDirection = new Vector2(horizontal * horizontalForceModifier, vertical * verticalForceModifier);
    }

    private void HandleMovementUpdate()
    {
        rigidBody.AddForce(inputDirection);
    }

    private void ClampVelocity()
    {
        rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity, maxVelocity);
    }
}
