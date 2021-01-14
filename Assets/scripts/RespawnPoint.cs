using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
[SerializeField] private Transform player;
[SerializeField] private Transform respawnPoint;
Rigidbody playerBallRB;

    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            Debug.Log("Yo gamerz");
            player.transform.position = respawnPoint.position;

        }

        //if(playerBallRB.IsSleeping())
            //{
                //Debug.Log("Yo gamerz");
                //player.transform.position = respawnPoint.position;

            //}


    }
}
