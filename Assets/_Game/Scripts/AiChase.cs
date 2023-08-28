using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChase : MonoBehaviour
{
    
    [SerializeField] 
    public GameObject player;
    Animator enemyAnimatorX;
    // agro range
    [SerializeField]
    public float distanceBetween;
    // movement speed
    [SerializeField]
    public float speed;
    public Rigidbody2D rb;
    public Vector3 direction;

    public EstadosEnemigo estado = EstadosEnemigo.Patrullando;
    public Transform OjosEnemigo;
    public float distanciaVisual;
    public bool isFacingLeft = true;
    public LayerMask layermaskRay;

    public Transform jugador;

    //actual distance
    private float distance;
 
    private bool Rightflip = true;

    


  



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //jugador = 
        //enemyAnimatorX = player.GetComponent<Animator>();
       

    }

    // Update is called once per frame
    void Update()
    {
        //distance between 2 transforms as a float
        distance = Vector3.Distance(transform.position, player.transform.position);

        // player positon - actual position of the enemy
        direction = player.transform.position - transform.position;

        ////normalize direction
        ////direction.Normalize();
        ////get the angle bewteen 2 points, and make it a degree
        //if(distance < distanceBetween)
        //{

        //    // follow & flip
        //    ChasingPlayer();

        //}
        //else
        //{
        //    stopChasingPlayer();
        //}

        if (CanSeePlayer(distanceBetween))
        {
            ChasingPlayer();
        }
        else
        {
            stopChasingPlayer();
        }


        switch (estado)
        {
            case EstadosEnemigo.Patrullando:
                Patrullar();
                break;
            case EstadosEnemigo.Persiguiendo:
                Perseguir();
                break;
            case EstadosEnemigo.Buscando:
                Buscar();
                break;
            case EstadosEnemigo.Descanzando:
                Descanzar();
                break;
            case EstadosEnemigo.Atacando:
                Atacar();
                break;
            case EstadosEnemigo.Cayendo:
                Caer();
                break;
            case EstadosEnemigo.Muriendo:
                Morir();
                break;
        }
    }

    void flip()
    {
        Rightflip = !Rightflip;
        isFacingLeft = !isFacingLeft;
        Vector3 Scalar = transform.localScale;
        Scalar.x *= -1;
        transform.localScale = Scalar;
    }

    void ChasingPlayer()
    {
        //flip
        if (Rightflip && direction.x > 0) { flip(); }
        else if (!Rightflip && direction.x < 0) { flip(); }
        //chase
        //enemyAnimatorX.Play("Running");

        transform.position = Vector3.MoveTowards(this.transform.position,
        player.transform.position, speed * Time.deltaTime);
    }
    void stopChasingPlayer()
    {
        rb.velocity = Vector3.zero;
        //enemyAnimatorX.Play("Idle");
    }

    private void Patrullar()
    {

    }

    private void Perseguir()
    {
        if (distance < distanceBetween)
        {

            // follow & flip
            ChasingPlayer();

        }
        else
        {
            stopChasingPlayer();
        }
    }

    private void Descanzar()
    {

    }

    private void Buscar()
    {

    }

    private void Atacar()
    {

    }

    private void Caer()
    {

    }

    private void Morir()
    {

    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))

        {

            
            //jugador entra al area
            Debug.Log("Entro en area de vision x");

            //Vector3 rayoAlEnemigo = other.transform.position - OjosEnemigo.position; ;
            //Ray rayo = new Ray(OjosEnemigo.position, rayoAlEnemigo + Vector3.up);
            //Debug.DrawRay2D(rayo.origin, rayo.direction * distanciaVisual, Color.green);


            CanSeePlayer(distanciaVisual);

            //if (Physics.Raycast(rayo, out RaycastHit hit, distanciaVisual))
            //{
            //    //rayo detecta algo
            //    if (hit.collider.CompareTag("Player"))
            //    {
            //        Debug.Log("rayo lo toco");
            //        //lo vio !
            //        estado = EstadosEnemigo.Persiguiendo;
            //        jugador = other.transform;

            //        //if (estado == EstadosEnemigo.Buscando)
            //        //{
            //            //FantasmaDelJugador.SetActive(false);
            //        //}
            //    }
            //    else
            //    {
            //        //no lo vio, por obstaculo
            //        if (estado == EstadosEnemigo.Persiguiendo || estado == EstadosEnemigo.Atacando)
            //        {
            //            estado = EstadosEnemigo.Buscando;
            //            //FantasmaDelJugador.transform.position = ultimaPosicion;
            //            //FantasmaDelJugador.SetActive(true);
            //            Debug.Log("rayo no lo toco");
            //        }
            //    }
            //}




        }
    }

    bool CanSeePlayer(float distanciaVisual)
    {
        bool val = false;
        float castDist = distanciaVisual;
        if (isFacingLeft)
        {
            castDist = -distanciaVisual;
        }


        Vector2 endPos = OjosEnemigo.position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(OjosEnemigo.position, endPos, layermaskRay);
        Debug.DrawLine(OjosEnemigo.position, endPos, Color.magenta);


        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
                Debug.Log("Entro en area de vision x2d");
            }
            else
            {
                val = false;
                Debug.Log("Salio en area de vision x2d");
            }
            Debug.DrawLine(OjosEnemigo.position,hit.point, Color.red);
        }
        else
        {
            Debug.DrawLine(OjosEnemigo.position, endPos, Color.blue);
        }
        return val;
    }

}


