using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCode : MonoBehaviour
{
    //prefabs
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;
    public GameObject prefab4;
    public GameObject prefab5;
    public GameObject prefab6;

    //variables code
    private float timer;
    private float MaxTime;

    int chooseObstacle;

    // Start is called before the first frame update
    void Start()
    {
        MaxTime = 1f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= MaxTime)
        {
            generateObstacle();
            randomMaxTime();
            timer = 0;

        }


    }

    void randomMaxTime()
    {
        MaxTime = Random.Range(1, 3);
    }

    void generateObstacle()
    {
        chooseObstacle = Random.Range(1, 9);
        if (chooseObstacle == 1) { Instantiate(prefab1); }
        if (chooseObstacle == 2) { Instantiate(prefab2); }
        if (chooseObstacle == 3) { Instantiate(prefab3); }
        if (chooseObstacle == 4) { Instantiate(prefab4); }
        if (chooseObstacle == 5) { Instantiate(prefab5); }
        if (chooseObstacle == 6) { Instantiate(prefab1); }
        if (chooseObstacle == 7) { Instantiate(prefab2); }
        if (chooseObstacle == 8) { Instantiate(prefab3); }
        if (chooseObstacle == 9) { Instantiate(prefab5); }

    }
}
