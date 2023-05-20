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

    public void iniciarVIda()
    {
        Debug.Log(vidaPersonaje);
        barraVida.maxValue = vidaPersonaje;
        barraVida.value = barraVida.maxValue;
    }

    public void setVida(int cantidadVida)
    {
        this.vidaPersonaje = cantidadVida;
        barraVida.value = vidaPersonaje;
    }

    public void destruirBarra()
    {
        Destroy(barraVida.gameObject);
        textoJefe.gameObject.SetActive(false);
    }

}
