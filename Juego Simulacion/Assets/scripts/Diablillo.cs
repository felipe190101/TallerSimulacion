using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;


public class Diablillo : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private int cantidad;
    [SerializeField] private int multiplicador;
    [SerializeField] public int daño;
    [SerializeField] public float escalaX;
    [SerializeField] public float escalaY;

    private Animator anim;
    private Rigidbody2D rig;
    private SpriteRenderer spritPersonaje;
    private float[] numeros;
    private int i;

    private NavMeshAgent agente;


    private Vector2 targetPosition;
    private float elapsedFrames = 0f;
    private bool objetivoDetectado;

    public Transform personaje;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        spritPersonaje = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        agente = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        i = 0;
        numeros = generateNumbers();
        targetPosition = GetRandomTargetPosition();
        agente.updateRotation = false;
        agente.updateUpAxis = false;
    
    }

    private void Update()
    {
        rig = GetComponent<Rigidbody2D>();

        this.transform.position = new Vector3(transform.position.x,transform.position.y,0);
        float distancia = Vector3.Distance(personaje.position, transform.position);

        cambioDaño();

        if(distancia < 2) {
            agente.SetDestination(personaje.position);
            if(this.transform.position.x > personaje.position.x) {
                transform.localScale = new Vector2(-escalaX,escalaY);  
            }else{
                transform.localScale = new Vector2(escalaX,escalaY);  
            }
        }else{
            elapsedFrames++;
            if (elapsedFrames >= 60f){
                elapsedFrames = 0f;
                targetPosition = GetRandomTargetPosition();

                MoveTowardsTarget();
            }

        }
    
        

        
    }

    private void MoveTowardsTarget()
    {
        Vector2 currentPosition = transform.position;
        Vector2 direction = (targetPosition - currentPosition).normalized;
        rig.velocity = direction * velocidad;
    }

    private Vector2 GetRandomTargetPosition()
    {
        float value = numeros[i];
        i++;

        Vector2 currentPosition = transform.position;
        float x = currentPosition.x;
        float y = currentPosition.y;

        if (value <= 0.25f){
            x += 2f;
            spritPersonaje.flipX = false;
        }
        else if (value > 0.25f && value <= 0.5f){   
            x -= 2f;
            spritPersonaje.flipX = true;
        }
        else if (value > 0.5f && value <= 0.75f){    
            y += 2f;
        }else{   
            y -= 2f;
        }

        return new Vector2(x, y);
    }

    private float[] generateNumbers()
    {
        numeros = new float[cantidad];
        double modulo = Mathf.Pow(2, 31) - 1;
        double semilla = System.DateTime.Now.Ticks % (int)modulo;
        for (int i = 0; i < cantidad; i++)
        {
            semilla = (multiplicador * semilla) % modulo;
            double randomNum = semilla / modulo;
            numeros[i] = (float)randomNum;
        }

        return numeros;
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            anim.SetTrigger("Ataca");
        }
    }

    private void cambioDaño () {
        //montecarlo();
    }

    private void montecarlo()
    {
        int debil = 0;
        int medio = 0;
        int fuerte = 0;

        float[] aleatorios = numeros;
        for (int i = 0; i < 100; i++)
        {
            switch (aleatorios[i])
            {
                case var n when (n >= 0 && n <= 0.4f):
                    debil++;
                    break;
                    
                case var n when (n > 0.4f && n <= 0.9f):
                    medio++;
                    break;
                    
                case var n when (n > 0.9f && n <= 1f):
                    fuerte++;
                    break;
 
                default:
                    break;
            }
        }

        float probDebil = debil/100;
        float probMedio = medio/100;
        float probFuerte = fuerte/100;

        float dato = numeros[100];
        switch (dato)
            {
                case var n when (n >= 0 && n <= probDebil):
                    daño = 2;
                    break;
                    
                case var n when (n > probDebil && n <= probDebil + probMedio):
                    daño = 5;
                    break;
                    
                case var n when (probDebil + probMedio > 0.9f && n <= 1f):
                     daño = 10;
                    break;
 
                default:
                    break;
            }
    }

}
