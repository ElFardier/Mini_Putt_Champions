using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftTheFlag : MonoBehaviour
{
    [SerializeField] private Animator FlagAnimation;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            FlagAnimation.SetBool("LiftTheFlag", true);
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            FlagAnimation.SetBool("LiftTheFlag", false);
        }
    }
}
