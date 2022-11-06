using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObstacleController : MonoBehaviour
{
    [SerializeField] Texture sprite;
    [SerializeField] Material textMaterial;
    [SerializeField] Text text;
    [SerializeField] int points;
    [SerializeField] int pointsPerTime;
    public int Points { get=> points; set { points = value; } }
    public int PointsPerTime { get => pointsPerTime; private set { } }

    
    [SerializeField] GameObject manager;
    GameManager gameManager;
    [SerializeField] GameObject player;
    MovePlayer movePlayer;

    private void Awake()
    {
        gameManager = manager.GetComponent<GameManager>();
        movePlayer = player.GetComponent<MovePlayer>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            movePlayer.CanPlayParticles = true;
            gameManager.PointsToAdd = pointsPerTime;
            ChangePoints();
            InvokeRepeating("AddPointsPerSecond", 0, 1);
            StartCoroutine(AddPoints());
        }
    }
    // add point to total score every second
    void AddPointsPerSecond()
    {
        if(movePlayer.CanMove)
        gameManager.Score += pointsPerTime * (movePlayer.DoubleSpeed ? 2 : 1);
    }
    void ChangePoints()
    {
        textMaterial.mainTexture = sprite;
    }
    // add points to total score when passes obstacle
    IEnumerator AddPoints()
    {
        text.gameObject.SetActive(true);
        text.text = "+" + points;
        gameManager.Score += points;
        yield return new WaitForSeconds(1f);
        text.gameObject.SetActive(false);
    }
    
}
