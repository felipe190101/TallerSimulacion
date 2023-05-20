using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class vida : MonoBehaviour
{

    [SerializeField] Slider barraVida;
    [SerializeField] TextMeshProUGUI textoJefe;

    private int vidaPersonaje;
    private int vidaPersonajeMaxima;


    public void iniciarVIda()
    {
        barraVida.maxValue = vidaPersonajeMaxima;
        barraVida.value = vidaPersonaje;
    }

    public void setVida(int cantidadVida)
    {
        this.vidaPersonaje = cantidadVida;
        barraVida.value = vidaPersonaje;
    }

    public void setVidaMaxima(int cantidadVidaMaxima)
    {
        this.vidaPersonajeMaxima = cantidadVidaMaxima;
    }

    public void destruirBarra()
    {
        Destroy(barraVida.gameObject);
        textoJefe.gameObject.SetActive(false);
    }

}
