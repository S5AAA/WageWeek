using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity  {

    public float speed = 5;
    public float jumpSpeed = 5;
    public float cameraSpeed = 5;
    public GunAsset gunAsset;
    public GameObject localCamera;

    private Transform camTransform;
    private Rigidbody rigidBody;
    private float timeFromFire = 999;
    private bool reloading = false;
    private float deltaTimeMilis;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        camTransform = localCamera.GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        deltaTimeMilis = Time.deltaTime*1000;
        DoMovement();
        DoCameraMovement();
        DoMouseInput();
    }

    void DoMouseInput()
    {
        if (timeFromFire > gunAsset.gunStats.fireRate) 
        {
            if (Input.GetButton("Fire1"))
            {
                timeFromFire = 0;
                DoGunFire();
            }
        } else
        {
            timeFromFire += deltaTimeMilis;
        }
    }

    void DoGunFire()
    {

        RaycastHit hit;
        if(Physics.Raycast(localCamera.transform.position, localCamera.transform.rotation * Vector3.forward, out hit))
        {
            Debug.Log("Hit");
            Debug.DrawLine(localCamera.transform.position, hit.point, Color.red, 100);
            Entity hitEntity = hit.collider.gameObject.GetComponent<Entity>();
            if (hitEntity)
            {
                Debug.Log("Hit Entity");
                hitEntity.DoDamage(gunAsset.gunStats.damage);
            }
        }
    }

    void DoCameraMovement()
    {
        float moveX = Input.GetAxis("Mouse X");
        float moveY = Input.GetAxis("Mouse Y");
        

        //transform.RotateAround(transform.position, Vector3.up, moveX);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y+moveX*cameraSpeed*Time.deltaTime, 0);
        camTransform.RotateAround(camTransform.position, camTransform.rotation * Vector3.left, moveY * cameraSpeed * Time.deltaTime);

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
