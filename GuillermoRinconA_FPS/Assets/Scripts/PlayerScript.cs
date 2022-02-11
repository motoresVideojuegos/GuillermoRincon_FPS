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

    private bool canMove;

    public int maxHealth;
    public int currentHealth;
    public int playerScore;
    Camera camera;
    Rigidbody playerRb;

    public CanvasController canvas;
    public DeathMenuController deathMenu;


    private void Awake() {

        Cursor.lockState = CursorLockMode.Locked;

        canMove = true;
        camera = Camera.main;
        playerRb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        playerScore = 0;

        Time.timeScale = 1;

        canvas.LifeBar(currentHealth, maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        if(canMove){
            Movement();
            CameraView();
        }
        

        if(Input.GetButtonDown("Jump")){
            Jump();
        }
    }

    public void Movement(){
        float x = Input.GetAxis("Horizontal") * velocity;
        float z = Input.GetAxis("Vertical") * velocity;

        Vector3 direction = transform.right * x + transform.forward * z;

        direction.y = playerRb.velocity.y;

        playerRb.velocity = direction;
        playerRb.velocity.Normalize();

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

    public void takeDmg(int dmgTaken){
        currentHealth -= dmgTaken;
        canvas.LifeBar(currentHealth, maxHealth);
        if(currentHealth <= 0){
            canMove = false;
            canvas.gameObject.SetActive(false);
            deathMenu.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }

    public void addLife(int heal){
        currentHealth = Mathf.Clamp(currentHealth + heal, 0, maxHealth);
        canvas.LifeBar(currentHealth, maxHealth);
    }

    public void addPoints(int points){
        playerScore += points;
        canvas.UpdateScore(playerScore);
    }
    
}
