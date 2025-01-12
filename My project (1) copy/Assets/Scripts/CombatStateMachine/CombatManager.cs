using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombatManager : MonoBehaviour
{
    #region Variables
    private static CombatManager _instance = null;
    private CombatState combatState;


    public List<BaseCharacter> enemies;
    public List<BaseCharacter> sortedParty;
    public List<BaseCharacter> sortedEnemies;
    public List<BaseCharacter> sortedAll = new List<BaseCharacter>();
    public BaseCharacter activeCharacter;
    private int turnNumber = 0;
    #endregion

    public static CombatManager Instance => _instance;

    [SerializeField] private BattleUI ui;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }
    #region Speed Calculations
    public void SetSpeed()
    {
        sortedParty = GameManager.Instance.currentParty;
        sortedParty.Sort((a, b) => b.Speed.CompareTo(a.Speed));
        sortedEnemies = new List<BaseCharacter>(enemies);
        sortedEnemies.Sort((a, b) => b.Speed.CompareTo(a.Speed));

        sortedAll = new List<BaseCharacter>(sortedParty);
        sortedAll.AddRange(sortedEnemies);
        sortedAll.Sort((a, b) => b.Speed.CompareTo(a.Speed));
    }
    #endregion

    public void Start(){
        Time.timeScale = 1;
        combatState = CombatState.Instance;
        SetSpeed();
        activeCharacter = sortedAll[0];
        CombatState.Instance.UpdateGameStatus(CombatState.BattleStates.STARTANIMATION);
    }
    public void BattleStart()
    {
        
        StartTurn();
    }

    public void StartTurn()
    {
        if (activeCharacter.isPlayer)
            combatState.UpdateGameStatus(CombatState.BattleStates.PLAYERCHOICE);
        else
            combatState.UpdateGameStatus(CombatState.BattleStates.ENEMYCHOICE);
    }

    public void PlayGeneralAnimation(){
        combatState.UpdateGameStatus(CombatState.BattleStates.PLAYERANIMATE);
    }


    public void StartPlayerAnimate()
    {
        combatState.UpdateGameStatus(CombatState.BattleStates.PLAYERANIMATE);
    }

    public (String,bool) PlayerAttack(BaseCharacter target)
    {
        int damage;
        if (activeCharacter.hasAdrenaline)
        {
            damage = Mathf.Max(activeCharacter.Attack * 2 - target.Defense, 0);
        }
        damage = Mathf.Max(activeCharacter.Attack - target.Defense, 0);
        target.CurrentHealth -= damage;
        target.GetComponent<EnemyUIController>().updateHealth();
        target.resetGuard();
        
        if (target.CurrentHealth <= 0)
        {
            target.CurrentHealth = 0;
            if (enemies.Count == 0)
            {
                combatState.UpdateGameStatus(CombatState.BattleStates.WIN);
   
            }             
            return (target.name + " defeated!",true);
            
        }
        return (activeCharacter.name + " attacks " + target.name + " for " + damage + "!", false);
    }

    public void NextTurn()
    {
        if (enemies.Count == 0)
        {
            combatState.UpdateGameStatus(CombatState.BattleStates.WIN);
            return;
        }

        turnNumber = (turnNumber + 1) % (sortedAll.Count);
        activeCharacter = sortedAll[turnNumber];
        StartTurn();
    }


  public void EnemyAttack()
    {
        BaseCharacter target = sortedParty[UnityEngine.Random.Range(0, sortedParty.Count)];
        int damage = Mathf.Max(activeCharacter.Attack - target.Defense, 0);
        if (target.isGuarding)
        {
            damage = damage / 2;
        }
        target.CurrentHealth -= damage;
        if (target.CurrentHealth <= 0)
        {
            target.CurrentHealth = 0;
            target.isDead = true;
            target.GetComponent<PartyUIController>().die();
            target.GetComponent<PartyUIController>().StopCoroutine("ToggleFramesCoroutine");
        }
        target.GetComponent<PartyUIController>().updateHealth();
        target.resetGuard();
    }


}
