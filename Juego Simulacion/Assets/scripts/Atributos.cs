using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atributos : MonoBehaviour
{
    [SerializeField] private int vidaPersonaje;
    [SerializeField] private int vidaMaxima;

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
}
