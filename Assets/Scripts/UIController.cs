using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;    

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textNumberDesafio;     // Variable referencia al texto textNumberDesafio
    [SerializeField] private TextMeshProUGUI textNumberSpeed;       // Variable referencia al texto textNumberSpeed
    [SerializeField] private TextMeshProUGUI textSumando1;          // Variable referencia al texto textSumando1
    [SerializeField] private TextMeshProUGUI textSumando2;          // Variable referencia al texto textSumando2
    [SerializeField] private TextMeshProUGUI textResultado;         // Variable referencia al texto textResultado

    [SerializeField] private GameObject panelDesafio;               // Variable referencia al game object panelDesafio
    [SerializeField] private GameObject panelGameWin;               // Variable referencia al game object panelWin
    [SerializeField] private GameObject panelGameOver;              // Variable referencia al game object panelGameOver
    [SerializeField] private GameManager gameManager;               // Variable referencia al GameManager panelManager

    [SerializeField] private RouteSpawner routeSpawner;             // Variable referencia al RouteSpawner routeSpawner


    // Start is called before the first frame update
    void Start()
    {
        // Desactiva el panel Game Over y Game Win al incio del juego
        panelGameOver.SetActive(false);
        panelGameWin.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // LLama al método que verifica el estado del juego
        UpDateUI();
           
    }

    // Gestiona el estado del juego
    private void UpDateUI()
    {
        // si gameOver = true => panel Game Over
        // sino si gameWin = true => panel Game Win
        // sino => actualiza panel del juego
        if (gameManager.GetIsGameOver())
        {
            
            UIGameOver();

        }else if (gameManager.GetIsWin())
        {
            UIGameWin();
        }
        else 
        {
            // Muestra el nivel de juego = cantidad de desafios ganados y speed del player
            panelGameOver.SetActive(false);
            panelGameWin.SetActive(false);
            panelDesafio.SetActive(true);

            textNumberDesafio.text = (gameManager.GetCurrentDesafioIndex() + 1).ToString();
            textNumberSpeed.text = gameManager.GetSpeed().ToString("F0"); 

            List<string> desafioTextoPizarra = routeSpawner.GetDesafioTextPizarra();
            if (desafioTextoPizarra != null && desafioTextoPizarra.Count >= 3)
            {
                textSumando1.text = desafioTextoPizarra[0];
                textSumando2.text = desafioTextoPizarra[1];
                textResultado.text = desafioTextoPizarra[2];
                for (int i = 0; i < desafioTextoPizarra.Count; i++)
                {
                    if (desafioTextoPizarra[i] == "?")
                    {
                        switch (i)
                        {
                            case 0:
                                textSumando1.color = Color.red;
                                textSumando2.color = Color.black;
                                textResultado.color = Color.black;
                                break;

                            case 1:
                                textSumando1.color = Color.black;
                                textSumando2.color = Color.red;
                                textResultado.color = Color.black;
                                break;

                            case 2:
                                textSumando1.color = Color.black;
                                textSumando2.color = Color.black;
                                textResultado.color = Color.red;
                                break;
                        }
                    }
                }
            }
        }
    }
    private void UIGameOver()
    {
        // Desactivar el Panel1 al inicio del juego
        panelGameOver.SetActive(true);
        panelGameWin.SetActive(false);
        panelDesafio.SetActive(false);
        
    }
    private void UIGameWin()
    {
        // Desactivar el Panel1 al inicio del juego
        panelGameOver.SetActive(false);
        panelGameWin.SetActive(true);
        panelDesafio.SetActive(false);
    }

    public void OnClickReiniciar()
    {
        gameManager.Reinicia();
    }
}
