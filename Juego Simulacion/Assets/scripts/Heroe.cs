using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Heroe : MonoBehaviour
{

    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer spritPersonaje;

   

    public float speed = 5.0f;

    private void Start() {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        spritPersonaje = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        rig.velocity = new Vector2(movimientoHorizontal, movimientoVertical) * speed;

        anim.SetFloat("Camina",Mathf.Abs(rig.velocity.magnitude));

        if(movimientoHorizontal > 0) {
            spritPersonaje.flipX = true;
        }else if(movimientoHorizontal < 0) {
            spritPersonaje.flipX = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.CompareTag("PuertaMina"))
        {
            SceneManager.LoadScene("Mina");
        }
        if (collision.gameObject.CompareTag("salida_mina"))
        {
            SceneManager.LoadScene("Nivel1"); 
        }
        if (collision.gameObject.CompareTag("PuertaCastillo"))
        {
            SceneManager.LoadScene("Castillo Nieve");
        }
        if (collision.gameObject.CompareTag("salida_castillo"))
        {
            SceneManager.LoadScene("Nivel1");
        }
        if (collision.gameObject.CompareTag("PuertaVolcan"))
        {
            SceneManager.LoadScene("Volcan");
        }
        if (collision.gameObject.CompareTag("salida_volcan"))
        {
            SceneManager.LoadScene("Nivel1");
        }
        
    }
}
