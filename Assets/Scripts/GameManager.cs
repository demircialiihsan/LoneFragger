using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject gameOverPanel;
    public GameObject plusTenPrefab;

    public Text scoreText;
    private int score = 0;

    void Start()
    {
        UpdateScore();
    }

    public IEnumerator EndGame()
    {
        GetComponent<EnemySpawner>().DisableEnemies();
        GetComponent<HealthPackSpawner>().gameEnded = true;

        yield return new WaitForSeconds(2f);

        gameOverPanel.SetActive(true);
    }

    public void UpdateScore(int amount = 0)
    {
        score += amount;
        scoreText.text = score.ToString();
    }
}
