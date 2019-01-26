using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 4.5f;
    [SerializeField] protected float jumpForce = 7.5f;
    [SerializeField] protected int score = 0;
    public float direction;
    public int facingDirection;
    private bool grounded = false;
    private Rigidbody2D rigidBodyObject;
    public Transform transformObject;
    public GameObject lemon;
    private Vector2 lemonPlacement;



    // Start is called before the first frame update
    void Start()
    {
        rigidBodyObject = this.GetComponent<Rigidbody2D>();
        transformObject = this.GetComponent<Transform>();
        QualitySettings.vSyncCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Shoot();
        Jump();
    }

    void MovePlayer()
    {
        // get the player input
        direction = Input.GetAxisRaw("Horizontal");

        // correctly align the sprite
        if (direction < 0)
        {
            facingDirection = -1;
            transformObject.localScale = new Vector3(-1,1,1);
        }
        else if (direction > 0)
        {
            facingDirection = 1;
            transformObject.localScale = new Vector3(1, 1, 1);
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

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            lemonPlacement = this.transform.position + new Vector3(0.8f * transformObject.localScale.x, 0);
            Instantiate(lemon, lemonPlacement, Quaternion.identity);
        }
    }


    void OnCollisionEnter2D(Collision2D otherObject)
    {
        // if the player collides with a ground-tagged object, set grounded to true
        if (otherObject.collider.CompareTag("Ground"))
        {
            grounded = true;
        }
        // die if touching enemy
        else if (otherObject.collider.CompareTag("Enemy"))
        {
            Debug.Log("The player died. Re-loading scene...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ETank"))
        {
            score++;
            Debug.Log("Picked up an ETank. Score is now " + score);
            Destroy(other.gameObject);
        }
    }
}
