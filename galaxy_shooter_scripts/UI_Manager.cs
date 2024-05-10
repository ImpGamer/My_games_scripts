using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    private Player player;
    public Sprite[] livesSprites = new Sprite[4];
    public Image livesImagesDisplay;
    public int score=0;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private TextMeshProUGUI deathText;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.isAlive())
        {
            StartCoroutine(backMenu());
        }
    }
    public void updateLives(int currentLives)
    {
        //Atributo Sprite es el que le da el "source Image" o la imagen.
        livesImagesDisplay.sprite = livesSprites[currentLives];
    }
    public void updateScore()
    {
        score += 5;
        scoreText.text = "Puntaje: " + this.score;
    }
    IEnumerator backMenu()
    {
        deathText.text = "Has Muerto...";
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Menu");
    }
}
