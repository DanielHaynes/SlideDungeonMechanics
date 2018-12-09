using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct PlayerData
{
    int currentHealth;
    int maxPlayerHealth;
    int playerScore;
    public PlayerData(int maxHealth) {
        currentHealth = maxHealth;
        maxPlayerHealth = maxHealth;
        playerScore = 0;
    }

    public int GetMaxHealth() {
        return maxPlayerHealth;
    }

    public int GetCurrentHealth() {
        return currentHealth;
    }

    public int GetPlayerScore() {
        return playerScore;
    }

    public void IncreaseMaxPlayerHealth(int newMaxHealth) {
        maxPlayerHealth = newMaxHealth;
        currentHealth = maxPlayerHealth;
    }

    public void IncreaseHealth(int healthChange) {
        currentHealth += healthChange;
        if (currentHealth > maxPlayerHealth) currentHealth = maxPlayerHealth;
    }

    public void DecreaseHealth(int healthChange) {
        currentHealth -= healthChange;
        if (currentHealth <= 0) currentHealth = 0;
    }

    public void UpdatePlayerScore(int score) {
        playerScore += score;
    }
} 

public class PlayerManager : MonoBehaviour {

    PlayerData currentPlayer;

    public void CreatePlayer(int startingHealth) {
        currentPlayer = new PlayerData(startingHealth);
    }
    
    public int GetPlayerHealth() {
        return currentPlayer.GetCurrentHealth();
    }

    public int GetPlayerScore() {
        return currentPlayer.GetPlayerScore();
    }

    /// <summary>
    /// This function will add to the player's max health, if the amount of increase causes the player to surpass the current max health.
    /// </summary>
    /// <param name="healthChange"></param>
    /// <returns></returns>
    public bool IncreasePlayerMaxHealth(int healthChange) {
        int currentHealth = currentPlayer.GetCurrentHealth();
        int newCombinedHealth = currentHealth + healthChange;
        if (newCombinedHealth  > currentPlayer.GetMaxHealth()) {
            currentPlayer.IncreaseMaxPlayerHealth(newCombinedHealth);
            return true;
        }else {
            IncreasePlayerHealth(healthChange);
            return false;
        }
    }
   
    public void IncreasePlayerHealth(int increase) {
        currentPlayer.IncreaseHealth(increase);
    }

    public void DecreasePlayerHealth(int decrease) {
        currentPlayer.DecreaseHealth(decrease);
    }

    public int UpdatePlayerScore(int score) {
        currentPlayer.UpdatePlayerScore(score);
        return currentPlayer.GetPlayerScore();
    }
}