
using UnityEngine;

[CreateAssetMenu(fileName = "Desafío", menuName = "Datos del Desafío")]
public class ScriptableObject_Desafios : ScriptableObject
{
    [Header("Texto de pizarra")]
    [SerializeField] private string textSumando1;       // Variable texto que va en Box1
    public string TextSumando1 { get => textSumando1; set => textSumando1 = value; }

    [SerializeField] private string textSumando2;       // Variable texto que va en Box2
    public string TextSumando2 { get => textSumando2; set => textSumando2 = value; }

    [SerializeField] private string textResultado;       // Variable texto que va en resultado
    public string TextResultado { get => textResultado; set => textResultado = value; }


    [Header("Solución del desafío")]
    [SerializeField] private string solucion;       // Variable texto que va en Box1
    public string Solucion { get => solucion; set => solucion = value; }


    [Header("Texto en Box")]
    [SerializeField] private string textBox1;       // Variable texto que va en Box1
    public string TextBox1 { get => textBox1; set => textBox1 = value; }

    [SerializeField] private string textBox2;       // Variable texto que va en Box1
    public string TextBox2 { get => textBox2; set => textBox2 = value; }

    [SerializeField] private string textBox3;       // Variable texto que va en Box1
    public string TextBox3 { get => textBox3; set => textBox3 = value; }

    
}
