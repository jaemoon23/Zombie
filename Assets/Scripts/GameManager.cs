using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public Uimanager uiManager;

    public ZombieSpawner zombieSpawner;
    public ItemSpawner itemSpawner;

    private int score;
    
    public bool IsGameOver { get; private set; }

    private void OnEnable()
    {
        Time.timeScale = 1f;
    }
    private void Start()
    {
        var findGo = GameObject.FindWithTag(Define.Player);
        var playerHealth = findGo.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.OnDeath += EndGame;
        }
    }

    public void AddScore(int add)
    {
        score += add;
        uiManager.SetUpdateScore(score);
    }

    public void EndGame()
    {
        IsGameOver = true;
        uiManager.SetActiveGameOverUi(true);
        zombieSpawner.enabled = false;
        itemSpawner.enabled = false;
        Time.timeScale = 0f;
    }
}
