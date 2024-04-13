using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RouteSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnRoutePrefab;                   // Referencia al prefab de Route
    [SerializeField] private TextMeshPro[] textListBox;                     // Referencias a los TextMeshPro hijos del prefab Route
    [SerializeField] private List<ScriptableObject_Desafios> desafios;      // Lista de Scriptable Object_Desafíos
    [SerializeField] private float offset = 25;  // Distancia entre un route y otro
    
    private int currentDesafioIndex = 0;  // Índice de los desafíos
    private GameObject currentRouteInstance;  // Instancia actual de Route

    // Start is called before the first frame update
    void Awake()
    {
        // Instanciar el primer Route desde el prefab
        currentRouteInstance = Instantiate(spawnRoutePrefab, new Vector3(0, 0, 0), Quaternion.identity);

        // Configurar los textos y obtener los TextMeshPro del primer Route
        SetTextBox(currentDesafioIndex);
        GetTextBox(currentRouteInstance);
        currentDesafioIndex++;
        
    }

    private void Update()
    {
        if(currentDesafioIndex >= desafios.Count)
        {
            //currentDesafioIndex = 0;
        }
    }
    public void SpawnTriggerEntered()
    {
        // Clonar el Route en la siguiente posición
        float newRouteZ = currentRouteInstance.transform.position.z + offset;
        GameObject newRouteObject = Instantiate(spawnRoutePrefab, new Vector3(0, 0, newRouteZ), Quaternion.identity);

        // Destruir la instancia anterior de Route
        Destroy(currentRouteInstance);

        // Actualizar la referencia a la nueva instancia de Route
        currentRouteInstance = newRouteObject;

        // Actualizar textos y obtener los TextMeshPro del nuevo Route
        SetTextBox(currentDesafioIndex); GetTextBox(newRouteObject);
        currentDesafioIndex++; 
       
        
    }

    // Obtiene los textos en los TextMeshPro hijos del GameObject dado
    private void GetTextBox(GameObject targetRoute)
    {
        List<TextMeshPro> tempList = new List<TextMeshPro>();

        foreach (Transform child in targetRoute.transform)
        {
            TextMeshPro textMesh = child.GetComponentInChildren<TextMeshPro>();
            if (textMesh != null)
            {
                tempList.Add(textMesh);
            }
        }

        textListBox = tempList.ToArray();

            Debug.Log(" Box1=" + tempList[0].text + " Box2=" + tempList[1].text + " Box3=" + tempList[2].text);
    
    }

    // Cambia los textos de los TextMeshPro según los datos del Desafio en el índice especificado
    private void SetTextBox(int _currentDesafioIndex)
    {
        Debug.Log(_currentDesafioIndex);
        if (_currentDesafioIndex < 0 || _currentDesafioIndex > desafios.Count)
        {
            Debug.LogWarning("Índice de desafío inválido");
            return;
        }

        ScriptableObject_Desafios desafio = desafios[_currentDesafioIndex];

        foreach (Transform child in currentRouteInstance.transform)
        {
            TextMeshPro textMesh = child.GetComponentInChildren<TextMeshPro>();
            if (textMesh != null)
            {
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

    // Obtener el texto de una caja específica
    public string GetBoxText(string boxName)
    {
        foreach (Transform child in currentRouteInstance.transform)
        {
            TextMeshPro textMesh = child.GetComponentInChildren<TextMeshPro>();
            if (textMesh != null && child.gameObject.name == boxName)
            {
                return textMesh.text;
            }
        }
        return null;
    }

    // Obtener la solución actual
    public string GetCurrentSolution()
    {
        if (currentDesafioIndex < 0 || currentDesafioIndex > desafios.Count)
        {
            return null;
        }
        return desafios[currentDesafioIndex].Solucion;
    }
}
