using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderColorChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cube")
        {
            this.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        this.GetComponent<Renderer>().material.color = Color.white;
    }
}
