using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("TurnLeft",true);
            anim.SetBool("TurnRight", false);
        } else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("TurnRight", true);
            anim.SetBool("TurnLeft", false);
        } else
        {
        anim.SetBool("TurnRight", false);
        anim.SetBool("TurnLeft", false);
        }

    }
}
