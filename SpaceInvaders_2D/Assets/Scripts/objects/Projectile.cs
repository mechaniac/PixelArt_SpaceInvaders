using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10f;
    int frameBorn;
    int framesToLive = 20;

    private void OnEnable()
    {
        frameBorn = GameManager.Instance.currentGlobalFrame;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb.velocity = new Vector2(0, 1) * speed;
    }
    private void FixedUpdate()
    {
        if(GameManager.Instance.currentGlobalFrame - frameBorn > framesToLive)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("projectile hit");
        Effect explosion = ObjectPooler.Instance.GetFromPoolerList<Effect>(ObjectPooler.Instance.hit_01_List);
        explosion.transform.position = transform.position + new Vector3(0,.1f,0);
        explosion.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
