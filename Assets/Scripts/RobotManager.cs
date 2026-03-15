using UnityEngine;
using TMPro; // Para poder escribir en la interfaz

public class RobotManager : MonoBehaviour
{
    [Header("Interfaz del Robot")]
    public TextMeshProUGUI textoRobotUI;

    // Nos suscribimos a los avisos del reloj
    private void OnEnable()
    {
        TimeManager.OnDiaEmpieza += HablarDeDia;
        TimeManager.OnNocheEmpieza += HablarDeNoche;
    }

    private void OnDisable()
    {
        TimeManager.OnDiaEmpieza -= HablarDeDia;
        TimeManager.OnNocheEmpieza -= HablarDeNoche;
    }

    private void HablarDeDia()
    {
        if (textoRobotUI != null)
        {
            textoRobotUI.text = "ROBOT: 'Ya es de día. Supongo que te toca fingir que trabajas. Manda a Chispitas a hacer algo.'";
        }
    }

    private void HablarDeNoche()
    {
        if (textoRobotUI != null)
        {
            textoRobotUI.text = "ROBOT: '¡Alarma! Peligro inminente. Procedo a esconder mis circuitos en la papelera de reciclaje. ¡Suerte no muriendo!'";
        }
    }
}