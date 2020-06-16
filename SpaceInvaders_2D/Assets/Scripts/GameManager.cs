using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Player player;



    public Enemy enemy;
    public Enemy[][] enemies;
    public int enemyRowLength = 10;
    public float enemyOffset = .5f;

    public int enemySideOffset = 6;

    public int enemyTypeCount = 3;

    float enemyMoveSpeed = 0.05f;

    public int enemySideTravelTime = 20;
    public int enemyDownTravelTime = 5;


    public int currentGlobalFrame;
    public int currentFrame;        //reset to 0 each second

    Vector2 lastSideDirection = new Vector2(-.1f, 0);

    public static GameManager Instance;

    MoveDirection currentMoveDirection = MoveDirection.left;
    /*
     * ----- LifeCycle Methods -----
     */

    void Start()
    {
        currentGlobalFrame = 0;
        InstantiateEnemys();
        InstantiatePlayer();

        InvokeRepeating("SetNextMoveDirectionOnEnemies", enemySideTravelTime, enemySideTravelTime+enemyDownTravelTime);

    }
    private void Awake()
    {
        Instance = this;

    }

    void Update()
    {
        UpdateFrames();


    }
    


    /*
     *  ----- Class Methods -----
     */



    void UpdateFrames()
    {
        if (currentFrame != DateTime.Now.Millisecond / 120)
        {
            currentFrame = DateTime.Now.Millisecond / 120;
            //Debug.Log("Frame: " + currentFrame);
            currentGlobalFrame++;
            //Debug.Log("global Frame: " + currentGlobalFrame);
        }
    }

    void InstantiatePlayer()
    {
        Player p = Instantiate(player);
        p.transform.position = new Vector3(0, -2.6f, 0);
    }

    void InstantiateEnemys()
    {
        enemies = new Enemy[enemyTypeCount][];

        for (int i = 0; i < enemyTypeCount; i++)
        {
            enemies[i] = new Enemy[enemyRowLength];
        }

        for (int i = 0; i < enemyTypeCount; i++)
        {
            InstantiateEnemyRow(i);
        }
    }


    void InstantiateEnemyRow(int row)
    {
        for (int i = 0; i < enemyRowLength; i++)
        {
            Enemy e = enemies[row][i] = Instantiate(enemy);
            float enemyXPosition = ((i - (enemyRowLength / 2)) * enemyOffset)+1;
            e.id = row;
            e.transform.position = new Vector3(enemyXPosition, row, 0);
        }
    }

    //void AnimateEnemies()
    //{
    //    Action<Enemy> action = new Action<Enemy>(MoveEnemy);

    //    for (int i = 0; i < enemyTypeCount; i++)
    //    {
    //        Array.ForEach(enemies[i], MoveEnemy);
    //    }

    //}

    //void MoveEnemy(Enemy enemy)
    //{
    //    enemy.transform.position += (Vector3)enemy.movement;
    //}

    void SetEnemyMoveDirection(Vector2 m)
    {
        for (int i = 0; i < enemyTypeCount; i++)
        {
            Array.ForEach(enemies[i], (e) => e.movement = m);
        }
    }

    void SetNextMoveDirection()
    {
        int moveDirectionCount = Enum.GetNames(typeof(MoveDirection)).Length;
        if (currentMoveDirection < (MoveDirection)moveDirectionCount-1)
        {
            currentMoveDirection = currentMoveDirection + 1;
        }
        else
        {
            currentMoveDirection = 0;
        }
        
    }


    Vector2 ReturnMoveDirectionVector()
    {
        Vector2 mD = new Vector2(enemyMoveSpeed, 0);

        if (currentMoveDirection == MoveDirection.left)
        {
            return mD * -1;
        }
        else if (currentMoveDirection == MoveDirection.leftDown || currentMoveDirection == MoveDirection.rightDown)
        {
            return new Vector2(0, -enemyMoveSpeed);
        } 

        return mD;
    }

    void SetNextMoveDirectionOnEnemies()
    {
        //Debug.Log("current MoveDirection: " + currentMoveDirection);
        SetNextMoveDirection();
        Vector2 mD = ReturnMoveDirectionVector();
        //Debug.Log("myVector: " + mD);
        SetEnemyMoveDirection(mD);
        Debug.Log("set MoveDirection to: " + currentMoveDirection);
        StartCoroutine(SetMoveDirectionForward());
    }

    IEnumerator SetMoveDirectionForward()
    {
        yield return new WaitForSeconds(enemyDownTravelTime);

        Debug.Log("fromCoroutine");
        SetNextMoveDirection();
        SetEnemyMoveDirection(ReturnMoveDirectionVector());
    }
}
enum MoveDirection {
    left,
    leftDown,
    right,
    rightDown
}
