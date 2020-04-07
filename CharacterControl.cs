using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [Header("移动相关")]
    private Vehicle m_vehicle;

    [Header("人物动画")]
    private Animator anim;

    void Start()
    {
        m_vehicle = GetComponent<AILocomotion>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_vehicle.velocity.magnitude != 0)
        {
            anim.SetBool("toWalk", true);
        }
        else
            anim.SetBool("toWalk", false);
    }
}
