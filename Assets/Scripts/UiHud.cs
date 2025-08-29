using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiHud : MonoBehaviour
{
    public Gun gun;

    public Text ammoText;
    public Text scoreText;
    public Text waveText;
    public GameObject gameOver;
    public PlayerHealth playerHealth;

    public int Score { get; private set; }
    public int WaveCount { get; private set; }
    public int zombieCount { get; private set; }
    public void Awake()
    {
        Score = 0;
        WaveCount = 1;
        zombieCount = 1;
        gameOver.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Update()
    {
        if (playerHealth.IsDead)
        {
            gameOver.SetActive(true);
            Time.timeScale = 0f;
        }

        scoreText.text = $"SCORE : {Score}";
        waveText.text = $"Wave: {WaveCount}\nEnemy Left : {zombieCount}";
        if (Input.GetKeyUp(KeyCode.P))
        {
            AddScore(10);
            
        }
        AmmoText();
    }

    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void WaveText()
    {
        
    }

    public void AmmoText()
    {
        ammoText.text = $"{gun.magAmmo}/{gun.ammoRemain}";
    }

    public void AddScore(int score)
    {
        Score += score;
    }
}
