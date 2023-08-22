using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChase : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float distanceBetween;

    private float distance;
    private bool Rightflip = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //distance between 2 transforms as a float
        distance = Vector3.Distance(transform.position, player.transform.position);
        // player positon - actual position of the enemy
        Vector3 direction = player.transform.position - transform.position;
        //normalize direction
        //direction.Normalize();
        //get the angle bewteen 2 points, and make it a degree
        if(distance < distanceBetween)
        {
            if (Rightflip && direction.x > 0) { flip(); }
            else if (!Rightflip && direction.x < 0) { flip(); }
            // follow
            transform.position = Vector3.MoveTowards(this.transform.position, 
                player.transform.position, speed * Time.deltaTime);

        }

                    
        
        // flipping?xxxx
       // transform.rotation = Quaternion.Euler(Vector3.forward * angle);xxxx
       
        

    }

    void flip()
    {
        Rightflip = !Rightflip;
        Vector3 Scalar = transform.localScale;
        Scalar.x *= -1;
        transform.localScale = Scalar;
    }
}
