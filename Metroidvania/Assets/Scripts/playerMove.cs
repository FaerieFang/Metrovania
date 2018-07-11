using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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
    public float attackCD;
    public float shootCD;
    float shootCDValue;
    float attackCDValue;

    bool facingRight = true;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    public GameObject projectile;
    public GameObject wyvern;
    void Start(){
        rb = GetComponent<Rigidbody2D>();

    }

    void Update(){
        //Check if grounded and set breaking power
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if (grounded){
            breakPower = 200;
        }
        else{
            breakPower = 30;
        }

        //attack inputs
        if (Input.GetKeyDown(attack)){
            Attack();
        }

        if (Input.GetKeyDown(shoot)){
            Shoot();
        }

        //jump and movement
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
        //Shoots at position of wyvern, offset to be front of wyvern
        if (shootCDValue <= 0){
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
            //enable destruction on projectile
            clone.GetComponent<projectileScript>().destroy = true;
            //set a cooldown, measured in tenth of seconds
            shootCDValue = shootCD;
            StartCoroutine("ShootCoolDown");
        }

    }
    //cooldown coroutines 
    IEnumerator ShootCoolDown(){
        while (shootCDValue > 0){
            yield return new WaitForSeconds(0.1f);
            shootCDValue -= 1;
        }
    }
    IEnumerator AttackCoolDown(){
        while (attackCDValue > 0){
            yield return new WaitForSeconds(0.1f);
            attackCDValue -= 1;
        }
    }

}
