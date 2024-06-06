using UnityEngine;
using UnityEngine.UI;
using Unity.Collections;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    public PlayerController player;
    public EnemyController enemy;
    public TMP_Text playerHealthText;
    public TMP_Text enemyHealthText;

    void Update()
    {
        playerHealthText.text = "Player Health: " + player.health;
        enemyHealthText.text = "Enemy Health: " + enemy.health;
    }
}
