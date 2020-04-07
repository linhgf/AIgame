using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    private float speed = 0.25f;
    private float startTime;
    private float endTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        if (Time.time - startTime < endTime)
        {
            transform.Translate(Vector3.forward * speed);
        }
        else
            Destroy(gameObject);

    }
}
