using UnityEngine;
using System.Collections;

public class playerMovementController : MonoBehaviour {
    private CharacterController charCon;
    private Vector3 moveDirection = Vector3.zero;
    public float walkSpeed = 5F;
    public float sprintSpeed = 5F;
    public float jumpSpeed = 10F;
    public float gravity = 30F;
    public float maxFallSpeed = 30F;
    private float currentFallSpeed = 0;
	// Use this for initialization
	void Start () {
        charCon = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!Menu.showMenu) {
			handleGravity ();
			handleMovement ();
			movePlayer ();
		}
    }

    private void handleMovement()
    {
        if (charCon.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection *= (Input.GetButton("Sprint") && Vector3.Dot(moveDirection,Vector3.forward)>0.5F? sprintSpeed : walkSpeed);
            if (Input.GetButton("Jump"))
            {
                currentFallSpeed = jumpSpeed;
            }
            moveDirection = transform.TransformDirection(moveDirection);
        }

    }

    private void handleGravity()
    {
        currentFallSpeed -= gravity * Time.deltaTime;
        if (Mathf.Abs(currentFallSpeed) > maxFallSpeed)
        {
            currentFallSpeed = maxFallSpeed*Mathf.Sign(currentFallSpeed);
        }
        moveDirection.y = currentFallSpeed;
        if (charCon.isGrounded)
        {
            currentFallSpeed = 0;
        }
    }

    private void movePlayer()
    {
        charCon.Move(moveDirection * Time.deltaTime);
    }
}
