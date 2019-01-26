using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonBehavior : MonoBehaviour
{
    public PlayerMovement player;
    [SerializeField] protected float speed = 9.0f;
    private Rigidbody2D lemonBody;
    private int lemonDirection;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType <PlayerMovement>();
        lemonBody = GetComponent<Rigidbody2D>();
        lemonDirection = player.facingDirection;
    }

    // Update is called once per frame
    void Update()
    {
        lemonBody.velocity = new Vector2((speed * lemonDirection), lemonBody.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
