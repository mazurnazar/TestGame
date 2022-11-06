using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float speed;
    public float Speed { get => speed; set { speed = value; } }
    [SerializeField] private float maxSpeed;
    public float MaxSpeed { get => maxSpeed; private set { } }
    private bool canMove;
    public bool CanMove { get => canMove; set { canMove = value; } }

    private bool doubleSpeed;
    public bool DoubleSpeed { get => doubleSpeed; private set { } }

    [SerializeField] ParticleSystem points;
    public ParticleSystem Points { get => points; private set { } }
    RotatePlayer rotatePlayer;
    [SerializeField] GameObject manager;
    GameManager gameManger;
    private bool canPlayParticles;
    public bool CanPlayParticles { get => canPlayParticles; set { canPlayParticles = value; } }
  
    // Start is called before the first frame update
    void Start()
    {
        gameManger = manager.GetComponent<GameManager>();
        rotatePlayer = GetComponent<RotatePlayer>();
        speed = 40;
        maxSpeed = 80;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) MoveForward();
        if (speed == maxSpeed) doubleSpeed = true; 
    }
    // move player with speed
    void MoveForward()
    {
        transform.position += Vector3.forward * Time.deltaTime * speed;
        if (!rotatePlayer.IsRotating&&CanPlayParticles)
            PlayParticles();
        else StopParticles();
    }
    // show particle system with points
    public void PlayParticles()
    {
        if (canMove)
        {
            points.Play();
            points.startSpeed = speed;
            points.emissionRate = speed / 8;
        }
        else StopParticles();
    }
    public void StopParticles()
    {
        points.Stop();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // if triggers with finish line
        if(other.tag=="Win")
        {
            canMove = false;
            StopParticles();
            StartCoroutine( gameManger.Win());
        }
    }
}
