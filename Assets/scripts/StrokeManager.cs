using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StrokeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindPlayerBall();
        StrokeAngle = 0f;
        StrokeCount = 0;
    }

    public float StrokeAngle { get; protected set;}
    public float StrokeForce { get; protected set;}
    public float StrokeForcePerc { get { return StrokeForce/MaxStrokeForce; } }

    public int StrokeCount { get; protected set; }

    float StrokeForceFillSpeed = 15f;
    int fillDir = 1;
    float MaxStrokeForce = 20f;

    public AudioSource audioSource;
    public AudioClip clip;
    public float volume=0.2f;

    public enum StrokeModeEnum 
    { 
        AIMING,
        FILL_UP,
        READY_SHOOT, 
        BALL_ROLLING 
    };

    
    public StrokeModeEnum StrokeMode { get; protected set; }
    Rigidbody playerBallRB;
    
    private void FindPlayerBall() 
    {
       GameObject go = GameObject.FindGameObjectWithTag("Player");

       if(go == null)
       {
           Debug.LogError("Ball not found. ");
       }

       playerBallRB = go.GetComponent<Rigidbody>();

    }

   // Update is called once per visual frame Input

    private void Update() 
    {

        if(StrokeMode == StrokeModeEnum.AIMING)
        {
            StrokeAngle += Input.GetAxis("Horizontal") * 100f * Time.deltaTime;
            if(Input.GetButtonUp("Fire")) 
            {
                StrokeMode = StrokeModeEnum.FILL_UP;
                return;
            }

        }

        if(StrokeMode == StrokeModeEnum.FILL_UP)
        {
            StrokeForce += (StrokeForceFillSpeed * fillDir) *Time.deltaTime;
            if(StrokeForce > MaxStrokeForce)
            {
                StrokeForce = MaxStrokeForce;
                fillDir = -1;
            }
            else if (StrokeForce < 0)
            {
                StrokeForce = 0;
                fillDir = 1;
            }

            
            if(Input.GetButton("Cancel"))
            {
                StrokeMode = StrokeModeEnum.AIMING;
                StrokeForce = 0;
                return;
            } 


            if(Input.GetButtonUp("Fire"))  
            {
                StrokeMode = StrokeModeEnum.READY_SHOOT;
                audioSource.PlayOneShot(clip, volume);
            }

        }
        
        if(Input.GetButtonUp("Quit")) 
        {
            SceneManager.LoadScene(0);
        }

    }

    void CheckRollStatus() 
    {
        // Is the ball roll ?
        if(playerBallRB.IsSleeping())
        {
            StrokeMode = StrokeModeEnum.AIMING;
        }
    
    }

   // FixedUpdate runs on every tick of the physics engine, Manipulation
    void FixedUpdate()
    {

        if(StrokeMode == StrokeModeEnum.BALL_ROLLING)
        {
            CheckRollStatus();
            return;
        }

        if(StrokeMode != StrokeModeEnum.READY_SHOOT)
        {
            return;

        }

        // Ball roll

        Vector3 forceVec = new Vector3(StrokeForce, 0, 0);

        playerBallRB.AddForce(Quaternion.Euler(0, StrokeAngle, 0) * forceVec, ForceMode.Impulse);

        StrokeForce = 0f;
        fillDir = 1;
        StrokeCount++;
        StrokeMode = StrokeModeEnum.BALL_ROLLING; 

    }
}
