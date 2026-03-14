using UnityEngine;
using System; // Necesario para los Eventos (Action)
using TMPro; // Necesario para la UI (Textos)
using TycoonAgency; // Nuestros Enums

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instancia { get; private set; }

    [Header("Configuración del Tiempo")]
    public float duracionDiaSegundos = 60f; // Segundos reales que dura el día
    public float tiempoActual;
    public int diaActual = 1;
    public FaseDia faseActual;

    [Header("Interfaz Visual")]
    public TextMeshProUGUI textoRelojUI;

    // Eventos a los que otros scripts (como GameManager) pueden suscribirse
    public static event Action OnNocheEmpieza;
    public static event Action OnDiaEmpieza;

    private bool relojPausado = false;

    private void Awake()
    {
        if (Instancia == null) Instancia = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        EmpezarNuevoDia();
    }

    private void Update()
    {
        // Si es de día y el reloj no está pausado, la cuenta atrás avanza
        if (faseActual == FaseDia.Dia && !relojPausado)
        {
            tiempoActual -= Time.deltaTime; // Restamos tiempo según los frames

            // Actualizamos el texto en pantalla quitando los decimales con ToString("F0")
            if (textoRelojUI != null)
            {
                textoRelojUI.text = $"DÍA {diaActual} - Faltan {tiempoActual.ToString("F0")}s para la Noche";
            }

            // Si el tiempo llega a 0, se hace de noche
            if (tiempoActual <= 0)
            {
                EmpezarNoche();
            }
        }
    }

    private void EmpezarNuevoDia()
    {
        faseActual = FaseDia.Dia;
        tiempoActual = duracionDiaSegundos;
        relojPausado = false;
        
        if (textoRelojUI != null)
        {
            textoRelojUI.text = $"DÍA {diaActual} - ¡A currar!";
        }

        Debug.Log($"☀️ COMIENZA EL DÍA {diaActual}. ¡A currar!");
        OnDiaEmpieza?.Invoke(); // Avisamos al resto del juego
    }

    private void EmpezarNoche()
    {
        faseActual = FaseDia.Noche;
        relojPausado = true; // Pausamos el reloj para resolver el ataque
        
        if (textoRelojUI != null)
        {
            textoRelojUI.text = $"DÍA {diaActual} - ¡NOCHE! (Defendiendo...)";
        }

        Debug.Log($"🌙 CAE LA NOCHE DEL DÍA {diaActual}. ¡Cerrad las puertas!");
        OnNocheEmpieza?.Invoke(); // Avisamos al GameManager para que calcule el ataque
    }

    // El GameManager llamará a esta función cuando termine el ataque
    public void FinalizarNoche()
    {
        diaActual++; // Pasamos al siguiente día
        EmpezarNuevoDia();
    }
}