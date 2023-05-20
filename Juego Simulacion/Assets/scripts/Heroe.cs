using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class Heroe : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer spritPersonaje;
    private vida sistemaVida;
    private Atributos atributos;
    private int vidaPersonaje;
    [SerializeField] private BoxCollider2D colEspada;
    [SerializeField] private GameObject textGameOver;
    [SerializeField] private GameObject iconMuerte;    
    [SerializeField] private GameObject botonReinicio;
    [SerializeField] private GameObject textoJefeDerrotado;
    [SerializeField] private GameObject iconoJefeDerrotado;
    [SerializeField] private GameObject textoLlaveConseguida;
    [SerializeField] private GameObject iconoLlaveConseguida;
    [SerializeField] private GameObject iconoLlaveMina;
    [SerializeField] private GameObject iconoLlaveCastillo;
    [SerializeField] private GameObject iconoLlaveVolcan;
    [SerializeField] private GameObject iconoLlaveNieve;
    [SerializeField] private GameObject iconoLlaveFuego;
    [SerializeField] private GameObject textoFaltaLlaveNieve;

    private float posColX = 0.3f;
    private float posColY = 0;

    public float speed = 5.0f;
    private bool isSprinting;
    public bool llaveHielo;
    private bool llaveFuego;
    private bool llaveFinal;
    private float sprintSpeed;

    private Fantasma fantasma;
    private FantasmaJefe fantasmaJefe;
    private Murcielago murcielago;
    private Esqueleto esqueleto;
    private Diablo diablo;
    private DiabloJefe diabloJefe;
    private Diablillo diablillo;
    private Goblin goblin;
    private Hongo hongo;
    private HongoJefe hongoJefe;
    private Gusano gusano;
    private Slime slime;

    private bool miLlaveHieloBooleana;

    private void Awake()
    {
        atributos = Atributos.Instance;
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        spritPersonaje = GetComponentInChildren<SpriteRenderer>();
        sistemaVida = GetComponent<vida>();
        sistemaVida.setVidaMaxima(atributos.getVidaMaximaPersonaje());
        sistemaVida.setVida(atributos.getVidaPersonaje());
        sistemaVida.iniciarVIda();

        string scenaName = SceneManager.GetActiveScene().name;

        if(string.Equals(scenaName, "MundoAbierto")) {
            GameObject fan = GameObject.Find("Fantasma");
            fantasma = fan.GetComponent<Fantasma>();

            GameObject mur = GameObject.Find("Murcielago");
            murcielago = mur.GetComponent<Murcielago>();

            GameObject esq = GameObject.Find("Esqueleto");
            esqueleto= esq.GetComponent<Esqueleto>();

            GameObject dia = GameObject.Find("Diablo");
            diablo = dia.GetComponent<Diablo>();

            GameObject diabli = GameObject.Find("Diablillo");
            diablillo = diabli.GetComponent<Diablillo>();

            GameObject gob = GameObject.Find("Goblin");
            goblin = gob.GetComponent<Goblin>();

            GameObject hon = GameObject.Find("Hongo");
            hongo = hon.GetComponent<Hongo>();

            GameObject gus = GameObject.Find("Gusano");
            gusano = gus.GetComponent<Gusano>();

            GameObject sli = GameObject.Find("Slime");
            slime = sli.GetComponent<Slime>();
        }else if(string.Equals(scenaName, "Mina")){
            GameObject hong = GameObject.Find("Hongo");
            hongoJefe = hong.GetComponent<HongoJefe>();
        }else if(string.Equals(scenaName, "Castillo Nieve")){
            GameObject fant = GameObject.Find("Fantasma");
            fantasmaJefe = fant.GetComponent<FantasmaJefe>();
        }else if(string.Equals(scenaName, "Volcan")){
            GameObject diab = GameObject.Find("Diablo");
            diabloJefe = diab.GetComponent<DiabloJefe>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Ataca");
        }

        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        if (!isSprinting)
        {
            rig.velocity = new Vector2(movimientoHorizontal, movimientoVertical) * speed;
        }

        anim.SetFloat("Camina", Mathf.Abs(rig.velocity.magnitude));

        if (movimientoHorizontal > 0)
        {
            colEspada.offset = new Vector2(posColX, posColY);
            spritPersonaje.flipX = true;
        }
        else if (movimientoHorizontal < 0)
        {
            colEspada.offset = new Vector2(-posColX, posColY);
            spritPersonaje.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartSprinting();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopSprinting();
        }

        if (isSprinting)
        {
            ApplySprintAcceleration();
        }

        string scenaName = SceneManager.GetActiveScene().name;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PuertaMina"))
        {
            SceneManager.LoadScene("Mina");
        }
        if (collision.gameObject.CompareTag("salida_mina"))
        {
            SceneManager.LoadScene("MundoAbierto");
        }
        if (collision.gameObject.CompareTag("PuertaCastillo"))
        {
            SceneManager.LoadScene("Castillo Nieve");
        }
        if (collision.gameObject.CompareTag("salida_castillo"))
        {
            SceneManager.LoadScene("MundoAbierto");
        }
        if (collision.gameObject.CompareTag("PuertaVolcan"))
        {
            SceneManager.LoadScene("Volcan");
        }
        if (collision.gameObject.CompareTag("salida_volcan"))
        {
            SceneManager.LoadScene("MundoAbierto");
        }
        if (collision.gameObject.CompareTag("Botiquin"))
        {
            vidaPersonaje = atributos.getVidaPersonaje();
            vidaPersonaje += 100;
            sistemaVida.setVida(vidaPersonaje);
            atributos.setVidaPersonaje(vidaPersonaje);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("llaveNieve")){
            textoJefeDerrotado.SetActive(false);
            iconoJefeDerrotado.SetActive(false);
            Destroy(collision.gameObject);
            StartCoroutine(textoLlave());
        }


        /*if (collision.gameObject.CompareTag("llaveFuego")){
            llaveFuego = true;
            textoJefeDerrotado.SetActive(false);
            iconoJefeDerrotado.SetActive(false);
            Destroy(collision.gameObject);
            textoLlaveConseguida.SetActive(true);
            iconoLlaveConseguida.SetActive(true);

            llaveFuego = true;
            PlayerPrefs.SetInt("llaveFuego", llaveFuego ? 1 : 0);
            PlayerPrefs.Save();
        }*/

        if (collision.gameObject.CompareTag("PuertaNieve") && llaveHielo == true)
        {
            Destroy(collision.gameObject);
        }else if(collision.gameObject.CompareTag("PuertaNieve") && llaveHielo == false){
            textoFaltaLlaveNieve.SetActive(true);
        }
    }

    public IEnumerator textoLlave()
        {
            textoLlaveConseguida.SetActive(true);
            iconoLlaveConseguida.SetActive(true);
            yield return new WaitForSeconds(3f);
            textoLlaveConseguida.SetActive(false);
            iconoLlaveConseguida.SetActive(false);
        }

    public void CausarHeridaFantasma()
    {
        if (atributos.getVidaPersonaje() > 0)
        {
            vidaPersonaje = atributos.getVidaPersonaje();
            vidaPersonaje -= fantasma.daño;
            sistemaVida.setVida(vidaPersonaje);
            atributos.setVidaPersonaje(vidaPersonaje);

            if (atributos.getVidaPersonaje() <= 0)
            {
                textGameOver.SetActive(true);
                iconMuerte.SetActive(true);
                Time.timeScale = 0f;
                botonReinicio.SetActive(true);
            }
        }
    }

    public void CausarHeridaEsqueleto()
    {
        if (atributos.getVidaPersonaje() > 0)
        {
            vidaPersonaje = atributos.getVidaPersonaje();
            vidaPersonaje -= esqueleto.daño;
            sistemaVida.setVida(vidaPersonaje);
            atributos.setVidaPersonaje(vidaPersonaje);


            if (atributos.getVidaPersonaje() <= 0)
            {
                textGameOver.SetActive(true);
                iconMuerte.SetActive(true);
                Time.timeScale = 0f;
                botonReinicio.SetActive(true);

            }
        }
    }

    public void CausarHeridaMurcielago()
    {
        if (atributos.getVidaPersonaje() > 0)
        {
            vidaPersonaje = atributos.getVidaPersonaje();
            vidaPersonaje -= murcielago.daño;
            sistemaVida.setVida(vidaPersonaje);
            atributos.setVidaPersonaje(vidaPersonaje);

            if (atributos.getVidaPersonaje() <= 0)
            {
                textGameOver.SetActive(true);
                iconMuerte.SetActive(true);
                Time.timeScale = 0f;
                botonReinicio.SetActive(true);
            }
        }
    }

    public void CausarHeridaDiablillo()
    {
        if (atributos.getVidaPersonaje() > 0)
        {
            vidaPersonaje = atributos.getVidaPersonaje();
            vidaPersonaje -= diablillo.daño;
            sistemaVida.setVida(vidaPersonaje);
            atributos.setVidaPersonaje(vidaPersonaje);

            if (atributos.getVidaPersonaje() <= 0)
            {
                textGameOver.SetActive(true);
                iconMuerte.SetActive(true);
                Time.timeScale = 0f;
                botonReinicio.SetActive(true);
            }
        }
    }

    public void CausarHeridaDiablo()
    {
        if (atributos.getVidaPersonaje() > 0)
        {
            vidaPersonaje = atributos.getVidaPersonaje();
            vidaPersonaje -= diablo.daño;
            sistemaVida.setVida(vidaPersonaje);
            atributos.setVidaPersonaje(vidaPersonaje);

            if (atributos.getVidaPersonaje() <= 0)
            {
                textGameOver.SetActive(true);
                iconMuerte.SetActive(true);
                Time.timeScale = 0f;
                botonReinicio.SetActive(true);
            }
        }
    }

    public void CausarHeridaGoblin()
    {
        if (atributos.getVidaPersonaje() > 0)
        {
            vidaPersonaje = atributos.getVidaPersonaje();
            vidaPersonaje -= goblin.daño;
            sistemaVida.setVida(vidaPersonaje);
            atributos.setVidaPersonaje(vidaPersonaje);

            if (atributos.getVidaPersonaje() <= 0)
            {
                textGameOver.SetActive(true);
                iconMuerte.SetActive(true);
                Time.timeScale = 0f;
                botonReinicio.SetActive(true);
            }
        }
    }

    public void CausarHeridaHongo()
    {
        if (atributos.getVidaPersonaje() > 0)
        {
            vidaPersonaje = atributos.getVidaPersonaje();
            vidaPersonaje -= hongo.daño;
            sistemaVida.setVida(vidaPersonaje);
            atributos.setVidaPersonaje(vidaPersonaje);

            if (atributos.getVidaPersonaje() <= 0)
            {
                textGameOver.SetActive(true);
                iconMuerte.SetActive(true);
                Time.timeScale = 0f;
                botonReinicio.SetActive(true);
            }
        }
    }

    public void CausarHeridaGusano()
    {
        if (atributos.getVidaPersonaje() > 0)
        {
            vidaPersonaje = atributos.getVidaPersonaje();
            vidaPersonaje -= gusano.daño;
            sistemaVida.setVida(vidaPersonaje);
            atributos.setVidaPersonaje(vidaPersonaje);

            if (atributos.getVidaPersonaje() <= 0)
            {
                textGameOver.SetActive(true);
                iconMuerte.SetActive(true);
                Time.timeScale = 0f;
                botonReinicio.SetActive(true);
            }
        }
    }

    public void CausarHeridaSlime()
    {
        if (atributos.getVidaPersonaje() > 0)
        {
            vidaPersonaje = atributos.getVidaPersonaje();
            vidaPersonaje -= slime.daño;
            sistemaVida.setVida(vidaPersonaje);
            atributos.setVidaPersonaje(vidaPersonaje);

            if (atributos.getVidaPersonaje() <= 0)
            {
                textGameOver.SetActive(true);
                iconMuerte.SetActive(true);
                Time.timeScale = 0f;
                botonReinicio.SetActive(true);
            }
        }
    }

    public void CausarHeridaHongoJefe()
    {
        if (atributos.getVidaPersonaje() > 0)
        {
            vidaPersonaje = atributos.getVidaPersonaje();
            vidaPersonaje -= hongoJefe.daño;
            sistemaVida.setVida(vidaPersonaje);
            atributos.setVidaPersonaje(vidaPersonaje);

            if (atributos.getVidaPersonaje() <= 0)
            {
                textGameOver.SetActive(true);
                iconMuerte.SetActive(true);
                Time.timeScale = 0f;
                botonReinicio.SetActive(true);
            }
        }
    }

    public void CausarHeridaFantasmaJefe()
    {
        if (atributos.getVidaPersonaje() > 0)
        {
            vidaPersonaje = atributos.getVidaPersonaje();
            vidaPersonaje -= fantasmaJefe.daño;
            sistemaVida.setVida(vidaPersonaje);
            atributos.setVidaPersonaje(vidaPersonaje);

            if (atributos.getVidaPersonaje() <= 0)
            {
                textGameOver.SetActive(true);
                iconMuerte.SetActive(true);
                Time.timeScale = 0f;
                botonReinicio.SetActive(true);
            }
        }
    }

    public void CausarHeridaDiabloJefe()
    {
        if (atributos.getVidaPersonaje() > 0)
        {
            vidaPersonaje = atributos.getVidaPersonaje();
            vidaPersonaje -= diabloJefe.daño;
            sistemaVida.setVida(vidaPersonaje);
            atributos.setVidaPersonaje(vidaPersonaje);

            if (atributos.getVidaPersonaje() <= 0)
            {
                textGameOver.SetActive(true);
                iconMuerte.SetActive(true);
                Time.timeScale = 0f;
                botonReinicio.SetActive(true);
            }
        }
    }

    private void StartSprinting()
    {
        isSprinting = true;
        sprintSpeed = speed;
    }

    private void StopSprinting()
    {
        isSprinting = false;
    }

    private void ApplySprintAcceleration()
    {
        sprintSpeed += 2 * Time.deltaTime;
        sprintSpeed = Mathf.Clamp(sprintSpeed, 0.0f, 10);

        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        Vector2 sprintVelocity = new Vector2(movimientoHorizontal, movimientoVertical) * sprintSpeed;
        rig.velocity = sprintVelocity;
    }

    public void ReiniciarJuego(){
        SceneManager.LoadScene("MundoAbierto");
        Time.timeScale = 1f;
        
    }
}
