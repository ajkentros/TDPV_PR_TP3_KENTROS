using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;          // Variable referencia al transform del player
    private Vector3 offsetCamera;           // Varaibel ajusta el offset de la camera


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;

        // Calcula el desplazamiento inicial de la cámara con respecto al Player
        offsetCamera = transform.position - player.position;    
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // nuevo vector de posición para la cámara = posición X e Y y Z = desplazamiento constante (offsetCamera.z) + posición Z actual del Player
        
        
        Vector3 newPositionCamera = new (transform.position.x, transform.position.y, offsetCamera.z + player.position.z);
        transform.position = Vector3.Lerp(transform.position, newPositionCamera, 10 * Time.deltaTime);
    }
}
