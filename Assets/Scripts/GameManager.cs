using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Image victoryImage;
    [SerializeField] Text scoreText;
    [SerializeField] Button nextLevel;

    [SerializeField] private int score;
    private int pointsToAdd;
    public int PointsToAdd { get => pointsToAdd; set { pointsToAdd = value; } }
    public int Score { get => score; set { score = value; } }
    private bool showParticles;
    public bool ShowParticles { get => showParticles; set { showParticles = value; } }
    public IEnumerator Win()
    {
        victoryImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        scoreText.gameObject.SetActive(true);
        ShowScore();
        yield return new WaitForSeconds(0.5f);
        nextLevel.gameObject.SetActive(true);
    }
    void ShowScore()
    {
        scoreText.text = "SCORE: \n" + score;
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(0);
    }
}
