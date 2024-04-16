using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RouteSpawner routeSpawner;             // Variable referencia al RouteSpawner routeSpawner
    [SerializeField] private PlayerController playerController;     // Variable referencia al PlayerController playerController

    private int cantidadDesafios = 0;       // Variable referencia a la cantidad de desaf�os del juego
    private int currentDesafioIndex = 0;    // Variable referencia a el �ndice del desaf�o actual
    private bool gameOver = false;          // Variable referencia a la bandera que controla el gameOver (perdiste)
    private bool gameWin = false;           // Variable referencia a la bandera que controla el gameWin (ganaste)
    private bool isPaused = false;          // Variable referencia a la bandera que controla si el juego est� pausado o no
    private float speed = 0;                // Variable referencia a la velocidad de player segun el desaf�o

    private void Start()
    {
        // Inicializa la variable cantidadDesafios con la cantidad de desaf�os
        cantidadDesafios = routeSpawner.GetCantidadDesafios();

        // Inicializa la variable cantidadDesafioIndex con el n�mero de desaf�o actual
        currentDesafioIndex = routeSpawner.GetCurrentDesafioIndex();
    }

    private void Update()
    {
        // Llama al m�todo que gestiona como est� el juego
        GameStatus();
        
    }

    // Gestiona el estado del jeugo
    public void GameStatus()
    {
        // Actualiza la variable currentDesafioIndex con el n�mero de desaf�o actual 
        currentDesafioIndex = routeSpawner.GetCurrentDesafioIndex();

        // Si pierdes o ganas = true => se llama al m�todo PauseGame() para pausar el juego
        if (gameOver == true || gameWin == true)
        {
            PauseGame();

        }
        //Debug.Log("Desaf�o n� GM" + currentDesafioIndex + " Cantidad de Desaf�os GM" + cantidadDesafios);
        
    }

    // Gestiona la pausa del jeugo
    public void PauseGame()
    {
        // cambia la bandera de juego pausado y detiene el juego
        isPaused = true;
        Time.timeScale = 0f;
    }

    // Gestiona el reinicio del juego
    public void ResumeGame()
    {
        // cambia la bandera de juego pausado y restaurar el tiempo normal
        isPaused = false;
        Time.timeScale = 1f; 
    }

    // Devuelve la bandera de pausa
    public bool IsPaused()
    {
        return isPaused;
    }

    // Devuelve a cantidad de desaf�os que tiene el juego
    public int GetCantidadDesafios()
    {
        return cantidadDesafios;
    }

    // Actualiza la cantidad de desaf�os que hay en el juego
    public void SetCantidadDesafios(int _cantidadDesafios)
    {
        cantidadDesafios = _cantidadDesafios; 
    }

    // Devuelve el n�emo de desaf�o actual (este dato lo toma UI)
    public int GetCurrentDesafioIndex() 
    {
       
        return currentDesafioIndex;
    }

    // Actualiza el n�emro de desaf�os actual
    public void SetCurrentDesafioIndex(int _currentDesafioIndex)
    {
        currentDesafioIndex = _currentDesafioIndex; 
    }

    // Devuelve el valor de la velocidad del player (este dato lo toma UI)
    public float GetSpeed()
    {
        return speed;
    }
    
    // Actualiza el valor de la velocidad del player
    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    // Gestiona el reinicio del juego
    public void Reinicia()
    {
        // Reinicia variables
        currentDesafioIndex = 0;
        gameOver = false;
        gameWin = false;
        speed = 0;

        // Reinicia el RouteSpawner
        routeSpawner.InitRouterSpawner();

        // Reinicia el PlayerController
        playerController.InitPlayerController();

        // Reactiva el tiempo y cambia la bandera de pausa
        ResumeGame();
    }

    // Devuelve el valor de la bandea gameOver
    public bool GetIsGameOver()
    {
        return gameOver;
    }

    // Actualiza el valor de la bandera gameOver
    public void SetIsGameOver(bool _isGameOver)
    {
        gameOver = _isGameOver;
    }

    // Devuelve el valor de la bandea gameWin
    public bool GetIsWin()
    {
        return gameWin;
    }

    // Actualiza el valor de la bandera gameWin
    public void SetIsWin(bool _win)
    {
        gameWin = _win;
    }

}
