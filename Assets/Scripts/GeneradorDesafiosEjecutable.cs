using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GeneradorDesafiosEjecutable
{

//#if UNITY_EDITOR
//    [MenuItem("Desafios/Generar Desafios")]

//    // Gestiona el men�
//    public static void GenerarDesdeMenu()
//    {
//        GeneradorDesafiosEjecutable generador = new GeneradorDesafiosEjecutable();
//        generador.Generar();
//    }
//#endif

//    private GenerarDesafios generador;                      // Variable referencia al GenerarDesafios generador
//    [SerializeField] private int cantidadDesafios = 5;      // Variable referencia a la cantidad de desaf�os a generar

//    // Gestiona la generaci�n de desaf�os
//    public void Generar()
//    {
//        // Instancia la variable generador como un Game Object GenerarDesafios
//        generador = GameObject.FindObjectOfType<GenerarDesafios>();

//        // Si no es null =>
//        if (generador != null)
//        {
//            // Genera scriptable object hasta la cantidad de desaf�os definida en el script GenerarDesafios
//            for (int i = 0; i < generador.GetCantidadDesafios(); i++)
//            {
//                // Crea una instancia un scriptable object del tipo ScriptableObject_Desafios
//                ScriptableObject_Desafios nuevoDesafio = ScriptableObject.CreateInstance<ScriptableObject_Desafios>();

//                // Llama al m�todo GenerarDatosAleatorios() que est� en el script GenerarDesafios
//                generador.GenerarDatosAleatorios(nuevoDesafio);

//                // Guardar el asset en una carpeta y refresca la carpeta
//                string path = "Assets/Desafios/Desafio_" + i + ".asset";
//                AssetDatabase.CreateAsset(nuevoDesafio, path);
//                AssetDatabase.SaveAssets();
//                AssetDatabase.Refresh();
//            }

//            Debug.Log(cantidadDesafios + " desaf�os generados bien");
//        }
//        else
//        {
//            Debug.LogError("No est� el script GenerarDesafios");
//        }
//    }
}