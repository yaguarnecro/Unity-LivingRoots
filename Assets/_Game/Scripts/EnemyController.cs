using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public EstadosEnemigo estado = EstadosEnemigo.Patrullando;

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
            case EstadosEnemigo.Patrullando:
                Patrullar();
                break;
            case EstadosEnemigo.Persiguiendo:
                Perseguir();
                break;
            case EstadosEnemigo.Atacando:
                Atacar();
                break;
            case EstadosEnemigo.Buscando:
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
            Debug.DrawRay(rayo.origin, rayo.direction * distanciaVisual, Color.green);

            if (Physics.Raycast(rayo, out RaycastHit hit, distanciaVisual))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    //lo vio
                    estado = EstadosEnemigo.Persiguiendo;
                    jugador = other.transform;

                    if(estado == EstadosEnemigo.Buscando)
                    {
                        FantasmaDelJugador.SetActive(false);
                    }
                }
                else
                {
                    //no lo vio, por obstaculo
                    if(estado== EstadosEnemigo.Persiguiendo || estado == EstadosEnemigo.Atacando)
                    {
                        estado = EstadosEnemigo.Buscando;
                        FantasmaDelJugador.transform.position = ultimaPosicion;
                        FantasmaDelJugador.SetActive(true);
                    }
                }
            }



            
        }
    }
}
