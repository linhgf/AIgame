using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBotsForQueue : MonoBehaviour
{
    public GameObject botPrefab;
    public int botCount;
    public Transform target;
    public float minX = 75f;
    public float maxX = 160.0f;
    public float minZ = -650f;
    public float maxZ = -600.0f;
    //public float Yvalue = 4.043714f;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPosition;
        GameObject bot;
        for (int i = 0; i < botCount; i++)
        {
            //在一个指定大小区域，随机选取位置产生物体
            spawnPosition = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
            spawnPosition += transform.position;
            bot = Instantiate(botPrefab, spawnPosition, Quaternion.identity);
            bot.GetComponent<SteeringForArrive>().target = target;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
