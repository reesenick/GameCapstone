using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public CombatManager combatManager;
    // public CombatState combatState;
    public List<BaseCharacter> currentParty = new List<BaseCharacter>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        combatManager = CombatManager.Instance;
        // combatState = CombatState.Instance;
    }
}
