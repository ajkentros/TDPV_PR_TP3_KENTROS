using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GeneradorDesafiosEjecutable
{

//#if UNITY_EDITOR
//    [MenuItem("Desafios/Generar Desafios")]

//    // Gestiona el menú
//    public static void GenerarDesdeMenu()
//    {
//        GeneradorDesafiosEjecutable generador = new GeneradorDesafiosEjecutable();
//        generador.Generar();
//    }
//#endif

//    private GenerarDesafios generador;                      // Variable referencia al GenerarDesafios generador
//    [SerializeField] private int cantidadDesafios = 5;      // Variable referencia a la cantidad de desafíos a generar

//    // Gestiona la generación de desafíos
//    public void Generar()
//    {
//        // Instancia la variable generador como un Game Object GenerarDesafios
//        generador = GameObject.FindObjectOfType<GenerarDesafios>();

//        // Si no es null =>
//        if (generador != null)
//        {
//            // Genera scriptable object hasta la cantidad de desafíos definida en el script GenerarDesafios
//            for (int i = 0; i < generador.GetCantidadDesafios(); i++)
//            {
//                // Crea una instancia un scriptable object del tipo ScriptableObject_Desafios
//                ScriptableObject_Desafios nuevoDesafio = ScriptableObject.CreateInstance<ScriptableObject_Desafios>();

//                // Llama al método GenerarDatosAleatorios() que está en el script GenerarDesafios
//                generador.GenerarDatosAleatorios(nuevoDesafio);

//                // Guardar el asset en una carpeta y refresca la carpeta
//                string path = "Assets/Desafios/Desafio_" + i + ".asset";
//                AssetDatabase.CreateAsset(nuevoDesafio, path);
//                AssetDatabase.SaveAssets();
//                AssetDatabase.Refresh();
//            }

//            Debug.Log(cantidadDesafios + " desafíos generados bien");
//        }
//        else
//        {
//            Debug.LogError("No está el script GenerarDesafios");
//        }
//    }
}