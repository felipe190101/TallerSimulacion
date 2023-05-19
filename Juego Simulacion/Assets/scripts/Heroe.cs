using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool isSprinting;
    private float sprintSpeed;

    private Fantasma fantasma;
    private Murcielago murcielago;
    private Esqueleto esqueleto;
    private Diablo diablo;
    private Diablillo diablillo;
    private Goblin goblin;
    private Hongo hongo;
    private Gusano gusano;
    private Slime slime;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        spritPersonaje = GetComponentInChildren<SpriteRenderer>();
        sistemaVida = GetComponent<vida>();
        sistemaVida.setVida(vidaPersonaje);
        sistemaVida.iniciarVIda();

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

    public void CausarHeridaFantasma()
    {
        if (vidaPersonaje > 0)
        {
            vidaPersonaje -= fantasma.daño;
            sistemaVida.setVida(vidaPersonaje);

            if (vidaPersonaje <= 0)
            {
                textGameOver.SetActive(true);
                iconMuerte.SetActive(true);
            }
        }
    }

    public void CausarHeridaEsqueleto()
    {
        if (vidaPersonaje > 0)
        {
            vidaPersonaje -= esqueleto.daño;
            sistemaVida.setVida(vidaPersonaje);

            if (vidaPersonaje <= 0)
            {
                textGameOver.SetActive(true);
                iconMuerte.SetActive(true);
            }
        }
    }

    public void CausarHeridaMurcielago()
    {
        if (vidaPersonaje > 0)
        {
            vidaPersonaje -= murcielago.daño;
            sistemaVida.setVida(vidaPersonaje);

            if (vidaPersonaje <= 0)
            {
                textGameOver.SetActive(true);
                iconMuerte.SetActive(true);
            }
        }
    }

    public void CausarHeridaDiablillo()
    {
        if (vidaPersonaje > 0)
        {
            vidaPersonaje -= diablillo.daño;
            sistemaVida.setVida(vidaPersonaje);

            if (vidaPersonaje <= 0)
            {
                textGameOver.SetActive(true);
                iconMuerte.SetActive(true);
            }
        }
    }

    public void CausarHeridaDiablo()
    {
        if (vidaPersonaje > 0)
        {
            vidaPersonaje -= diablo.daño;
            sistemaVida.setVida(vidaPersonaje);

            if (vidaPersonaje <= 0)
            {
                textGameOver.SetActive(true);
                iconMuerte.SetActive(true);
            }
        }
    }

    public void CausarHeridaGoblin()
    {
        if (vidaPersonaje > 0)
        {
            vidaPersonaje -= goblin.daño;
            sistemaVida.setVida(vidaPersonaje);

            if (vidaPersonaje <= 0)
            {
                textGameOver.SetActive(true);
                iconMuerte.SetActive(true);
            }
        }
    }

    public void CausarHeridaHongo()
    {
        if (vidaPersonaje > 0)
        {
            vidaPersonaje -= hongo.daño;
            sistemaVida.setVida(vidaPersonaje);

            if (vidaPersonaje <= 0)
            {
                textGameOver.SetActive(true);
                iconMuerte.SetActive(true);
            }
        }
    }

    public void CausarHeridaGusano()
    {
        if (vidaPersonaje > 0)
        {
            vidaPersonaje -= gusano.daño;
            sistemaVida.setVida(vidaPersonaje);

            if (vidaPersonaje <= 0)
            {
                textGameOver.SetActive(true);
                iconMuerte.SetActive(true);
            }
        }
    }

    public void CausarHeridaSlime()
    {
        if (vidaPersonaje > 0)
        {
            vidaPersonaje -= slime.daño;
            sistemaVida.setVida(vidaPersonaje);

            if (vidaPersonaje <= 0)
            {
                textGameOver.SetActive(true);
                iconMuerte.SetActive(true);
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
}
