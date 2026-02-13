using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI enemyText;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI healthText;


    int Enemies = 0;

    PlayerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
        
        enemyText.text = "Enemies: " + Enemies;
        healthText.text = "Health: " + player.Health;

        if (player != null)
        {
            coinText.text = "Coins: " + player.coinCount;
        }


        if (player == null)
        {
            Debug.Log("Player not found in GameManager");
            player = FindFirstObjectByType<PlayerController>();
        }
        
    }


    void ChangeEnemyCount(int amount)
    {
        Enemies += amount;
        enemyText.text = "Enemies: " + Enemies;
        if (Enemies <= 0)
        {
            SceneManager.LoadScene("Level2");
        }
    }

    public void coinCountChanged(int newCount)
    {
        coinText.text = "Coins: " + newCount;
    }

    public void healthChanged(int newHealth)
    {
        healthText.text = "Health: " + newHealth;
    }
        /*if (newHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        */


    public void addEnemy()
    {
        Enemies += 1;
    }
}
