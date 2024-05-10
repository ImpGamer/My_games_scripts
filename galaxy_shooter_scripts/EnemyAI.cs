using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.7f;
    private float randomX;
    public int life = 3;
    [SerializeField]
    private GameObject explosionPrefab;
    private UI_Manager ui_manager;
    [SerializeField]
    private AudioClip efectoExplosion;
    
    /*
     * Mover al enemigo hacia abajo, si el enemigo llega a una posicion 'y' fuera de la pantalla volver a reposicionarlo arriba para que salga como
     * "nuevo enemigo" pero sera el mismo randomizando su posicion 'x' para que no salga en la misma posicion anterior.
     */
    private void Start()
    {
        ui_manager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        randomX = (Random.value * 16.5f) - 8;
        transform.position = new Vector3(randomX, 7f);
    }


    void Update()
    {
        //Moverlo hacia abajo
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        repositionEnemy();
        isdeath();
    }
    void repositionEnemy()
    {
        if(transform.position.y <= -6.0f)
        {
            //Entre -8 y 8
            randomX = (Random.value * 16.5f) - 8;
            transform.position = new Vector3(randomX, 7f);
        }
       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            Collider2D myCollider = this.gameObject.GetComponent<Collider2D>();

            player.getDamage(myCollider);
        }
    }
    void isdeath()
    {
        if (life <= 0)
        {
            //Funcion .PlayClipAtPoint() permite reproducir un audio en una posicion de nuestro espacio aunque el objeto se destruya este se reproducira
            AudioSource.PlayClipAtPoint(efectoExplosion,Camera.main.transform.position);
            ui_manager.updateScore();
            Instantiate(explosionPrefab,transform.position,Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    
}
