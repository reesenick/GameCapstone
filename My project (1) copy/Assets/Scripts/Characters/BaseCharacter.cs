using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public bool hasAdrenaline = false;
    public string characterName;
    public int MaxHealth;
    public int CurrentHealth;
    public int Speed;
    public int Attack;
    public int Defense;
    public int SpecialMeter;
    public bool isPlayer;
    public bool isGuarding = false;
    public bool isDead = false;

    public int speedPoints;
    public int orderInQueue;

    private PartyUIController partyUIController;
    private EnemyUIController enemyUIController;

    private void Start()
    {
        if (isPlayer)
        {
            partyUIController = GetComponent<PartyUIController>();
        }
        else
        {
            enemyUIController = GetComponent<EnemyUIController>();
        }

    }

    public void takeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (isPlayer)
        {
            partyUIController.updateHealth();
        }
        else
        {
            enemyUIController.updateHealth();
        }

    }

    public void AddSpeed(){
        speedPoints += Speed;
    }

    public void ResetSpeed(){
        speedPoints = 0;
    }
    public void guard(){
        isGuarding = true;
    }
    public void resetGuard(){
        isGuarding = false;
    }
    
}
