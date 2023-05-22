using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atributos : MonoBehaviour
{
    [SerializeField] private int vidaPersonaje;
    [SerializeField] private int vidaMaxima;


    private bool llaveEntrarMina;
    private bool llaveEntrarNieve;
    private bool llaveEntrarCastillo;
    private bool llaveEntrarFuego;
    private bool llaveEntrarColiseo;
    private bool llaveEntrarVolcan;

    private bool tutorialIniciado;

    private static Atributos instance;

    public static Atributos Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }

       
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool getTutorialIniciado()
    {
        return tutorialIniciado;
    }

    public void setTutorialIniciado(bool tutoIniciado)
    {
        this.tutorialIniciado = tutoIniciado;
    }

    public int getVidaPersonaje()
    {
        return this.vidaPersonaje;
    }

    public void setVidaPersonaje(int vidaNueva)
    {
        this.vidaPersonaje = vidaNueva;
    }

    public int getVidaMaximaPersonaje()
    {
        return this.vidaMaxima;
    }

    public bool getllaveEntrarMina()
    {
        return this.llaveEntrarMina;
    }

    public void setllaveEntrarMina(bool status)
    {
        this.llaveEntrarMina = status;
    }

    public bool getllaveEntrarNieve()
    {
        return this.llaveEntrarNieve;
    }

    public void setllaveEntrarNieve(bool status)
    {
        this.llaveEntrarNieve = status;
    }

    public bool getllaveEntrarCastillo()
    {
        return this.llaveEntrarCastillo;
    }

    public void setllaveEntrarCastillo(bool status)
    {
        this.llaveEntrarCastillo = status;
    }

    public bool getllaveEntrarFuego()
    {
        return this.llaveEntrarFuego;
    }

    public void setllaveEntrarFuego(bool status)
    {
        this.llaveEntrarFuego = status;
    }

    public bool getllaveEntrarColiseo()
    {
        return this.llaveEntrarColiseo;
    }

    public void setllaveEntrarColiseo(bool status)
    {
        this.llaveEntrarColiseo = status;
    }

    public bool getllaveEntrarVolcan()
    {
        return this.llaveEntrarVolcan;
    }

    public void setllaveEntrarVolcan(bool status)
    {
        this.llaveEntrarVolcan = status;
    }

    

    
}
