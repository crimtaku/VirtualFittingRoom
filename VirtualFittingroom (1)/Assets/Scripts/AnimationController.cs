using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("1"))
        {
            animator.SetInteger("animationNumber", 1);
        }
        if (Input.GetKey("2"))
        {
            animator.SetInteger("animationNumber", 2);
        }
        if (Input.GetKey("0"))
        {
            animator.SetInteger("animationNumber", 0);
        }
    }
}
