using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Heroe : MonoBehaviour
{

    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer spritPersonaje;
    private vida sistemaVida;
    [SerializeField] private int vidaPersonaje;
    [SerializeField] private BoxCollider2D colEspada;
    [SerializeField] private GameObject textGameOver;
    [SerializeField] private GameObject iconMuerte;
   
    private float posColX = 0.3f;
    private float posColY = 0; 

    public float speed = 5.0f;

    private Fantasma fantasma;

    private void Start() {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        spritPersonaje = GetComponentInChildren<SpriteRenderer>();
        sistemaVida = GetComponent<vida>();
        sistemaVida.setVida(vidaPersonaje);
        sistemaVida.iniciarVIda();

        GameObject fan = GameObject.Find("Fantasma");
        fantasma = fan.GetComponent<Fantasma>();
    }

    void Update()
    {

        if(Input.GetMouseButtonDown(0)) {
            anim.SetTrigger("Ataca");
        }

        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        rig.velocity = new Vector2(movimientoHorizontal, movimientoVertical) * speed;

        anim.SetFloat("Camina",Mathf.Abs(rig.velocity.magnitude));

        if(movimientoHorizontal > 0) {
            colEspada.offset = new Vector2(posColX, posColY);
            spritPersonaje.flipX = true; 
        }else if(movimientoHorizontal < 0) {
            colEspada.offset = new Vector2(-posColX, posColY);
            spritPersonaje.flipX = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("PuertaMina")){
            SceneManager.LoadScene("Mina");
        }
        if (collision.gameObject.CompareTag("salida_mina")){
            SceneManager.LoadScene("Nivel1"); 
        }
        if (collision.gameObject.CompareTag("PuertaCastillo")){
            SceneManager.LoadScene("Castillo Nieve");
        }
        if (collision.gameObject.CompareTag("salida_castillo")){
            SceneManager.LoadScene("Nivel1");
        }
        if (collision.gameObject.CompareTag("PuertaVolcan")){
            SceneManager.LoadScene("Volcan");
        }
        if (collision.gameObject.CompareTag("salida_volcan")){
            SceneManager.LoadScene("Nivel1");
        }
    }

    public void CausarHerida () {
    if(vidaPersonaje > 0) {
            vidaPersonaje = vidaPersonaje - fantasma.daño ;
            sistemaVida.setVida(vidaPersonaje);

            if(vidaPersonaje <= 0) {
                textGameOver.SetActive(true);
                iconMuerte.SetActive(true);
            }
        }
    }
}
