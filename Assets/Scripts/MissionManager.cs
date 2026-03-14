using System.Collections; // Necesario para usar Corrutinas (el temporizador)
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instancia { get; private set; }

    [Header("Datos para probar el MVP sin botones")]
    public HeroeData heroeDePrueba;
    public MisionData misionDePrueba;

    private void Awake()
    {
        if (Instancia == null) Instancia = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Esto es un "hack" temporal para probar la lógica nada más darle al Play
        if (heroeDePrueba != null && misionDePrueba != null)
        {
            Debug.Log("Iniciando prueba automática en 2 segundos...");
            Invoke(nameof(TestearMision), 2f);
        }
    }

    private void TestearMision()
    {
        EnviarAMision(heroeDePrueba, misionDePrueba);
    }

    // Esta es la función real que llamaremos en el futuro desde los botones
    public void EnviarAMision(HeroeData heroe, MisionData mision)
    {
        Debug.Log($"[{heroe.nombre}] sale zumbando a la misión: {mision.nombreMision}. Tardará {mision.tiempoDuracionSegundos}s.");
        
        // Iniciamos el temporizador asíncrono
        StartCoroutine(ProcesarMision(heroe, mision));
    }

    private IEnumerator ProcesarMision(HeroeData heroe, MisionData mision)
    {
        // 1. El juego "espera" sin congelarse gracias a la corrutina
        yield return new WaitForSeconds(mision.tiempoDuracionSegundos);

        // 2. Comparamos estadísticas (Matemáticas muy básicas para este MVP)
        int puntosHeroe = heroe.fuerza + heroe.inteligencia;
        int puntosMision = mision.requisitoFuerza + mision.requisitoInteligencia;

        if (puntosHeroe >= puntosMision)
        {
            // ÉXITO ABSOLUTO
            Debug.Log($"¡ÉXITO! {heroe.nombre} resolvió la misión como un profesional.");
            GameManager.Instancia.GanarDinero(mision.recompensaDinero);
        }
        else if (puntosHeroe >= puntosMision - 3) 
        {
            // ÉXITO CON DAÑOS COLATERALES (se quedó cerca)
            float pagoReducido = mision.recompensaDinero / 2;
            Debug.Log($"Uff... {heroe.nombre} lo logró, pero destrozó medio barrio. Nos pagan la mitad.");
            GameManager.Instancia.GanarDinero(pagoReducido);
        }
        else
        {
            // FRACASO
            Debug.Log($"¡FRACASO ESTREPITOSO! {heroe.nombre} hizo el ridículo. No cobramos un duro.");
            // Aquí en el futuro le cambiaremos el estado a "Herido"
        }
    }
}