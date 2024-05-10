using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private UI_Manager ui_manager;
    private Player player;
    [SerializeField]
    private AudioClip coinFX; 
    private void Start()
    {
        ui_manager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            ui_manager.setPopUpVisible(true);
            if (Input.GetKey(KeyCode.E)) {
                AudioSource.PlayClipAtPoint(coinFX,transform.position);
                player.setCoin(true);
                ui_manager.setPopUpVisible(false);
                Destroy(this.gameObject);
            }
        } else
        {
            ui_manager.setPopUpVisible(false);
        }
    }
}
