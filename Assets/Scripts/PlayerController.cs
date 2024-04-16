using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalStep = 3f;          // Variable referencia a ls saltos que hará el player en el eje x (horizontal)
    [SerializeField] private float movementSpeedInit = 5f;       // Variable referencia a la velocidad inicial de movimiento en el eje y (vertical)
    [SerializeField] private float movementSpeedFinal = 20f;     // Variable referencia a la velocidad final de movimiento en el eje y (vertical)
    [SerializeField] private float jumpForce = 5f;               // Variable referencia a la fuerza del salto
    
    [SerializeField] private RouteSpawner routeSpawner;          // Variable referencia al RouteSpawner
    [SerializeField] private GameManager gameManager;            // Variable referencia al GameManager

    private Rigidbody rb;                       // Variable referencia al Rigidbody del player
    private float movementSpeed;                // Variable referencia velocidad actual de movimiento en el eje y (vertical) 
    private float factorSpeed;                  // Factor de aumento de la velocidad = (20f = velocidad Final) / (5f = velocidad inicial) elevado a (1/maxima cantidad de desfíos) 
    private readonly float gravityScale = 3f;   // Variable referencia de la escala de aumento a aplicar a la gravedad (para que caiga más rápido)
    private Vector3 playerPosition;        // Variable referencia para guardar la posición original del playerPrefab
    private bool isGrounded = true;             // Variable referencia para verificar si el jugador está en el suelo


    private void Start()
    {
        // Obtiene la posición del playerPrefab
        playerPosition = transform.position; Debug.Log(playerPosition);

        // Instancia el player en su posición original del prefab
        InitPlayerController();
    }

    private void Update()
    {
        // Verifica si el juego está pausado
        if (gameManager.IsPaused())
        {
            return; // Sale de Update si el juego está pausado

        }else if(gameManager.GetIsGameOver() == false || gameManager.GetIsWin() == false)
        {
            // Si no está gameOver o gameWin => se mueve
            ManageMovements(); 
        }
    }

    // Gestiona el movimiento del player
    private void ManageMovements()
    {
        // Mueve al jugador en el eje x (horizontal) solo en pasos discretos (3 movimientos) si hace clic en la tecla izquierda o derecha
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Mueve al player a la izqueirda
            MoveHorizontal(-horizontalStep);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Mueve al pleyar a la derecha
            MoveHorizontal(horizontalStep);
        }

        // Mueve al player en el eje y (vertical)
        MoveVertical();

        // Salta el player si hace clic en space y está en el suelo
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // Mueve al player en el eje y (vertical)
            Jump();
        }
    }

    // Gestiona el movimiento en el eje x (horizontal)
    private void MoveHorizontal(float targetXPosition)
    {
        // Define la cariable currentPosition = a la posición actual del player
        Vector3 currentPosition = transform.position;

        // Si la variable currentPosition en el eje x está dentro de un margen de error => posiciona al player en su nueva posición con saltos definidos en targetPosition = horizotal step
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

    // Gestiona el movimiento del player en el eje z (vertical)
    private void MoveVertical()
    {
        // Mueve al pleyer en el eje z (vertical)
        transform.Translate(movementSpeed * Time.deltaTime * Vector3.forward);
    }

    // Gestiona el movimiento del player en el eje y (salto)
    private void Jump()
    {
        // Aplicaa una fuerza de salto en la dirección y, al rigidbody del player
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // Establece un drag alto para reducir la velocidad en el aire
        rb.drag = 5f;
    }

    // Gestiona la colisión del Player cuando toca el route (lo habilita a saltar)
    private void OnCollisionEnter(Collision collision)
    {
        // Si el player colisiona con route => cambia la bandera de que está en el piso y modifica el drag
        if (collision.gameObject.CompareTag("Route"))
        {
            isGrounded = true;
            // Restablecer el drag a 0 para mantener la velocidad constante en el suelo
            rb.drag = 0f;
        }
    }

    // Gestiona la colisión del Player cuando está saltando, no toca el route (lo habilita a no saltar cuando está saltando)
    private void OnCollisionExit(Collision collision)
    {
        // Si el jugador ya no toca el route => cambia la bandera para que no pueda saltar mientras está saltando
        if (collision.gameObject.CompareTag("Route"))
        {
            isGrounded = false;
        }
    }

    // Gestiona la colisión del player con el final del route (tag = SpawnTrigger) o el player con una Box (que tiene la solución del acertijo)
    private void OnTriggerEnter(Collider other)
    {
        // Si el player colisiona con el final del route => llama al método que crea un nuevo route
        if (other.gameObject.CompareTag("SpawnTrigger"))
        {
            //Debug.Log("trigger con SpawnTrigger");
            routeSpawner.SpawnTriggerEntered();
        }

        // Si el player con el Box => 
        if (other.CompareTag("Box"))
        {
            // Obtiene el nombre de la caja colisionada
            string boxName = other.gameObject.name; //Debug.Log("colisión con box =" + boxName);

            // Obteniene el texto de la caja colisioanda
            string boxText = routeSpawner.GetBoxText(boxName); //Debug.Log("texto de la caja =" + boxText);

            // Obtiene la solución actual llamando al método GetCurrentSolution() que le devuelve el texto de la solución cargada en el scriptabel object
            string currentSolution = routeSpawner.GetCurrentSolution(); //Debug.Log("Solución = " +  currentSolution);

            // Si el texto de la caja colisionada = al texto solución =>
            if (boxText == currentSolution)
            {
                //Debug.Log("Respuesta correcta!: " + boxText + " " + currentSolution);

                // Destruye la caja (GameObject) colisionada
                Destroy(other.gameObject);

                // Llama al método SetCurrentDesafioIndex() que incrementa el índice del desafío actual 
                routeSpawner.SetCurrentDesafioIndex();

                // Aumenta la velocidad del player haciendo que la velcodad actual = velcidad actual * el factor de velocidad elevado al índice de desafío actual
                movementSpeed =  movementSpeedInit * Mathf.Pow(factorSpeed, routeSpawner.GetCurrentDesafioIndex());

                //Debug.Log(" velocidad = " + movementSpeed);

                // Actualiza el valor de la velocida en GamaManager
                gameManager.SetSpeed(movementSpeed);
            }
            else
            {
                //Debug.Log("Respuesta incorrecta." + boxText + " " + currentSolution);

                // Si el texto de la caja colisionada no es igual la texto solución => actualiza la bandera de gameOver en GameManager
                gameManager.SetIsGameOver(true);
                                
            }   
        }
    }
    
    // Instancia el player en su posición original del prefab
    public void InitPlayerController()
    {
        // Asigna velocidad de movimiento = velocidad incial
        movementSpeed = movementSpeedInit;

        // Calcula el factor de crecimiento de la velocidad
        factorSpeed = Mathf.Pow(movementSpeedFinal / movementSpeedInit, 1f / gameManager.GetCantidadDesafios());

        //movementSpeed = movementSpeedInit * Mathf.Pow(factorSpeed, routeSpawner.GetCurrentDesafioIndex()); Debug.Log("velocidad =" + movementSpeed);
        Debug.Log("movementSpeed = " + movementSpeed);

        // Instancia el player desde el prefab en la posición original
        transform.position = playerPosition;

        // Instancia el Ribidbody del player
        rb = GetComponent<Rigidbody>();

        // Ajusta el -9.81 según la gravedad y gravityScale
        Physics.gravity = new Vector3(0, -9.81f * gravityScale, 0);  

        // Instancia el GameObject del tipo RouteSpawner
        routeSpawner = FindObjectOfType<RouteSpawner>();

        // Actualiza la velocidad en GameManager
        gameManager.SetSpeed(movementSpeed);
        
    }
}

