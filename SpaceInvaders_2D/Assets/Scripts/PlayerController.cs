using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), 0f) * speed;

        Vector2 velocity = rb.velocity;

        velocity.x = Mathf.MoveTowards(velocity.x, moveInput.x, 3f);

        rb.velocity = velocity;
    }
}
