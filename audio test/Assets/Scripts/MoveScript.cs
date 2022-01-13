using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    private CharacterController controller;
    private float verticalVelocity;
    private float groundedTimer;        // to allow jumping when going down ramps
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    private float gravityValue = 9.81f;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        bool groundedPlayer = controller.isGrounded;
        if (groundedPlayer)
        {
            // cooldown interval to allow reliable jumping even whem coming down ramps
            groundedTimer = 0.2f;
        }
        if (groundedTimer > 0)
        {
            groundedTimer -= Time.deltaTime;
        }

        // slam into the ground
        if (groundedPlayer && verticalVelocity < 0)
        {
            // hit ground
            verticalVelocity = 0f;
        }

        // apply gravity always, to let us track down ramps properly
        verticalVelocity -= gravityValue * Time.deltaTime;

        // gather lateral input control
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // scale by speed
        move *= playerSpeed;

        // only align to motion if we are providing enough input
        if (move.magnitude > 0.05f)
        {
            gameObject.transform.forward = move;
        }

        // allow jump as long as the player is on the ground
        if (Input.GetButtonDown("Jump"))
        {
            // must have been grounded recently to allow jump
            if (groundedTimer > 0)
            {
                // no more until we recontact ground
                groundedTimer = 0;

                // Physics dynamics formula for calculating jump up velocity based on height and gravity
                verticalVelocity += Mathf.Sqrt(jumpHeight * 2 * gravityValue);
            }
        }

        // inject Y velocity before we use it
        move.y = verticalVelocity;

        // call .Move() once only
        controller.Move(move * Time.deltaTime);
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Vector3 normalisedPoint = hit.point;
        normalisedPoint.y = 1;
        transform.LookAt(normalisedPoint);
    }
}
