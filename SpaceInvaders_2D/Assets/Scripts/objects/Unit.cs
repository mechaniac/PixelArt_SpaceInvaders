using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int health = 3;
    public Transform turret;

    void Start()
    {
        
    }

    void getHit()
    {
        health--;
        if(health < 1)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
        PlayDeathAnimation();
        if (this.gameObject.tag == "Player")
        {
            GameManager.Instance.OpenMainMenu();
        }
    }

    public virtual void Fire(float speed)
    {
        Projectile p = ObjectPooler.Instance.GetProjectile();
        if(p!= null)
        {
            p.transform.position = turret.transform.position;
            p.speed = speed;
            p.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        getHit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void PlayDeathAnimation()
    {
        Effect explosion = ObjectPooler.Instance.GetFromPoolerList<Effect>(ObjectPooler.Instance.explosion_01_List);
        explosion.transform.position = transform.position;
        explosion.gameObject.SetActive(true);
    }
    
}
