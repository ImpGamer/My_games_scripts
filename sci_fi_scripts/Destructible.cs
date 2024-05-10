using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField]
    private GameObject boxDestroy;
    
    public void DestroyBox()
    {
        Instantiate(boxDestroy, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }
}
