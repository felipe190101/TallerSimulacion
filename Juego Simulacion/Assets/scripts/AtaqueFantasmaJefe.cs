using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueFantasmaJefe : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            Heroe heroe = collision.gameObject.GetComponent<Heroe>();
            heroe.CausarHeridaFantasmaJefe();
        }
    }
}
