using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Espada : MonoBehaviour
{
    private BoxCollider2D colEspada;
    private Diablillo diablillo;
    private Hongo hongo;
    private Fantasma fantasma;
    private Diablo diablo;


    private void Awake() {

        colEspada = GetComponent<BoxCollider2D>();

        string scenaName = SceneManager.GetActiveScene().name;
    
        if(string.Equals(scenaName, "Nivel1") ) {
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
            hongo = hon.GetComponent<Hongo>();
        }else if(string.Equals(scenaName, "Castillo Nieve")){
            GameObject fan = GameObject.Find("Fantasma");
            fantasma = fan.GetComponent<Fantasma>();
        }else if(string.Equals(scenaName, "Volcan")){
            GameObject dia = GameObject.Find("Diablo");
            diablo = dia.GetComponent<Diablo>();
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
            StartCoroutine(hongo.recibirDaño());
           
        }

        if(collision.CompareTag("JefeDiablo"))
        {
            StartCoroutine(diablo.recibirDaño());
           
        }

        if(collision.CompareTag("JefeFantasma"))
        {
            StartCoroutine(fantasma.recibirDaño());
           
        }
    }
}
