using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour {
    public int health = 2;

    void Start()
    {

    }


    void Update()
    {

    }

    /*
     *  --------- Class Methods --------- 
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetHit();
    }

    void GetHit()
    {
        health--;
        if (health <= 0) Die();
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}
