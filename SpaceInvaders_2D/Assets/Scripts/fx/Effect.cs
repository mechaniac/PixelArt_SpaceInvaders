using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour {
    [SerializeField]
    Sprite[] frameArray;

    float timer = 0f;
    public float animationSpeed = .5f;
    int currentFrame = 0;


    void Start()
    {

    }
    private void OnEnable()
    {
        timer = 0f;
        currentFrame = 0;
        GetComponentInChildren<SpriteRenderer>().sprite = frameArray[0];
    }
    

    // Update is called once per frame
    void Update()
    {
        PlayAnimation();
    }

    void PlayAnimation()
    {
        timer += Time.deltaTime;

        if (timer >= animationSpeed)
        {
            timer -= animationSpeed;
            currentFrame = currentFrame + 1;
            if (currentFrame >= frameArray.Length)
            {
                gameObject.SetActive(false);
            }
            else
            {
                GetComponentInChildren<SpriteRenderer>().sprite = frameArray[currentFrame];
            }
            
            

        }
    }
}
