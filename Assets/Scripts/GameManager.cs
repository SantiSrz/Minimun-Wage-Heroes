using UnityEngine;
using TMPro; // Importante para la interfaz de texto

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia { get; private set; }

    [Header("Economía de la Agencia")]
    public float dineroActual;
    public float famaActual; // Cambiamos reputación por Fama según tu nuevo GDD

    [Header("Interfaz de Usuario")]
    public TextMeshProUGUI textoDineroUI;

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        dineroActual = 1000f;
        famaActual = 0f;
        ActualizarUI();
    }

    // --- ECONOMÍA ---

    public void GanarDinero(float cantidad)
    {
        dineroActual += cantidad;
        ActualizarUI();
        Debug.Log($"¡Ingreso! +{cantidad}$. Total: {dineroActual}$");
    }

    public bool IntentarGastarDinero(float cantidad)
    {
        if (dineroActual >= cantidad)
        {
            dineroActual -= cantidad;
            ActualizarUI();
            return true;
        }
        
        Debug.LogWarning("¡Fondos insuficientes!");
        return false;
    }

    public void ActualizarUI()
    {
        if (textoDineroUI != null)
        {
            textoDineroUI.text = $"Presupuesto: {dineroActual}$";
        }
    }

    // --- SISTEMA DE DÍA Y NOCHE (RIESGO) ---

    private void OnEnable()
    {
        // Nos suscribimos al evento del reloj cuando la agencia abre
        TimeManager.OnNocheEmpieza += CalcularAtaqueNocturno;
    }

    private void OnDisable()
    {
        // Nos desuscribimos si el script se apaga para evitar errores
        TimeManager.OnNocheEmpieza -= CalcularAtaqueNocturno;
    }

    private void CalcularAtaqueNocturno()
    {
        Debug.Log("GameManager: Calculando el riesgo de ataque de los villanos...");
        
        // Simulación temporal del riesgo (50% de probabilidad)
        float probabilidadAtaque = Random.Range(0f, 100f);

        if (probabilidadAtaque > 50f)
        {
            Debug.Log("💥 ¡NOS ATACAN! (Los héroes defienden la base...)");
        }
        else
        {
            Debug.Log("🛡️ Noche tranquila. Nadie ha atacado la oficina.");
        }

        // Avisamos al reloj tras 3 segundos para que vuelva a salir el sol
        Invoke(nameof(AvanzarAlSiguienteDia), 3f); 
    }

    private void AvanzarAlSiguienteDia()
    {
        TimeManager.Instancia.FinalizarNoche();
    }
}