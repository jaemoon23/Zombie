using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Item : MonoBehaviour, IItem
{
    public enum Types
    {
        Coin,
        Ammo,
        Health,
    }

    public Types itemType;
    public int coinValue = 10;
    public int ammoValue = 20;
    public int healValue = 20;
    GameManager gameManager;

    private void Start()
    {
        var findGo = GameObject.FindWithTag("GameController");
        gameManager = findGo.GetComponent<GameManager>();
    }

    public void Use(GameObject go)
    {
        switch (itemType)
        {
            case Types.Coin:
                {
                    gameManager.AddScore(coinValue);
                }
                break;
            case Types.Ammo:
                {
                    var shooter = go.GetComponent<PlayerShooter>();
                    if (shooter != null)
                    {
                        shooter.gun.AddAmmo(ammoValue);
                    }
                }
                break;
            case Types.Health:
                {
                    var playerHealth = go.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.Heal(healValue);
                    }
                }
                break;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Define.Player))
        {
            Use(other.gameObject);
            Destroy(gameObject);
        }
    }

}
