using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5;
    public float jumpSpeed = 5;
    public float cameraSpeed = 5;

    public GameObject camera;
    private Transform camTransform;
    private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        camTransform = camera.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        DoMovement();
        DoCameraMovement();
    }

    void DoCameraMovement()
    {
        float moveX = Input.GetAxis("Mouse X");
        float moveY = Input.GetAxis("Mouse Y");
        

        //transform.RotateAround(transform.position, Vector3.up, moveX);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y+moveX, 0);
        camTransform.RotateAround(camTransform.position, camTransform.rotation * Vector3.left, moveY);

    }

    void DoMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = rigidBody.velocity.y / speed;
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, moveY, moveZ);

        rigidBody.velocity = rigidBody.transform.rotation * movement * speed;
        if (IsGrounded())
        {
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("Pressed Jump");
                rigidBody.velocity += jumpSpeed * Vector3.up;
            }
        }
    }

    bool IsGrounded()
    {
        float DisstanceToTheGround = GetComponent<Collider>().bounds.extents.y;

        return Physics.Raycast(transform.position, Vector3.down, DisstanceToTheGround + 0.1f);
    }
}
