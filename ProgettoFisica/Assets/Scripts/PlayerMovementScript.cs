using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {

    Rigidbody rb;

    [SerializeField] float moveSpeed, jumpForce;
    
    Vector3 moveDirection;

    [SerializeField] Transform groundCheck;
    const float groundedRadius = .2f;

    bool isGrounded;
	bool jump;

	void Awake()
	{
        rb = gameObject.GetComponent<Rigidbody>();
	}


    //Uso Update per registrare l'input
    void Update()
    {
		if (isGrounded) {
            //Input sullo spostamento sulla superficie della sfera
            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

            //Jump
            if (Input.GetKeyDown(KeyCode.Space)) {
                jump = true;
            }
        }
    }

    //Uso FixedUpdate per applicare la fisica
    void FixedUpdate()
    {
        //Controllo che il Player non sia in aria
        isGrounded = false;
        Collider[] colliders = Physics.OverlapSphere(groundCheck.position, groundedRadius);
        for (int i = 0; i < colliders.Length; i++) {
			if (colliders[i].tag.Equals("Planet")) {
                isGrounded = true;
            }
        }

        //Spostamento del Player
        rb.MovePosition(rb.position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);

        //Salto del Player
		if (jump) {
            rb.AddForce(transform.up * (PlanetScript.Gravity(rb.mass) + jumpForce), ForceMode.Impulse);
            jump = false;
        }
    }
}
