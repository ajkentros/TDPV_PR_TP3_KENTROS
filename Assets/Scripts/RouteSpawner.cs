using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RouteSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnRoutePrefab;                   // Referencia al prefab de Route
    [SerializeField] private GameManager gameManager;                       // Referencia al prefab de Route
    [SerializeField] private List<ScriptableObject_Desafios> desafios;      // Lista de Scriptable Object_Desaf�os
    [SerializeField] private float offset = 25;                             // Distancia entre un route y otro


    private int currentDesafioIndex = 0;        // Variable referencia al indice del desaf�o actual
    private int cantidadDesafios;               // variable referencia a la cantidad de desaf�os
    private Vector3 spawnPosition;              // Variable referencia que guardar la posici�n del spawnRoutePrefab

    private GameObject currentRouteInstance;    // Veferencia del game object route actual


    // Start is called before the first frame update
    void Awake()
    {
        // Obtiene la posici�n del spawnRoutePrefab
        spawnPosition = spawnRoutePrefab.transform.position;

        // Inicializa el primer route
        InitRouterSpawner();
    }

    private void Update()
    {
        // Verifica si el juego est� pausado
        if (gameManager.IsPaused())
        {
            return; // sale del m�todo Update si el juego est� pausado

        }
    }

    // Gestiona las acciones cuando el player colisiona con el final del route
    public void SpawnTriggerEntered()
    {
        // Calcula la posici�n del siguiente route con el offset (largo en z del route)
        float newRouteZ = currentRouteInstance.transform.position.z + offset;

        // Clona el route en la siguiente posici�n
        GameObject newRouteObject = Instantiate(spawnRoutePrefab, new Vector3(0, 0, newRouteZ), Quaternion.identity);

        // Destruye la instancia del anterior Route
        Destroy(currentRouteInstance);

        // Actualiza la referencia a la nueva instancia de Route
        currentRouteInstance = newRouteObject;

        // Actualiza los TextMeshPro del nuevo Route
        SetTextBox(currentDesafioIndex); GetTextBox(newRouteObject);

        // Actualiza el GameManager con el valor del �ndice del desaf�o actual
        gameManager.SetCurrentDesafioIndex(currentDesafioIndex);

    }

    // Obtiene los textos en los TextMeshPro hijos del GameObject dado
    private void GetTextBox(GameObject targetRoute)
    {
        List<TextMeshPro> tempList = new();

        foreach (Transform child in targetRoute.transform)
        {
            TextMeshPro textMesh = child.GetComponentInChildren<TextMeshPro>();

            if (textMesh != null)
            {
                tempList.Add(textMesh);
            }
        }

        //textListBox = tempList.ToArray();

        //Debug.Log(" Box1=" + tempList[0].text + " Box2=" + tempList[1].text + " Box3=" + tempList[2].text);

    }

    // Cambia los textos de los TextMeshPro seg�n los datos del Desafio en el �ndice especificado
    private void SetTextBox(int _currentDesafioIndex)
    {
        //Debug.Log("Desaf�o n� "+ _currentDesafioIndex);

        if (_currentDesafioIndex < 0 || _currentDesafioIndex > desafios.Count)
        {
            //Debug.LogWarning("�ndice de desaf�o inv�lido");
            return; // sale del m�todo si el el �ndice del desaf�o supera la cantidad m�xima de desaf�os (no deber�a suceder)
        }

        // Instancia un nuevo ScriptableObject_Desafios = al de la lista seg�n el valor del �ndice del desaf�o actual
        ScriptableObject_Desafios desafio = desafios[_currentDesafioIndex];

        // Por cada hijo en el route actual
        foreach (Transform child in currentRouteInstance.transform)
        {
            // Instancia un TextMeshPro que contiene el texto del hijo encontrado (estos textos est�n en las box)
            TextMeshPro textMesh = child.GetComponentInChildren<TextMeshPro>();

            // Si el texto existe =>
            if (textMesh != null)
            {
                // Seg�n el nombre del game object encontrado se le asigna un texto obtenido de la lista de scritables object desafios 
                switch (child.gameObject.name)
                {
                    case "Box1":
                        textMesh.text = desafio.TextBox1;
                        break;
                    case "Box2":
                        textMesh.text = desafio.TextBox2;
                        break;
                    case "Box3":
                        textMesh.text = desafio.TextBox3;
                        break;
                }
            }
        }

    }

    // Obtiene el texto de una caja espec�fica
    public string GetBoxText(string boxName)
    {
        // Por cada hijo del route actual
        foreach (Transform child in currentRouteInstance.transform)
        {
            // Instancia un TextMeshPro que contiene el texto del hijo encontrado (estos textos est�n en las box)
            TextMeshPro textMesh = child.GetComponentInChildren<TextMeshPro>();

            // Si el texto existe (no es nulo) y el hijo del route que contiene el texto (es una box) = el nombre de la caja que colision� el player =>
            if (textMesh != null && child.gameObject.name == boxName)
            {
                // Retorna el texto de la caja colisionada
                return textMesh.text;

            }

        }

        return null;
    }

    // Obteiene la soluci�n actual
    public string GetCurrentSolution()
    {
        // Si el �ndice del desaf�o actual no est� fuera de rango retorna el valor soluci�n guarado en el scriptable object
        if (currentDesafioIndex < 0 || currentDesafioIndex >= desafios.Count)
        {
            return null;
        }
        return desafios[currentDesafioIndex].Solucion;
    }

    // Actualiza el valor del �ndice del desaf�o
    public void SetCurrentDesafioIndex()
    {
        // Aumenta el valor de currentDesafioIndex;
        currentDesafioIndex++;

        // Si el �ndice del desaf�o actual >= cantidad de desaf�os =>
        if(currentDesafioIndex >= desafios.Count)
        {
            // Actualiza el flag gameWin (porque el jugador gano al resolver todos los desaf�os) y detiene el jeugo
            gameManager.SetIsWin(true);
            Time.timeScale = 0f;
        }
        //Debug.Log(" currentDesafioIndex = " + currentDesafioIndex);
        
    }

    // Devuelve el valor del �ndide de desaf�o actual
    public int GetCurrentDesafioIndex()
    {
        // Devuelve el valor de currentDesafioIndex;
        return currentDesafioIndex;
    }

    // Devuelve el valor de la cantidad de desaf�os
    public int GetCantidadDesafios()
    {
        // Contar la cantidad de desaf�os
        cantidadDesafios = desafios.Count;

        // Devuelve el valor de currentDesafioIndex;
        return cantidadDesafios;
    }

    // Gestiona la instancia inicial del route
    public void InitRouterSpawner()
    {
        // Reinicia variables
        currentDesafioIndex = 0;

        // Destuye alguna instancia route anterior (por la dudas si hay alguna)
        Destroy(currentRouteInstance);

        // Instancia el primer Route desde el prefab en la posici�n obtenida
        currentRouteInstance = Instantiate(spawnRoutePrefab, spawnPosition, Quaternion.identity);

        //offset = transform.localScale.z; Debug.Log(offset);

        // Configura los textos del primer Route
        SetTextBox(currentDesafioIndex);

        GetTextBox(currentRouteInstance); // este m�todo se puede eliminar

    }

    // Devuelve la lista de la pizarra con los sumandos y el resultado, lo usa UI 
    public List<string> GetDesafioTextPizarra()
    {
        // Si currentDesafioIndex no est� dentro de los l�mites de la lista desafios => actualiza el flag gameWin (gan� la partida)
        if (currentDesafioIndex < 0 || currentDesafioIndex >= desafios.Count)
        {
            Debug.LogError("�ndice de desaf�o fuera de los l�mites.");
            gameManager.SetIsWin(true);
            return null;
        }
        else
        {
            // Inicializa y agrega elementos a la lista desafioTextoPizarra con los sumandos y el resultado obtenidos del scriptable object desafio
            List<string> desafioTextoPizarra = new()
        {
            desafios[currentDesafioIndex].TextSumando1,
            desafios[currentDesafioIndex].TextSumando2,
            desafios[currentDesafioIndex].TextResultado
        };
            // Retorna la lista
            return desafioTextoPizarra;
        }
    }
}
