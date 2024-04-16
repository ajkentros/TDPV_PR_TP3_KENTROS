using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalStep = 3f;          // Variable referencia a ls saltos que har� el player en el eje x (horizontal)
    [SerializeField] private float movementSpeedInit = 5f;       // Variable referencia a la velocidad inicial de movimiento en el eje y (vertical)
    [SerializeField] private float movementSpeedFinal = 20f;     // Variable referencia a la velocidad final de movimiento en el eje y (vertical)
    [SerializeField] private float jumpForce = 5f;               // Variable referencia a la fuerza del salto
    
    [SerializeField] private RouteSpawner routeSpawner;          // Variable referencia al RouteSpawner
    [SerializeField] private GameManager gameManager;            // Variable referencia al GameManager

    private Rigidbody rb;                       // Variable referencia al Rigidbody del player
    private float movementSpeed;                // Variable referencia velocidad actual de movimiento en el eje y (vertical) 
    private float factorSpeed;                  // Factor de aumento de la velocidad = (20f = velocidad Final) / (5f = velocidad inicial) elevado a (1/maxima cantidad de desf�os) 
    private readonly float gravityScale = 3f;   // Variable referencia de la escala de aumento a aplicar a la gravedad (para que caiga m�s r�pido)
    private Vector3 playerPosition;        // Variable referencia para guardar la posici�n original del playerPrefab
    private bool isGrounded = true;             // Variable referencia para verificar si el jugador est� en el suelo


    private void Start()
    {
        // Obtiene la posici�n del playerPrefab
        playerPosition = transform.position; Debug.Log(playerPosition);

        // Instancia el player en su posici�n original del prefab
        InitPlayerController();
    }

    private void Update()
    {
        // Verifica si el juego est� pausado
        if (gameManager.IsPaused())
        {
            return; // Sale de Update si el juego est� pausado

        }else if(gameManager.GetIsGameOver() == false || gameManager.GetIsWin() == false)
        {
            // Si no est� gameOver o gameWin => se mueve
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

        // Salta el player si hace clic en space y est� en el suelo
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // Mueve al player en el eje y (vertical)
            Jump();
        }
    }

    // Gestiona el movimiento en el eje x (horizontal)
    private void MoveHorizontal(float targetXPosition)
    {
        // Define la cariable currentPosition = a la posici�n actual del player
        Vector3 currentPosition = transform.position;

        // Si la variable currentPosition en el eje x est� dentro de un margen de error => posiciona al player en su nueva posici�n con saltos definidos en targetPosition = horizotal step
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
        // Aplicaa una fuerza de salto en la direcci�n y, al rigidbody del player
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // Establece un drag alto para reducir la velocidad en el aire
        rb.drag = 5f;
    }

    // Gestiona la colisi�n del Player cuando toca el route (lo habilita a saltar)
    private void OnCollisionEnter(Collision collision)
    {
        // Si el player colisiona con route => cambia la bandera de que est� en el piso y modifica el drag
        if (collision.gameObject.CompareTag("Route"))
        {
            isGrounded = true;
            // Restablecer el drag a 0 para mantener la velocidad constante en el suelo
            rb.drag = 0f;
        }
    }

    // Gestiona la colisi�n del Player cuando est� saltando, no toca el route (lo habilita a no saltar cuando est� saltando)
    private void OnCollisionExit(Collision collision)
    {
        // Si el jugador ya no toca el route => cambia la bandera para que no pueda saltar mientras est� saltando
        if (collision.gameObject.CompareTag("Route"))
        {
            isGrounded = false;
        }
    }

    // Gestiona la colisi�n del player con el final del route (tag = SpawnTrigger) o el player con una Box (que tiene la soluci�n del acertijo)
    private void OnTriggerEnter(Collider other)
    {
        // Si el player colisiona con el final del route => llama al m�todo que crea un nuevo route
        if (other.gameObject.CompareTag("SpawnTrigger"))
        {
            //Debug.Log("trigger con SpawnTrigger");
            routeSpawner.SpawnTriggerEntered();
        }

        // Si el player con el Box => 
        if (other.CompareTag("Box"))
        {
            // Obtiene el nombre de la caja colisionada
            string boxName = other.gameObject.name; //Debug.Log("colisi�n con box =" + boxName);

            // Obteniene el texto de la caja colisioanda
            string boxText = routeSpawner.GetBoxText(boxName); //Debug.Log("texto de la caja =" + boxText);

            // Obtiene la soluci�n actual llamando al m�todo GetCurrentSolution() que le devuelve el texto de la soluci�n cargada en el scriptabel object
            string currentSolution = routeSpawner.GetCurrentSolution(); //Debug.Log("Soluci�n = " +  currentSolution);

            // Si el texto de la caja colisionada = al texto soluci�n =>
            if (boxText == currentSolution)
            {
                //Debug.Log("Respuesta correcta!: " + boxText + " " + currentSolution);

                // Destruye la caja (GameObject) colisionada
                Destroy(other.gameObject);

                // Llama al m�todo SetCurrentDesafioIndex() que incrementa el �ndice del desaf�o actual 
                routeSpawner.SetCurrentDesafioIndex();

                // Aumenta la velocidad del player haciendo que la velcodad actual = velcidad actual * el factor de velocidad elevado al �ndice de desaf�o actual
                movementSpeed =  movementSpeedInit * Mathf.Pow(factorSpeed, routeSpawner.GetCurrentDesafioIndex());

                //Debug.Log(" velocidad = " + movementSpeed);

                // Actualiza el valor de la velocida en GamaManager
                gameManager.SetSpeed(movementSpeed);
            }
            else
            {
                //Debug.Log("Respuesta incorrecta." + boxText + " " + currentSolution);

                // Si el texto de la caja colisionada no es igual la texto soluci�n => actualiza la bandera de gameOver en GameManager
                gameManager.SetIsGameOver(true);
                                
            }   
        }
    }
    
    // Instancia el player en su posici�n original del prefab
    public void InitPlayerController()
    {
        // Asigna velocidad de movimiento = velocidad incial
        movementSpeed = movementSpeedInit;

        // Calcula el factor de crecimiento de la velocidad
        factorSpeed = Mathf.Pow(movementSpeedFinal / movementSpeedInit, 1f / gameManager.GetCantidadDesafios());

        //movementSpeed = movementSpeedInit * Mathf.Pow(factorSpeed, routeSpawner.GetCurrentDesafioIndex()); Debug.Log("velocidad =" + movementSpeed);
        Debug.Log("movementSpeed = " + movementSpeed);

        // Instancia el player desde el prefab en la posici�n original
        transform.position = playerPosition;

        // Instancia el Ribidbody del player
        rb = GetComponent<Rigidbody>();

        // Ajusta el -9.81 seg�n la gravedad y gravityScale
        Physics.gravity = new Vector3(0, -9.81f * gravityScale, 0);  

        // Instancia el GameObject del tipo RouteSpawner
        routeSpawner = FindObjectOfType<RouteSpawner>();

        // Actualiza la velocidad en GameManager
        gameManager.SetSpeed(movementSpeed);
        
    }
}

