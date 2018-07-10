using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO Add comments to this mess so I don't hate myself as much later
//TODO: Add cooldown to attack and shoot


public class playerMove : MonoBehaviour{
    public Rigidbody2D rb;
    public KeyCode jump;
    public KeyCode moveRight;
    public KeyCode moveLeft;
    public KeyCode attack;
    public KeyCode shoot;

    public float jumpVelocity;
    public float breakPower;
    public float movementVelocity;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    public float projectileSpeed;
    bool facingRight = true;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.3f;
    public LayerMask whatIsGround;
    public GameObject projectile;
    public GameObject wyvern;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        if (grounded){
            breakPower = 200;
        }
        else{
            breakPower = 30;
        }

        if (Input.GetKeyDown(attack)){
            Attack();
        }

        if (Input.GetKeyDown(shoot)){
            Shoot();
        }


        if (Input.GetKeyDown(jump) && grounded){
            rb.velocity = Vector2.up * jumpVelocity;
        }
        if (Input.GetKey(moveLeft)){
            Vector2 v = rb.velocity;
            v.x = movementVelocity * -1;
            rb.velocity = v;
            facingRight = false;
        }
        else if (Input.GetKey(moveRight)){
            Vector2 v = rb.velocity;
            v.x = movementVelocity;
            rb.velocity = v;
            facingRight = true;
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

    void Shoot(){
        Vector2 offset = wyvern.transform.position;
        
        GameObject clone = Instantiate(projectile, offset, transform.rotation);
        if (facingRight){
            offset.x = offset.x + 0.3f;
            clone.GetComponent<Rigidbody2D>().velocity = Vector2.right * projectileSpeed;
        }
        else if (!facingRight){
            offset.x = offset.x - 0.3f;
            clone.GetComponent<Rigidbody2D>().velocity = Vector2.left * projectileSpeed;
        }
        clone.GetComponent<projectileScript>().destroy = true;
    }

}
