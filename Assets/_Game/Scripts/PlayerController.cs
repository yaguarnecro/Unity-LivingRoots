using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Comment steps

    #region 1,the rigidbody
    //1a, create variable to hold a rigid body
    //1b, Assing rigid body component on the variable when scene starts
    //1c, create var movemetn to hold player input
    //1d, asing player input(horiz Y) to the variable
    //1e, create speed var 
    //1f, take rb attribute velocity and repplace it with a new
    //    Vector2 (actual place * speed so it moves on X, actual horizontal value Y)
    #endregion
    #region 2, fix the movement in 2d
    //2 move face/image direction since is moving in a 2d vector
    //2a, create flip function
    #region 2dfliping image scale, maybe not the same that a 3d object
    //2a1, create rightflip bool var, lookin to the right
    //2a2, change value of the fipbool 
    //2a3, create variable to hold transfomlocal scale
    //2a4, change var value transform.localscale to its negative value
    //2a5, assign new value to transform.localScale
    #endregion
    //2b, create if and if else to apply Flip function
    #region 3 jumping
    //3a, create bool var to know if is in the ground, bool var to know if is in the air
    //3b, create float vars for values like, foze jump, gravity scale, fallgravity scale
    //3c, apply logic if is posible to jump, if is not possible
    #region 4 reactivate jumping when touching tag ground
    //4,1
    #endregion

    #endregion



    #endregion
    #endregion

    [Header("Movement vars")]

    //1e
    public float speed;
    //1c,
    float movement;

    [Header("Jump vars")]

    //3a
    bool isGround= true;
    bool isJumping;
    //3b
    public float jumpH;
    public float gravityScale;
    public float fallGravityScale;

    public Transform groundRayCast;

    //1a,
    Rigidbody2D rb;
    //2a1
    bool rightFlip = true;

    public LayerMask maskVar;
    


    void Start()
    {
        Debug.Log("Hola mundo");      
        //1b, 
        rb = GetComponent<Rigidbody2D>();


    }

    void Update()
    {
        //xxxx  should this be outside update and in a flixedupdate? both privates ?
        //1d,
        movement = Input.GetAxis("Horizontal");
        //1f
        rb.velocity = new Vector2(movement * speed, rb.velocity.y);
        //2b
        if (rightFlip && rb.velocity.x < 0) Flip();
        else if (!rightFlip && rb.velocity.x > 0) Flip();
        //xxxx

        //3c
        if (isGround && Input.GetButtonDown("Jump"))
        {
            rb.gravityScale = gravityScale;

            float jumpF = Mathf.Sqrt(jumpH * (Physics2D.gravity.y * rb.gravityScale) * -2) * rb.mass;
            rb.AddForce(Vector2.up * jumpF, ForceMode2D.Impulse);

            //isGround = false;
            isJumping = true;
        }
        if (isJumping)
        {
            if (Input.GetButtonUp("Jump"))
            {
                rb.gravityScale = fallGravityScale;
            }
            if(rb.velocity.y < 0)
            {
                rb.gravityScale = fallGravityScale;
                isJumping = false;
            }
        }

        CheckGround();
         
    }
    //2a
    void Flip()
    {
        //2a2
        rightFlip= !rightFlip;
        //2a3
        Vector3 Scalar = transform.localScale;
        //2a4
        Scalar.x *= -1;
        //2a5
        transform.localScale= Scalar;
    }

    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundRayCast.position, Vector2.down, 0.2f, maskVar);

        if (hit.collider != null)
        {
            isGround = true;
            //Debug.Log("has colided 0.0");
        }
        else
        {
            isGround = false;
        }

    }



    //4a
   // private void OnCollisionEnter2D(Collision2D collision)
  //  {
  //      if (collision.gameObject.CompareTag("Ground"))
  //      {
  //          isGround = true;
  //          Debug.Log("has colided 0.0");
  //      }
 //  }
}