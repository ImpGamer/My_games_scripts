using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject _tripleShootPrefab;
    [SerializeField]
    private float speed = 6f;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    public GameObject shieldGameObject;

    private float fireRate = 0.25f;
    private float nextFire = 0.0f;
    //public bool tripleShoot = false;
    /* PowerUps Array Position:
     Pos 1: triple_shoot
     Pos 2: Shield
     Pos 3: Speed Boost
     */
    public bool[] powerUps = new bool[3];
    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;
    public int lifes = 3;
    private UI_Manager ui_manager;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip efectoExplosion;
    [SerializeField]
    private AudioClip efectoDisparo;
    [SerializeField]
    private GameObject[] roturasAlas = new GameObject[2];

    void Start()
    {
        transform.position = new Vector3(0, -3, 0);
        ui_manager = GameObject.FindObjectOfType<UI_Manager>();
        if(ui_manager != null)
        {
            ui_manager.updateLives(this.lifes);
        }
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerOne && Input.GetKey(KeyCode.Space))
        {
            setModeShoot();
        }
        if(isPlayerTwo && Input.GetMouseButton(0))
        {
            setModeShoot();
        }
        if (isPlayerOne && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            movePlayerOne();
        }

        if (isPlayerTwo && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            movePlayerTwo();
        }

        setSpeed();
        death();
    }

    //FUNCIONES DE MOVIMIENTO Y LIMITACION
    void movePlayerOne()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        transform.Translate(Vector3.up * Time.deltaTime * speed * verticalInput);

        // Restricciones de movimiento
        restrictMovement();
    }

    void movePlayerTwo()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        transform.Translate(Vector3.up * Time.deltaTime * speed * verticalInput);

        // Restricciones de movimiento
        restrictMovement();
    }
    void restrictMovement()
    {
        // Restricciones horizontales
        if (transform.position.x <= -9.7)
        {
            transform.position = new Vector3((float)8.9, transform.position.y, 0);
        }
        else if (transform.position.x >= 9.7)
        {
            transform.position = new Vector3((float)-8.9, transform.position.y, 0);
        }

        // Restricciones verticales
        if (transform.position.y > 0.5)
        {
            transform.position = new Vector3(transform.position.x, (float)0.5, 0);
        }
        else if (transform.position.y < -4)
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }
    }
    void normal_shoot()
    {
        if(Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            //Sistema de coolDown para los disparos
            if(Time.time > nextFire)
            {
                audioSource.PlayOneShot(efectoDisparo);
                nextFire = Time.time + fireRate;
            //Le indicamos que objeto instanciara y cual sera su posicion, ademas de indicarle si abra o no rotacion, en este caso sera la que tiene por default el objeto
                Instantiate(laserPrefab, new Vector3(transform.position.x, (float)(transform.position.y + 1.3), 0), Quaternion.identity);
            } 
        }
    }
   
    void triple_shoot()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            //Sistema de coolDown para los disparos
            if (Time.time > nextFire)
            {
                audioSource.PlayOneShot(efectoDisparo);
                nextFire = Time.time + fireRate;
                Instantiate(_tripleShootPrefab,new Vector3(transform.position.x + 1.1f,transform.position.y + 3f), Quaternion.identity);
            }
        }
    }
    void setModeShoot()
    {
        if (powerUps[0] == true)
        {
            triple_shoot();
            StartCoroutine(powerUp_cooldown(0));
        } else
        {
            normal_shoot();
        }
    }
    void setSpeed()
    {
        if (powerUps[2] == true)
        {
            speed = 12f;
            StartCoroutine(powerUp_cooldown(2));
        } else
        {
            speed = 6f;
        }
    }
    /* 
     Sistema de cooldown para las habilidades, una vez pasado los 10 segundos agarrado el powerUp o que la posicion del array se encuentre en true, esperara 10 segundos
    para volver a setearlo en false y que se quite el PowerUp al jugador
     */
    IEnumerator powerUp_cooldown(int power_num)
    {
        yield return new WaitForSeconds(12);
        powerUps[power_num] = false;
    }
    void death()
    {
        if(lifes == 0)
        {
            //Funcion .PlayClipAtPoint() permite reproducir un audio en una posicion de nuestro espacio aunque el objeto se destruya este se reproducira
            //Esto se hace debido a que si ejecutamos el .PlayOneShot() original, no se reproducira ya que en este momento estamos destruyendo nuestro objeto
            //por lo que el objeto muerto ya no puede reproducir su audio
            AudioSource.PlayClipAtPoint(efectoExplosion, Camera.main.transform.position);
            Instantiate(explosionPrefab, transform.position,Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    public bool isAlive()
    {
        return lifes > 0;
    }
    public void getDamage(Collider2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (powerUps[1])
        {
            shieldGameObject.SetActive(false);
            enemy.life = 0;
            powerUps[1] = false;
            return;
        }
        setSpriteDamage();
        lifes--;
        enemy.life = 0;
        ui_manager.updateLives(this.lifes);
        Debug.Log("Vidas restantes: " + this.lifes);
    }
    void setSpriteDamage()
    {
        int sprite = Random.Range(0, 2);
        if (roturasAlas[0].activeSelf)
        {
            sprite = 1;
        } else if (roturasAlas[1].activeSelf)
        {
            sprite = 0;
        }

        roturasAlas[sprite].SetActive(true);
    }
}