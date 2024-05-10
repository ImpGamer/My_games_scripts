using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private AudioClip efectoAgarrar;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        if(transform.position.y <= -5.9)
        {
            Destroy(this.gameObject);
        }
    }
    //Funcion que se activa cuando se entra en contacto con una collision (Trigger) con otro objeto
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            string objectTag = this.gameObject.tag;
            Debug.Log(objectTag);
            Player player = other.GetComponent<Player>();
            switch(objectTag)
            {
                case "triple_shoot":
                    player.powerUps[0] = true; break;
                case "shield_powerUp":
                    player.shieldGameObject.SetActive(true);
                    player.powerUps[1] = true; break;
                case "speed_boost":
                    player.powerUps[2] = true;break;
            }
            AudioSource.PlayClipAtPoint(efectoAgarrar, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
    }
}
