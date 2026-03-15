using System.Collections;
using UnityEngine;
using TycoonAgency; // Importante para saber si es de Día o de Noche

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instancia { get; private set; }

    [Header("Datos de la Misión")]
    public HeroeData heroeDePrueba;
    public MisionData misionDePrueba;

    private void Awake()
    {
        if (Instancia == null) Instancia = this;
        else Destroy(gameObject);
    }

    // --- ESTA ES LA FUNCIÓN QUE CONECTAREMOS AL BOTÓN ---
    public void Boton_EnviarAMision()
    {
        // 1. Le preguntamos al reloj si es de noche
        if (TimeManager.Instancia.faseActual == FaseDia.Noche)
        {
            Debug.LogWarning("¡Es de noche! No puedes enviar héroes ahora, los necesitas para defender la base.");
            return; // Cortamos la ejecución aquí. Chispitas no sale.
        }

        // 2. Si es de día, comprobamos que los archivos de datos estén puestos
        if (heroeDePrueba != null && misionDePrueba != null)
        {
            // ¡A trabajar! Arrancamos el temporizador de la misión
            StartCoroutine(ProcesarMision(heroeDePrueba, misionDePrueba));
        }
        else
        {
            Debug.LogError("Faltan los datos de Chispitas o de la Misión en el Inspector.");
        }
    }

    private IEnumerator ProcesarMision(HeroeData heroe, MisionData mision)
    {
        Debug.Log($"[{heroe.nombre}] sale zumbando a: {mision.nombreMision}. Tardará {mision.tiempoDuracionSegundos}s.");
        
        // El juego espera el tiempo que dure la misión (ej: 5 segundos)
        yield return new WaitForSeconds(mision.tiempoDuracionSegundos);

        // Calculamos el resultado sumando atributos
        int puntosHeroe = heroe.fuerza + heroe.inteligencia;
        int puntosMision = mision.requisitoFuerza + mision.requisitoInteligencia;

        if (puntosHeroe >= puntosMision)
        {
            Debug.Log($"¡ÉXITO! {heroe.nombre} resolvió la misión. Cobramos {mision.recompensaDinero}$.");
            GameManager.Instancia.GanarDinero(mision.recompensaDinero);
        }
        else if (puntosHeroe >= puntosMision - 3) 
        {
            float pagoReducido = mision.recompensaDinero / 2;
            Debug.Log($"Uff... {heroe.nombre} destrozó medio barrio. Nos pagan la mitad: {pagoReducido}$.");
            GameManager.Instancia.GanarDinero(pagoReducido);
        }
        else
        {
            Debug.Log($"¡FRACASO! {heroe.nombre} hizo el ridículo. No cobramos nada.");
        }
    }
}