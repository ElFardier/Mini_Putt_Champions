using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoringTrigger : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip clip;
    public float volume=0.2f;
    

    // Start is called before the first frame update
    // Update is called once per frame
   private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) 
    {

        Debug.Log("RIGHT IN THE HOLE !!!!");
        if (other.gameObject.tag == "Player") 
        {
            StartCoroutine(Wait());
            audioSource.PlayOneShot(clip, volume);
        }
    }
    
    public IEnumerator Wait()
    {
  	 yield return new WaitForSeconds(5f);
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
        