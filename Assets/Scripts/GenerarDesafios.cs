using UnityEngine;

public class GenerarDesafios : MonoBehaviour
{
    //[SerializeField] private ScriptableObject_Desafios desafioPrefab;   // Variable referencia al ScriptableObject original que servirá como plantilla
    //[SerializeField] private int maxNumero;                        // Variable referencia al número máximo para el sumando1 y sumando2
    //[SerializeField] private int cantidadDesafios;                  // Variable referencia a la cantidad de desafíos a generar

    //private int sumando1;
    //private int sumando2;
    //private int resultado;
    //private int solucion;
    //public void GenerarDatosAleatorios(ScriptableObject_Desafios desafio)
    //{
    //    // Asigna un valor aleatorio al sumando 1 (entre 1 y el máximo a configurar)
    //    sumando1 = Random.Range(1, maxNumero);

    //    // Asigna un valor aleatorio al sumando 2 (entre 1 y el máximo a configurar)
    //    sumando2 = Random.Range(1, maxNumero);

    //    // Calcula el resultado de sumando1 + sumando2
    //    resultado = sumando1 + sumando2;
  
    //    // Obtiene un índice aleatorio entre 1 y 3
    //    int incognita = Random.Range(1, 4);

    //    // Según el índice se asignan los campos del scriptable object
    //    switch (incognita)
    //    {
    //        case 1:
    //            desafio.TextSumando1 = "?";
    //            desafio.TextSumando2 = sumando2.ToString();
    //            desafio.TextResultado = resultado.ToString();
    //            solucion = sumando1;
    //            desafio.Solucion = sumando1.ToString();
    //            break;

    //        case 2:
    //            desafio.TextSumando1 = sumando1.ToString();
    //            desafio.TextSumando2 = "?";
    //            desafio.TextResultado = resultado.ToString();
    //            solucion = sumando2;
    //            desafio.Solucion = sumando2.ToString();
    //            break;

    //        case 3:
    //            desafio.TextSumando1 = sumando1.ToString();
    //            desafio.TextSumando2 = sumando2.ToString();
    //            desafio.TextResultado = "?";
    //            solucion = resultado;
    //            desafio.Solucion = resultado.ToString();
    //            break;
    //    }
        
    //    // Obtiene un índice aleatorio entre 1 y 3
    //    int box = Random.Range(1, 4);

    //    // Según el índice se asignan los campos del scriptable object
    //    switch (box)
    //    {
    //        case 1:
    //            desafio.TextBox1 = solucion.ToString();
    //            desafio.TextBox2 = (solucion + 1).ToString();
    //            desafio.TextBox3 = (solucion - 1).ToString();
    //            break;

    //        case 2:
    //            desafio.TextBox1 = (solucion + 1).ToString();
    //            desafio.TextBox2 = solucion.ToString();
    //            desafio.TextBox3 = (solucion - 1).ToString();
    //            break;

    //        case 3:
    //            desafio.TextBox1 = (solucion + 1).ToString();
    //            desafio.TextBox2 = (solucion - 1).ToString();
    //            desafio.TextBox3 = solucion.ToString();
    //            break;
    //    }
    //}

    //// Devuelve el valor de la cantidad de desafíos a generar
    // public int GetCantidadDesafios()
    //{
    //    return cantidadDesafios;
    //}
}
