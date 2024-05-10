using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_manager : MonoBehaviour
{
    public void LoadSinglePlayer()
    {
        Debug.Log("Cargando nivel 'Un Jugador'");
        SceneManager.LoadScene("Game");
    }
    public void LoadCo_OpGame()
    {
        Debug.Log("Cargando Nivel Cooperativo...");
        SceneManager.LoadScene("Co_Op");
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("El juego a cerrado");
    }
}