using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float playerSpeed;
    private float horizontalInput;
    private float verticalInput;

    private float horizontalScreenLimit = 9.5f;
    //private float verticalScreenLimit = 6.5f;

    public GameObject bulletPrefab;

    void Start()
    {
        playerSpeed = 6f;
        
    }

    void Update()
    {

        Movement();
        Shooting();

    }

    void Shooting()
    {
        //if the player presses the SPACE key, create a projectile
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }

    void Movement()
    {
        //Read the input from the player
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        //Move the player
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * playerSpeed);

        //Player leaves the screen horizontally
        if(transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        //Player can't go past the middle of the screen or the bottom
        float topLimit = 0.5f;
        float bottomLimit = -3.5f;

        float clampedY = Mathf.Clamp(transform.position.y, bottomLimit, topLimit);
        transform.position = new Vector3(transform.position.x, clampedY, 0);

    }

}
