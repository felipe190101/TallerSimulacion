using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlJuego : MonoBehaviour
{
    public void SalirJuego(){
        Application.Quit();
    }

    public void ComenzarJuego(){
        SceneManager.LoadScene("MundoAbierto");
    }
}
