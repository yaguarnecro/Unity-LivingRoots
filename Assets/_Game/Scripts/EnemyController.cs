using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public EstadoEnemigo estado = EstadoEnemigo.Patrullando;

    public Transform OjosEnemigo;
    public float distanciaVisual;
    public GameObject FantasmaDelJugador;

    private NavMeshAgent agente;
    private Transform jugador;
    private Vector3 ultimaPosicion;
    

    private void Start()
    {
        agente = GetComponent<NavMeshAgent>();

    }
    private void Update()
    {
        switch (estado)
        {
            case EstadoEnemigo.Patrullando:
                Patrullar();
                break;
            case EstadoEnemigo.Persiguientdo:
                Perseguir();
                break;
            case EstadoEnemigo.Atacando:
                Atacar();
                break;
            case EstadoEnemigo.Buscando:
                Buscar();
                break;
        }
    }
    public void Patrullar()
    {

    }
    public void Perseguir()
    {
        agente.SetDestination(jugador.position);
    }
    public void Atacar()
    {

    }
    public void Buscar()
    {
        agente.SetDestination(ultimaPosicion);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("player"))
        {
            Vector3 rayoAlEnemigo = other.transform.position - OjosEnemigo.position;;
            Ray rayo = new Ray(OjosEnemigo.position, rayoAlEnemigo + Vector3.up);

            if(Physics.Raycast(rayo, out RaycastHit hit, distanciaVisual))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    //lo vio
                    estado = EstadoEnemigo.Persiguientdo;
                    jugador = other.transform;

                    if(estado == EstadoEnemigo.Buscando)
                    {
                        FantasmaDelJugador.SetActive(false);
                    }
                }
                else
                {
                    //no lo vio, por obstaculo
                    if(estado== EstadoEnemigo.Persiguientdo || estado == EstadoEnemigo.Atacando)
                    {
                        estado = EstadoEnemigo.Buscando;
                        FantasmaDelJugador.transform.position = ultimaPosicion;
                        FantasmaDelJugador.SetActive(true);
                    }
                }
            }



            
        }
    }
}
