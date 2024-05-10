using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject[] powerUps = new GameObject[3];
    Player player;
    private UI_Manager ui_manager;
    private float enemySpawn=5f;
    private 
    // Start is called before the first frame update
    void Start()
    {
        ui_manager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        player = FindObjectOfType<Player>();
        StartCoroutine(generateEnemy());
        StartCoroutine(generatePowerUps());
        StartCoroutine(cleanExplosivesEnemies());
    }
    private void Update()
    {
        setDifficult(ui_manager.score);
    }
    //Crear corrutina que genere un nuevo enemigo cada determinado tiempo
    IEnumerator generateEnemy()
    {
        
        while(player.isAlive())
        {
            Instantiate(enemyShipPrefab, new Vector3(Random.Range(-8f, 8f), 7f, 0), Quaternion.identity);
            yield return new WaitForSeconds(enemySpawn);
        }
    }
    IEnumerator generatePowerUps()
    {
        while(player.isAlive())
        {
            int cooldownSpawn = Random.Range(10, 25);
            short randomPowerUp = (short)Random.Range(0f, 3f);
            GameObject powerUp = powerUps[randomPowerUp];

            Instantiate(powerUp, new Vector3(Random.Range(-8f, 8f), 8f, 0), powerUp.transform.rotation);
            yield return new WaitForSeconds(cooldownSpawn);
        }
    }
    IEnumerator cleanExplosivesEnemies()
    {
        while (true)
        {
            // Busca todos los objetos con el tag "enemy_explosion" en la escena
            GameObject[] enemies_exploded = GameObject.FindGameObjectsWithTag("enemy_explosion");

            // Destruye cada uno de los objetos encontrados
            foreach (GameObject enemy in enemies_exploded)
            {
                Destroy(enemy);
            }

            // Espera 10 segundos antes de volver a buscar y limpiar los objetos
            yield return new WaitForSeconds(15);
        }
    }
    void setDifficult(int score)
    {
        switch(score)
        {
            //Dificultad medio
            case 45:
                Debug.Log("Dificultad Media");
                enemySpawn = 2; break;
            //Dificultad Dificil
            case 100:
                Debug.Log("Dificultad Dificil");
                enemySpawn = 1.6f; break;
            //Dificultad Imposible
            case 250:
                Debug.Log("Dificultad Imposible");
                enemySpawn = 1f; break;
        }
    }

}
