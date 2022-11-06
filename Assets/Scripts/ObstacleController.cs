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
    GameManager gameManager;
    [SerializeField] GameObject manager;
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
            gameManager.ShowParticles = true;
            gameManager.PointsToAdd = pointsPerTime;
            ChangePoints();
            InvokeRepeating("AddPointsPerSecond", 0, 1);
            StartCoroutine(AddPoints());
        }
    }
    void AddPointsPerSecond()
    {
        gameManager.Score += pointsPerTime * (movePlayer.DoubleSpeed ? 2 : 1);
    }
    void ChangePoints()
    {
        textMaterial.mainTexture = sprite;
    }
    IEnumerator AddPoints()
    {
        text.gameObject.SetActive(true);
        text.text = "+" + points;
        gameManager.Score += points;
        yield return new WaitForSeconds(1f);
        text.gameObject.SetActive(false);
    }
    
}
