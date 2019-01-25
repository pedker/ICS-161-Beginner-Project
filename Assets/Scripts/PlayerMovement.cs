using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 4.5f;
    [SerializeField] protected float jumpForce = 7.5f;
    public float direction;
    private bool grounded = false;
    private Rigidbody2D rigidBodyObject;


    // Start is called before the first frame update
    void Start()
    {
        rigidBodyObject = this.GetComponent<Rigidbody2D>();
        QualitySettings.vSyncCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Jump();
    }

    void MovePlayer()
    {
        // get the player input
        direction = Input.GetAxisRaw("Horizontal");

        // correctly align the sprite
        if (direction < 0 && !GetComponent<SpriteRenderer>().flipX)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (direction > 0 && GetComponent<SpriteRenderer>().flipX)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        // move
        rigidBodyObject.velocity = new Vector2( (moveSpeed * direction), rigidBodyObject.velocity.y );

    }

    void Jump()
    {


        // check for a spacebar press and grounded state
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            // jump :)
            rigidBodyObject.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            grounded = false;
        }
    }


    void OnCollisionEnter2D(Collision2D otherObject)
    {
        // if the player collides with a ground-tagged object, set grounded to true
        if (otherObject.collider.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
}
