using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlJuego : MonoBehaviour
{

    [SerializeField] private GameObject textoCargando;

    public void SalirJuego(){
        Application.Quit();
    }

    public void ComenzarJuego(){
        SceneManager.LoadScene("Historia");
    }

    public void continuarJuego(){
        textoCargando.SetActive(true);
        SceneManager.LoadScene("MundoAbierto");
    }
}
