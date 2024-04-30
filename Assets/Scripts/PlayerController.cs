using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public float jumpHeight = 250f;
    public bool isGrounded;
    
    private int score;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    
    void Start()
    {
        score = 0;
        rb = GetComponent <Rigidbody>();
        SetCountText();
        winTextObject.SetActive(false);

    }
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    void SetCountText()
    {
        countText.text = "Count " + score.ToString();
        if (score >= 4)
        {
            winTextObject.SetActive(true);
        }
    }
    
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed); 
    }

    private void Update()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown("space"))
            {
                Vector3 jump = new Vector3(0, jumpHeight, 0);
                rb.AddForce(jump);
            }
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            score += 1;
            SetCountText();
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
 
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
