using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	public Camera cam;
	public float speed;
	public float jumpForce;
	
	private InputAction moveAction;
	private InputAction jumpAction;
	private Rigidbody2D body;
    
    void Start()
    {
	    body = GetComponent<Rigidbody2D>();
	    moveAction = InputSystem.actions.FindAction("Move");
	    jumpAction = InputSystem.actions.FindAction("Jump");
    }

    private float moveX;
    private void Update()
    {
	    moveX = moveAction.ReadValue<Vector2>().x;
    }

    private List<Collision> activeCollisions = new List<Collision>();

    private void OnCollisionEnter(Collision other)
    {
	    // TODO: Fix this, it doesn't add anything
	    activeCollisions.Add(other);
    }

    private void OnCollisionExit(Collision other)
    {
	    activeCollisions.Remove(other);
    }

    private bool IsGrounded()
    {
	    return activeCollisions.Count != 0;
    }

    void FixedUpdate()
    {
	    Vector2 velocity = new Vector2(moveX * speed, body.linearVelocityY);
	    if (jumpAction.IsPressed() && IsGrounded())
	    {
		    velocity += new Vector2(0, jumpForce);
	    }

	    body.AddForce(velocity);
	    Vector2 newCameraPos = Vector2.Lerp(cam.transform.position, body.transform.position, 0.1f);
	    cam.transform.position = new Vector3(newCameraPos.x, newCameraPos.y, -10);
    }
}
