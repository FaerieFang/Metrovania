using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour{
    public Rigidbody2D rb;
    public KeyCode jump;
    public KeyCode moveRight;
    public KeyCode moveLeft;
    public KeyCode attack;

    public float jumpVelocity;
    public float breakPower;
    public float movementVelocity;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.3f;
    public LayerMask whatIsGround;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        if (grounded){
            breakPower = 500;
        }
        else{
            breakPower = 100;
        }

        if (Input.GetKeyDown(attack)){
            Attack();
        }


        if (Input.GetKeyDown(jump) && grounded){
            rb.velocity = Vector2.up * jumpVelocity;
        }
        if (Input.GetKey(moveLeft)){
            Vector2 v = rb.velocity;
            v.x = movementVelocity * -1;
            rb.velocity = v;
        }
        else if (Input.GetKey(moveRight)){
            Vector2 v = rb.velocity;
            v.x = movementVelocity;
            rb.velocity = v;
        }
        else{
            Vector2 v2 = -rb.velocity;
            v2.x = v2.x * breakPower;
            rb.AddForce(v2 * Time.deltaTime);
        }


        if (rb.velocity.y < 0){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(jump)){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

    }

    void Attack(){

    }

}
