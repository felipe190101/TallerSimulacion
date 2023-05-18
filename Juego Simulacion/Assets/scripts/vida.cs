using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vida : MonoBehaviour
{

    [SerializeField] Slider barraVida;

    private int vidaPersonaje;

    public void iniciarVIda()
    {
        barraVida.maxValue = vidaPersonaje;
        barraVida.value = barraVida.maxValue;
    }

    public void setVida(int cantidadVida)
    {
        this.vidaPersonaje = cantidadVida;
        barraVida.value = vidaPersonaje;
    }

}
