using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : Unit {
    public int id = 0;
    int currentFrame;
    int currentSprite = 0;
    bool updateFrame = true;

    [Serializable]
    public class SpriteList {
        public Sprite[] sprites;
    }
    public SpriteList[] spriteList;

    public Vector2 movement;

    void Start()
    {

    }
    private void Awake()
    {
        movement = new Vector2(-.05f, 0);
    }

    void Update()
    {
        UpdateTime();
        if (updateFrame) SetTexture();

    }

    void UpdateTime()
    {
        int frameFromManager = GameManager.Instance.currentGlobalFrame;
        if (currentFrame != frameFromManager)
        {
            currentFrame = frameFromManager;
            updateFrame = true;

            if (currentSprite < spriteList[id].sprites.Length - 1)
            {
                currentSprite++;
            }
            else { currentSprite = 0; }

            if (currentFrame % 8 == 0)
            {
                Move(movement);
            }
            if (WouldIHitPlayer("Player"))
            {
                Fire(-8f);
            }

        }
    }
    void SetTexture()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = spriteList[id].sprites[currentSprite];
        //Debug.Log("SpriteList:" + spriteList[0].sprites[0]);
        updateFrame = false;
    }

    void Move(Vector2 mvmnt)
    {
        transform.position += (Vector3)mvmnt;
    }

    bool WouldIHitPlayer(string objectTag)
    {
        RaycastHit2D hit = Physics2D.Raycast(turret.transform.position, -Vector2.up);

        if (hit.collider != null)
        {
            if(hit.collider.gameObject.tag == "Player")
            {
                return true;
            }
            
        }
        
        return false;
    }

}
