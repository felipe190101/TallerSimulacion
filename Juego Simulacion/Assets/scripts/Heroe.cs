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
    [SerializeField] private GameObject botonVolverAlMenu;
    [SerializeField] private GameObject panelMenuPausa;
    [SerializeField] private GameObject textoJefeDerrotado;
    [SerializeField] private GameObject iconoJefeDerrotado;
    [SerializeField] private GameObject textoLlaveConseguida;
    [SerializeField] private GameObject textoLlaveConseguidaMina;
    [SerializeField] private GameObject textoLlaveConseguidaCastillo;
    [SerializeField] private GameObject textoLlaveConseguidaColiseo;
    [SerializeField] private GameObject textoLlaveConseguidaVolcan;
    [SerializeField] private GameObject iconoLlaveConseguida;
    [SerializeField] private GameObject iconoLlaveMina;
    [SerializeField] private GameObject iconoLlaveCastillo;
    [SerializeField] private GameObject iconoLlaveVolcan;
    [SerializeField] private GameObject iconoLlaveNieve;
    [SerializeField] private GameObject iconoLlaveColiseo;
    [SerializeField] private GameObject iconoLlaveFuego;
    [SerializeField] private GameObject textoFaltaLlaveNieve;
    [SerializeField] private GameObject textoFaltaLlaveFuego;
    [SerializeField] private GameObject textoFaltaLlaveMina;
    [SerializeField] private GameObject textoFaltaLlaveVolcan;
    [SerializeField] private GameObject textoFaltaLlaveColiseo;
    [SerializeField] private GameObject textoFaltaLlaveCastillo;
    [SerializeField] private GameObject textoReiniciar;
    [SerializeField] private GameObject textoCargando;
    [SerializeField] private GameObject salir;

    [SerializeField] private GameObject textoTutorialUno;
    [SerializeField] private GameObject textoTutorialDos;
    [SerializeField] private GameObject textoTutorialTres;
    [SerializeField] private GameObject textoTutorialCuatro;
    [SerializeField] private GameObject textoTutorialCinco;
    [SerializeField] private GameObject textoTutorialSeis;

    [SerializeField] private GameObject wasdTutorial;
    [SerializeField] private GameObject shiftTutorial;
    [SerializeField] private GameObject clickTutorial;

    [SerializeField] private GameObject Win;


    
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

            GameObject diab = GameObject.Find("Diablo");
            diabloJefe = diab.GetComponent<DiabloJefe>();
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
        string scenaName = SceneManager.GetActiveScene().name;
        
        if(!atributos.getTutorialIniciado() && string.Equals(scenaName, "MundoAbierto")){
            StartCoroutine(iniciarTutorial());
            atributos.setTutorialIniciado(true);
        }
        
        if (!string.Equals(scenaName, "MundoAbierto")){
            if (Input.GetKey(KeyCode.Return)){
                Time.timeScale = 1f; 
                textoCargando.SetActive(true);
                SceneManager.LoadScene("MundoAbierto");
                atributos.setllaveEntrarMina(false);
                atributos.setllaveEntrarNieve(false);
                atributos.setllaveEntrarCastillo(false);
                atributos.setllaveEntrarFuego(false);
                atributos.setllaveEntrarColiseo(false);
                atributos.setllaveEntrarVolcan(false);
                atributos.setVidaPersonaje(1000);
            }
        }

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


        
        if(atributos.getllaveEntrarNieve() == true) {
            iconoLlaveNieve.SetActive(true);
        }

        if(atributos.getllaveEntrarMina() == true) {
            iconoLlaveMina.SetActive(true);
        }

        if(atributos.getllaveEntrarCastillo() == true) {
            iconoLlaveCastillo.SetActive(true);
        }

        if(atributos.getllaveEntrarFuego() == true) {
            iconoLlaveFuego.SetActive(true);
        }

        if(atributos.getllaveEntrarColiseo() == true) {
            iconoLlaveColiseo.SetActive(true);
        }

        if(atributos.getllaveEntrarVolcan() == true) {
            iconoLlaveVolcan.SetActive(true);
        }
        

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Locket"))
        {
            Win.SetActive(true);
            salir.SetActive(true);
        }
        if (collision.gameObject.CompareTag("salida_mina"))
        {
            SceneManager.LoadScene("MundoAbierto");
        }
        if (collision.gameObject.CompareTag("salida_castillo"))
        {
            SceneManager.LoadScene("MundoAbierto");
        }
        if (collision.gameObject.CompareTag("salida_volcan"))
        {
            SceneManager.LoadScene("MundoAbierto");
        }
        if (collision.gameObject.CompareTag("Botiquin"))
        {
            vidaPersonaje = atributos.getVidaPersonaje();
            vidaPersonaje = 1000;
            sistemaVida.setVida(vidaPersonaje);
            atributos.setVidaPersonaje(vidaPersonaje);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("llaveMina")){
            textoJefeDerrotado.SetActive(false);
            iconoJefeDerrotado.SetActive(false);
            Destroy(collision.gameObject);
            StartCoroutine(textoLlaveMina());
            atributos.setllaveEntrarMina(true);
        }
        
        if (collision.gameObject.CompareTag("llaveNieve")){
            textoJefeDerrotado.SetActive(false);
            iconoJefeDerrotado.SetActive(false);
            Destroy(collision.gameObject);
            StartCoroutine(textoLlave());
            atributos.setllaveEntrarNieve(true);
        }


        if (collision.gameObject.CompareTag("llaveFuego")){
            textoJefeDerrotado.SetActive(false);
            iconoJefeDerrotado.SetActive(false);
            Destroy(collision.gameObject);
            StartCoroutine(textoLlave());
        
            atributos.setllaveEntrarFuego(true);
        }

        if (collision.gameObject.CompareTag("llaveCastillo")){
            textoJefeDerrotado.SetActive(false);
            iconoJefeDerrotado.SetActive(false);
            Destroy(collision.gameObject);
            StartCoroutine(textoLlaveCastillo());
            atributos.setllaveEntrarCastillo(true);
        }

        if (collision.gameObject.CompareTag("llaveVolcan")){
            textoJefeDerrotado.SetActive(false);
            iconoJefeDerrotado.SetActive(false);
            Destroy(collision.gameObject);
            StartCoroutine(textoLlaveVolcan());
            atributos.setllaveEntrarVolcan(true);
        }

        if (collision.gameObject.CompareTag("llaveColiseo")){
            textoJefeDerrotado.SetActive(false);
            iconoJefeDerrotado.SetActive(false);
            Destroy(collision.gameObject);
            StartCoroutine(textoColiseo());
            atributos.setllaveEntrarColiseo(true);
        }

        if (collision.gameObject.CompareTag("PuertaNieve") && atributos.getllaveEntrarNieve() == true)
        {
            Destroy(collision.gameObject);
            
        }else if(collision.gameObject.CompareTag("PuertaNieve") && atributos.getllaveEntrarNieve() == false){
            textoFaltaLlaveNieve.SetActive(true);
            StartCoroutine(FaltaLlaveNieve());
        }

        if (collision.gameObject.CompareTag("PuertaFuego") && atributos.getllaveEntrarFuego() == true)
        {
            Destroy(collision.gameObject);
        }else if(collision.gameObject.CompareTag("PuertaFuego") && atributos.getllaveEntrarFuego() == false){
            StartCoroutine(FaltaLlaveFuego());
        }

        if (collision.gameObject.CompareTag("PuertaMina") && atributos.getllaveEntrarMina() == true)
        {
            Destroy(collision.gameObject);
            SceneManager.LoadScene("Mina");
        }else if(collision.gameObject.CompareTag("PuertaMina") && atributos.getllaveEntrarMina() == false){
            StartCoroutine(FaltaLlaveMina());
        }

        if (collision.gameObject.CompareTag("PuertaCastillo") && atributos.getllaveEntrarCastillo() == true)
        {
            Destroy(collision.gameObject);
            SceneManager.LoadScene("Castillo Nieve");
        }else if(collision.gameObject.CompareTag("PuertaCastillo") && atributos.getllaveEntrarCastillo() == false){
            StartCoroutine(FaltaLlaveCastillo());
        }

        if (collision.gameObject.CompareTag("PuertaColiseo") && atributos.getllaveEntrarColiseo() == true)
        {
            Destroy(collision.gameObject);
        }else if(collision.gameObject.CompareTag("PuertaColiseo") && atributos.getllaveEntrarColiseo() == false){
            StartCoroutine(FaltaLlaveColiseo());
        }

        if (collision.gameObject.CompareTag("PuertaVolcan") && atributos.getllaveEntrarVolcan() == true)
        {
            Destroy(collision.gameObject);
            SceneManager.LoadScene("Volcan");
        }else if(collision.gameObject.CompareTag("PuertaVolcan") && atributos.getllaveEntrarVolcan() == false){
            StartCoroutine(FaltaLlaveVolcan());
        }


    }

    public IEnumerator iniciarTutorial(){
        textoTutorialUno.SetActive(true);
        yield return new WaitForSeconds(5f);
        textoTutorialUno.SetActive(false);
         textoTutorialDos.SetActive(true);
        yield return new WaitForSeconds(5f);
        textoTutorialDos.SetActive(false);
         textoTutorialTres.SetActive(true);
         wasdTutorial.SetActive(true);
        yield return new WaitForSeconds(5f);
        textoTutorialTres.SetActive(false);
        wasdTutorial.SetActive(false);
         textoTutorialCuatro.SetActive(true);
         shiftTutorial.SetActive(true);
        yield return new WaitForSeconds(5f);
        shiftTutorial.SetActive(false);
        textoTutorialCuatro.SetActive(false);
         textoTutorialCinco.SetActive(true);
         clickTutorial.SetActive(true);
        yield return new WaitForSeconds(7f);
         clickTutorial.SetActive(false);
        textoTutorialCinco.SetActive(false);
         textoTutorialSeis.SetActive(true);
        yield return new WaitForSeconds(5f);
        textoTutorialSeis.SetActive(false);
        
    }

    public IEnumerator textoLlave(){
        textoLlaveConseguida.SetActive(true);
        iconoLlaveConseguida.SetActive(true);
        yield return new WaitForSeconds(3f);
        textoLlaveConseguida.SetActive(false);
        iconoLlaveConseguida.SetActive(false);
    }

    public IEnumerator textoLlaveMina(){
        textoLlaveConseguidaMina.SetActive(true);
        iconoLlaveConseguida.SetActive(true);
        yield return new WaitForSeconds(3f);
        textoLlaveConseguidaMina.SetActive(false);
        iconoLlaveConseguida.SetActive(false);
    }

    public IEnumerator textoLlaveCastillo(){
        textoLlaveConseguidaCastillo.SetActive(true);
        iconoLlaveConseguida.SetActive(true);
        yield return new WaitForSeconds(3f);
        textoLlaveConseguidaCastillo.SetActive(false);
        iconoLlaveConseguida.SetActive(false);
    }

    public IEnumerator textoColiseo(){
        textoLlaveConseguidaColiseo.SetActive(true);
        iconoLlaveConseguida.SetActive(true);
        yield return new WaitForSeconds(3f);
        textoLlaveConseguidaColiseo.SetActive(false);
        iconoLlaveConseguida.SetActive(false);
    }

    public IEnumerator textoLlaveVolcan(){
        textoLlaveConseguidaVolcan.SetActive(true);
        iconoLlaveConseguida.SetActive(true);
        yield return new WaitForSeconds(3f);
        textoLlaveConseguidaVolcan.SetActive(false);
        iconoLlaveConseguida.SetActive(false);
    }

    public IEnumerator FaltaLlaveMina(){
        textoFaltaLlaveMina.SetActive(true);
        yield return new WaitForSeconds(3f);
        textoFaltaLlaveMina.SetActive(false);
    }

    public IEnumerator FaltaLlaveNieve(){
        textoFaltaLlaveNieve.SetActive(true);
        yield return new WaitForSeconds(3f);
        textoFaltaLlaveNieve.SetActive(false);
    }

    public IEnumerator FaltaLlaveFuego(){
        textoFaltaLlaveFuego.SetActive(true);
        yield return new WaitForSeconds(3f);
        textoFaltaLlaveFuego.SetActive(false);
    }

    public IEnumerator FaltaLlaveCastillo(){
        textoFaltaLlaveCastillo.SetActive(true);
        yield return new WaitForSeconds(3f);
        textoFaltaLlaveCastillo.SetActive(false);
    }

    public IEnumerator FaltaLlaveColiseo(){
        textoFaltaLlaveColiseo.SetActive(true);
        yield return new WaitForSeconds(3f);
        textoFaltaLlaveColiseo.SetActive(false);
    }

    public IEnumerator FaltaLlaveVolcan(){
        textoFaltaLlaveVolcan.SetActive(true);
        yield return new WaitForSeconds(3f);
        textoFaltaLlaveVolcan.SetActive(false);
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
                botonReinicio.SetActive(true);
                botonVolverAlMenu.SetActive(true);
                Time.timeScale = 0f;
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
                botonReinicio.SetActive(true);
                botonVolverAlMenu.SetActive(true);
                Time.timeScale = 0f;

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
                botonReinicio.SetActive(true);
                botonVolverAlMenu.SetActive(true);
                Time.timeScale = 0f;
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
                botonReinicio.SetActive(true);
                botonVolverAlMenu.SetActive(true);
                Time.timeScale = 0f;
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
                botonReinicio.SetActive(true);
                botonVolverAlMenu.SetActive(true);
                Time.timeScale = 0f;
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
                botonReinicio.SetActive(true);
                botonVolverAlMenu.SetActive(true);
                Time.timeScale = 0f;
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
                botonReinicio.SetActive(true);
                botonVolverAlMenu.SetActive(true);
                Time.timeScale = 0f;
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
                botonReinicio.SetActive(true);
                botonVolverAlMenu.SetActive(true);
                Time.timeScale = 0f;
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
                botonReinicio.SetActive(true);
                botonVolverAlMenu.SetActive(true);
                Time.timeScale = 0f;
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
                botonReinicio.SetActive(true);
                textoReiniciar.SetActive(true);
                Time.timeScale = 0f;
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
                botonReinicio.SetActive(true);
                textoReiniciar.SetActive(true);
                Time.timeScale = 0f;

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
                botonReinicio.SetActive(true);
                textoReiniciar.SetActive(true);
                Time.timeScale = 0f;
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
        Time.timeScale = 1f;
        textoCargando.SetActive(true);
        SceneManager.LoadScene("MundoAbierto");
        atributos.setllaveEntrarMina(false);
        atributos.setllaveEntrarNieve(false);
        atributos.setllaveEntrarCastillo(false);
        atributos.setllaveEntrarFuego(false);
        atributos.setllaveEntrarColiseo(false);
        atributos.setllaveEntrarVolcan(false);
        atributos.setVidaPersonaje(1000);
    
    }

    public void PausarJuego(){
        panelMenuPausa.SetActive(true);
        Time.timeScale = 0f;  
    }

    public void ContinuarJuego(){
        Time.timeScale = 1f;  
        panelMenuPausa.SetActive(false);
    }

    public void MenuPrincipal(){
        Time.timeScale = 1f;  
        SceneManager.LoadScene("Menu");
    }
}
