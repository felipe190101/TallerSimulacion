using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Espada : MonoBehaviour
{
    private BoxCollider2D colEspada;
    private Diablillo diablillo;
    private HongoJefe hongoJefe;
    private FantasmaJefe fantasmaJefe;
    private DiabloJefe diabloJefe;
    private Hongo hongo;
    private Fantasma fantasma;
    private Diablo diablo;


    private void Awake() {

        colEspada = GetComponent<BoxCollider2D>();

        string scenaName = SceneManager.GetActiveScene().name;
    
        if(string.Equals(scenaName, "MundoAbierto") ) {
            GameObject diabli = GameObject.Find("Diablillo");
            diablillo = diabli.GetComponent<Diablillo>();
            GameObject fan = GameObject.Find("Fantasma");
            fantasma = fan.GetComponent<Fantasma>();
            GameObject dia = GameObject.Find("Diablo");
            diablo = dia.GetComponent<Diablo>();
            GameObject hon = GameObject.Find("Hongo");
            hongo = hon.GetComponent<Hongo>();
        }else if(string.Equals(scenaName, "Mina")){
            GameObject hon = GameObject.Find("Hongo");
            hongoJefe = hon.GetComponent<HongoJefe>();
        }else if(string.Equals(scenaName, "Castillo Nieve")){
            GameObject fan = GameObject.Find("Fantasma");
            fantasmaJefe = fan.GetComponent<FantasmaJefe>();
        }else if(string.Equals(scenaName, "Volcan")){
            GameObject dia = GameObject.Find("Diablo");
            diabloJefe = dia.GetComponent<DiabloJefe>();
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Enemigo")) {
            Destroy(collision.gameObject);
        }

        if(collision.CompareTag("JefeDiablillo"))
        {
            StartCoroutine(diablillo.recibirDaño());
           
        }

        if(collision.CompareTag("JefeHongo"))
        {
            StartCoroutine(hongoJefe.recibirDaño());
           
        }

        if(collision.CompareTag("JefeDiablo"))
        {
            StartCoroutine(diabloJefe.recibirDaño());
           
        }

        if(collision.CompareTag("JefeFantasma"))
        {
            StartCoroutine(fantasmaJefe.recibirDaño());
           
        }
    }
}
