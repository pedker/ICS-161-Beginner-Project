using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonBehavior : MonoBehaviour
{
    public GameObject player = null;
    [SerializeField] protected float lifeTime = 0.5f;
    private float totalLife = 0.0f;
    [SerializeField] protected float speed = 9.0f;
    private float lemonDirection;
    private Rigidbody2D lemonBody;

    // Start is called before the first frame update
    void Start()
    {
        lemonDirection = player.GetComponent<PlayerMovement>().direction;
        lemonBody = GetComponent<Rigidbody2D>();
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
