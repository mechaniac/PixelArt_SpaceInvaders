using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public Sprite[] sprites;

    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire(7f);
            SetTexture(1);
            StartCoroutine(SetTextureBack(.2f));
            
        }
    }

    void SetTexture(int spriteId)
    {
        GetComponentInChildren<SpriteRenderer>().sprite = sprites[spriteId];

    }

    IEnumerator SetTextureBack(float time)
    {
        yield return new WaitForSeconds(time);
        //Debug.Log("coroutine");
        SetTexture(0);
    }
}
