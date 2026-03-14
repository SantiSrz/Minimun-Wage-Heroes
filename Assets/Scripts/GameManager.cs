using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Patrón Singleton: Permite acceder al GameManager desde CUALQUIER otro script del juego
    // simplemente escribiendo: GameManager.Instancia.GanarDinero(50);
    public static GameManager Instancia { get; private set; }

    [Header("Economía de la Agencia")]
    public float dineroActual;
    public float reputacionActual;

    private void Awake()
    {
        // Configuramos el Singleton para asegurarnos de que solo hay un GameManager
        if (Instancia == null)
        {
            Instancia = this;
            // Opcional: Descomenta la siguiente línea si quieres que la economía se mantenga al cambiar de escena (ej. ir al menú principal)
            // DontDestroyOnLoad(gameObject); 
        }
        else
        {
            // Si ya hay uno creado, destruimos la copia nueva para evitar conflictos
            Destroy(gameObject); 
        }
    }

    private void Start()
    {
        // Valores iniciales al arrancar el juego (como dice tu GDD)
        dineroActual = 1000f;
        reputacionActual = 0f;
        
        Debug.Log($"Agencia abierta. Dinero inicial: {dineroActual}$ | Reputación: {reputacionActual}");
    }

    // --- FUNCIONES PÚBLICAS PARA MODIFICAR LA ECONOMÍA ---

    public void GanarDinero(float cantidad)
    {
        dineroActual += cantidad;
        Debug.Log($"¡Ingreso! +{cantidad}$. Total: {dineroActual}$");
    }

    // Esta función devuelve un 'bool' (verdadero/falso). Así, si intentas comprar un héroe,
    // el juego sabrá si la compra ha sido exitosa o si te has quedado sin fondos.
    public bool IntentarGastarDinero(float cantidad)
    {
        if (dineroActual >= cantidad)
        {
            dineroActual -= cantidad;
            Debug.Log($"Gasto: -{cantidad}$. Total: {dineroActual}$");
            return true; // Compra exitosa
        }
        else
        {
            Debug.LogWarning("¡Fondos insuficientes! No puedes permitirte esto.");
            return false; // Compra fallida
        }
    }

    public void ModificarReputacion(float cantidad)
    {
        reputacionActual += cantidad;
        Debug.Log($"Reputación actualizada. Nuevo total: {reputacionActual}");
    }
}