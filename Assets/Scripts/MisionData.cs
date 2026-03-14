using UnityEngine;
using TycoonAgency;

[CreateAssetMenu(fileName = "NuevaMision", menuName = "Tycoon/Datos de Mision")]
public class MisionData : ScriptableObject
{
    [Header("Información Básica")]
    public string nombreMision;
    public float tiempoDuracionSegundos;

    [Header("Requisitos")]
    public int requisitoFuerza;
    public int requisitoInteligencia;

    [Header("Recompensas")]
    public float recompensaDinero;
    public float recompensaReputacion;
}