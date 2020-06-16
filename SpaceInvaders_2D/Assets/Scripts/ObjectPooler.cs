using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;


    private List<Projectile> projectile_01_List;
    public Projectile p1;
    public int projectile_01_Count = 40;

    public List<Effect> explosion_01_List;
    public Effect expl1;
    public int explosion_01_Count = 10;

    public List<Effect> hit_01_List;
    public Effect hit1;
    public int hit_01_Count;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        projectile_01_List = new List<Projectile>();
        InitializeProjectiles();

        explosion_01_List = new List<Effect>();
        InitializePool<Effect>(explosion_01_Count, expl1, explosion_01_List);

        hit_01_List = new List<Effect>();
        InitializePool<Effect>(hit_01_Count, hit1, hit_01_List);
    }

    
    void Update()
    {
        
    }

    private void InitializeProjectiles()
    {
        for (int i = 0; i < projectile_01_Count; i++)
        {
            Projectile p =  Instantiate(p1);
            p.gameObject.SetActive(false);
            projectile_01_List.Add(p);
            p.transform.parent = transform;
        }
    }

    private void InitializePool<T>(int poolCount, T toInstantiate, List<T> list) where T : MonoBehaviour
    {
        
        for (int i = 0; i < poolCount; i++)
        {
            T o = Instantiate(toInstantiate);
            o.gameObject.SetActive(false);
            list.Add(o);
            o.transform.parent = transform;
        }
    }

    public T GetFromPoolerList<T>(List<T> inputList) where T : MonoBehaviour
    {
        for (int i = 0; i < inputList.Count; i++)
        {
            if (!inputList[i].gameObject.activeInHierarchy)
            {
                return inputList[i];
            }
        }
        return null;
        
    }

    public Projectile GetProjectile()
    {
        for (int i = 0; i < projectile_01_List.Count; i++)
        {
            if (!projectile_01_List[i].gameObject.activeInHierarchy)
            {
                return projectile_01_List[i];
            }
        }
        return null;
    }
}
