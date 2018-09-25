using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float movementSpeed = 7.0f;
    public float jumpForceInitial = 3.0f;
    public float jumpForce = 5.0f;
    public float jumpDuration = 1.0f;

    private bool grounded = true;
    private float jumpStartTime;//when the jump last started

    Rigidbody2D rb2d;
    Animator anim;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //Move Left and Right
        float horizontal = Input.GetAxis("Horizontal");
        float speed = horizontal * movementSpeed;
        anim.SetFloat("movementSpeed", Mathf.Abs(speed));
        //Flip
        if (Mathf.Sign(transform.localScale.x) != Mathf.Sign(horizontal)
            && !Mathf.Approximately(horizontal, 0))
        {
            Flip(horizontal);
        }
        //Jump
        float vertical = Input.GetAxis("Vertical");
        float jump = 0;
        if (Input.GetButton("Jump"))
        {
            if (jumpStartTime == 0)
            {
                jumpStartTime = Time.time;
                grounded = false;
                jump = jumpForceInitial;
            }
            else if (Time.time < jumpStartTime + jumpDuration)
            {
                jump = jumpForce;
            }
        }
        anim.SetBool("isJumping", jump > 0 || !grounded);
        //Apply all factors to moving
        rb2d.velocity = (Vector2.right * speed) + (Vector2.up * (jump + rb2d.velocity.y));
        //Check ESC
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void Flip(float direction)
    {
        transform.localScale = new Vector3(Mathf.Sign(direction), transform.localScale.y, transform.localScale.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;
        jumpStartTime = 0;
    }
}
