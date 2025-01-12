using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CombatState : MonoBehaviour
{
    #region UnityEvents for combat state changes
    public UnityEvent OnBattleIntro;
    public UnityEvent OnBattleStart;

    public UnityEvent OnPlayerTurn;
    public UnityEvent OnEnemyTurn;

    public UnityEvent OnPlayerAnimate;
    public UnityEvent OnEnemyAnimate; 

    public UnityEvent OnEndPlayerTurn;
    public UnityEvent OnEndEnemyTurn;

    public UnityEvent OnBattleWon;
    public UnityEvent OnBattleLost;
    #endregion
    
    #region private variables
    private static CombatState _instance = null;
    
    #endregion

    public BattleStates CurrentBattleState { get; private set; }

    public static CombatState Instance
   {
       get { return _instance; }
   }


    private void Awake()
    {
        if (_instance == null)
       {
           _instance = this;
       }
       else
       {
           Destroy(gameObject);
       }
       CurrentBattleState = BattleStates.STARTANIMATION;
        UpdateGameStatus(BattleStates.STARTANIMATION); 
    }

   public void UpdateGameStatus(BattleStates newCombatState)
   {
        CurrentBattleState = newCombatState;
        switch (newCombatState)
        {
            case BattleStates.STARTANIMATION:
                OnBattleIntro.Invoke();
                break;
            case BattleStates.START:
                OnBattleStart.Invoke();
                break;
            case BattleStates.PLAYERCHOICE:
                OnPlayerTurn.Invoke();
                break;
            case BattleStates.ENEMYCHOICE:
                OnEnemyTurn.Invoke();
                break;
            case BattleStates.LOSE:
                OnBattleLost.Invoke();
                break;
            case BattleStates.WIN:
                OnBattleWon.Invoke();
                break;
            case BattleStates.PLAYERANIMATE:
                OnPlayerAnimate.Invoke();
                break;
            case BattleStates.ENEMYANIMATE:
                OnEnemyAnimate.Invoke();
                break;
            default:
               Debug.LogError("Unhandled GameStatus! This shouldn't happen.");
               break;
        }
        
    }


    public enum BattleStates
    {
        STARTANIMATION,
        START,
        PLAYERCHOICE,
        PLAYERANIMATE,
        ENEMYCHOICE,
        ENEMYANIMATE,
        LOSE,
        WIN
    }
}
