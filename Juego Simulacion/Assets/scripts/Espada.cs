using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : MonoBehaviour
{
    private BoxCollider2D colEspada;
    private Diablillo diablillo;


    private void Awake() {

        colEspada = GetComponent<BoxCollider2D>();

        GameObject diabli = GameObject.Find("Diablillo");
        diablillo = diabli.GetComponent<Diablillo>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Enemigo")) {
            Destroy(collision.gameObject);
        }

        if(collision.CompareTag("Jefe"))
        {
            StartCoroutine(diablillo.recibirDaño());
           
        }
    }
}
