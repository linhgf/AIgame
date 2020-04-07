using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody rb;
    private int move;

    [Header("Move")]
    public float speed;

    [Header("Jump")]
    public float jumpForce;
    private bool jumpPressed;

    [Header("Fire")]
    private bool firePressed;
    public GameObject bullets;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Input.GetAxisRaw("Horizontal")*Time.deltaTime*speed, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime*speed);
        if (Input.GetButtonDown("Jump"))
            jumpPressed = true;
        if (Input.GetButtonDown("Fire1"))
            firePressed = true;

    }

    private void FixedUpdate()
    {
        Jump();
        Movement();
        Fire();
    }

    private void Movement()
    {
        rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y, Input.GetAxisRaw("Vertical")  * speed);
    }

    private void Jump()
    {
        if (jumpPressed == true)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            jumpPressed = false;
        }
    }

    private void Fire()
    {
        if (firePressed)
        {
            Instantiate(bullets,transform.position,transform.rotation);
            firePressed = false;
        }
    }
}
