using UnityEngine;
using TycoonAgency; // Importamos los enums que acabamos de crear

// Esta línea crea un botón en el menú de Unity para fabricar héroes fácilmente
[CreateAssetMenu(fileName = "NuevoHeroe", menuName = "Tycoon/Datos de Heroe")]
public class HeroeData : ScriptableObject
{
    [Header("Información Básica")]
    public string nombre;
    public Rango rango;
    public float costeContratacion;

    [Header("Estadísticas (MVP)")]
    public int fuerza;
    public int inteligencia;
}