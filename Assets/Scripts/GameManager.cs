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
    [SerializeField] ParticleSystem particleSystem;
    public int Score { get => score; set { score = value; } }
    public int PointsToAdd { get => pointsToAdd; set { pointsToAdd = value; } }

    // activate all win panels
    public IEnumerator Win()
    {
        victoryImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        scoreText.gameObject.SetActive(true);
        ShowScore();
        yield return new WaitForSeconds(0.5f);
        nextLevel.gameObject.SetActive(true);
    }
    //show score
    void ShowScore()
    {
        scoreText.text = "SCORE: \n" + score;
    }
    // go to next level, in this case just reload this scene
    public void NextLevel()
    {
        SceneManager.LoadScene(0);
    }
}
