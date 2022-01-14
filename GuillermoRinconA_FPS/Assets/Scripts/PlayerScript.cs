using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement")]
    public float velocity;
    public float jumpForce;

    [Header("Camera")]
    public float mouseSensibility;
    public float maxViewX;
    public float minViewX;
    public float rotationX;

    Camera camera;
    Rigidbody playerRb;

    private void Awake() {
        camera = Camera.main;
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
        CameraView();

        if(Input.GetButtonDown("Jump")){
            Jump();
        }
    }

    public void Movement(){
        float x = Input.GetAxis("Horizontal") * velocity;
        float z = Input.GetAxis("Vertical") * velocity;

        Vector3 direction = transform.right * x + transform.forward * z;

        playerRb.velocity = direction;
    }

    private void CameraView(){
        float y = Input.GetAxis("Mouse X") * mouseSensibility;
        rotationX += Input.GetAxis("Mouse Y") * mouseSensibility;

        rotationX = Mathf.Clamp(rotationX, minViewX, maxViewX);

        camera.transform.localRotation = Quaternion.Euler(-rotationX, 0, 0);
        transform.eulerAngles += Vector3.up * y;
    }

    private void Jump(){

        Ray hit = new Ray(transform.position, Vector3.down);

        if(Physics.Raycast(hit, 1.1f)){
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }
    
}
