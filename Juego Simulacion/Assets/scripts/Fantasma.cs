using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;


public class Fantasma : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private int cantidad;
    [SerializeField] private int multiplicador;
    [SerializeField] private int controlMovimiento;

    private Animator anim;
    private Rigidbody2D rig;
    private SpriteRenderer spritPersonaje;
    private float[] numeros;
    private int i;


    private Vector2 targetPosition;
    private float elapsedFrames = 0f;
    private bool objetivoDetectado;

    public Transform personaje;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        spritPersonaje = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        i = 0;
        numeros = generateNumbers();
        targetPosition = GetRandomTargetPosition();
    
    }

    private void Update()
    {
        rig = GetComponent<Rigidbody2D>();

        elapsedFrames++;
        if (controlMovimiento > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(personaje.position.x,personaje.position.y), velocidad);
        }else{
            if (elapsedFrames >= 60f)
            {
            elapsedFrames = 0f;
            targetPosition = GetRandomTargetPosition();
            }
        }
        

        MoveTowardsTarget();
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

        


        if (value <= 0.25f)
        {
            x += 2f;
             
            spritPersonaje.flipX = true;

        }
        else if (value > 0.25f && value <= 0.5f)
        {
            x -= 2f;
            spritPersonaje.flipX = false;
        }
        else if (value > 0.5f && value <= 0.75f)
        {
            y += 2f;
        }
        else
        {
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
}
