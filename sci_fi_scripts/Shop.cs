using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private AudioClip buyWeaponSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("El " + other.name + " esta en la tienda");
            if(Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();
                if(player != null && player.isCoin())
                {
                    player.setCoin(false);
                    player.setWeapon(true);
                    AudioSource.PlayClipAtPoint(buyWeaponSound,Camera.main.transform.position);
                }
            }
        }
    }
}
