using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    [SerializeField]
    private float sensibility = 1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lookHorizontal();
    }
    void lookHorizontal()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + (mouseX * sensibility), transform.localEulerAngles.z);
    }
    
}