using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalStep = 3f; // Paso horizontal
    [SerializeField] private float movementSpeed = 3f; // Velocidad de movimiento vertical
    [SerializeField] private float jumpForce = 5f; // Fuerza del salto
    [SerializeField] private RouteSpawner routeSpawner; // Referencia al GameManager

    private bool isGrounded = true; // Variable para verificar si el jugador está en el suelo

    private void Start()
    {
        routeSpawner = FindObjectOfType<RouteSpawner>();
    }

    private void Update()
    {
        // Mover el jugador horizontalmente solo en pasos discretos
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveHorizontal(-horizontalStep);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveHorizontal(horizontalStep);
        }

        // Mover verticalmente 
        
            MoveVertical();
        

        // Saltar si el jugador está en el suelo y se presiona la tecla de salto (Espacio)
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void MoveHorizontal(float targetXPosition)
    {
        Vector3 currentPosition = transform.position;
        // Mover al jugador horizontalmente
        if(currentPosition.x >= -0.5f && currentPosition.x <= 0.5f)
        {
            currentPosition.x = 0;
            transform.position = new Vector3(currentPosition.x + targetXPosition, transform.position.y, transform.position.z);
            
            
        }else if(currentPosition.x >= 2.5f && currentPosition.x <= 3.5f)
        {
            if(targetXPosition < 0)
            {
                currentPosition.x = 3;
                transform.position = new Vector3(currentPosition.x + targetXPosition, transform.position.y, transform.position.z);
            }
            
        }else if (currentPosition.x >= -3.5f && currentPosition.x <= -2.5f)
        {
            if (targetXPosition > 0)
            {
                currentPosition.x = -3;
                transform.position = new Vector3(currentPosition.x + targetXPosition, transform.position.y, transform.position.z);
            }
        }
        //Debug.Log(transform.position.x);
    }

    private void MoveVertical()
    {
        // Mover verticalmente sin intervención del jugador
        transform.Translate(movementSpeed * Time.deltaTime * Vector3.forward);
    }

    private void Jump()
    {
        // Aplicar la fuerza de salto en la dirección vertical

        //GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        transform.Translate(jumpForce * Time.deltaTime * Vector3.up);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si el jugador colisiona con un objeto que tenga el tag "Route"
        if (collision.gameObject.CompareTag("Route"))
        {
            isGrounded = true;
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        // Verificar si el jugador ya no está en contacto con un objeto que tenga el tag "Route"
        if (collision.gameObject.CompareTag("Route"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpawnTrigger"))
        {
            Debug.Log("trigger con SpawnTrigger");
            routeSpawner.SpawnTriggerEntered();
        }

        if (other.CompareTag("Box"))
        {
            // Obtener el nombre de la caja
            string boxName = other.gameObject.name;

            // Obtener el texto de la caja
            string boxText = routeSpawner.GetBoxText(boxName);

            // Obtener la solución actual
            string currentSolution = routeSpawner.GetCurrentSolution();

            // Verificar si el texto coincide con la solución
            if (boxText == currentSolution)
            {
                Debug.Log("Respuesta correcta!: " + boxText +" "+ currentSolution);
                // Aquí puedes agregar la lógica adicional cuando la respuesta es correcta
            }
            else
            {
                Debug.Log("Respuesta incorrecta." + boxText + " " + currentSolution);
                // Aquí puedes agregar la lógica adicional cuando la respuesta es incorrecta
            }
        }
    }
}

