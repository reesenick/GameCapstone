using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BattleUI : MonoBehaviour
{
    #region UI Elements
    private VisualElement root, bg, bgEffect, actionBox, abBg, textBox, textBoxContainer, actionBoxContainer;
    private VisualElement card1, card2, card3, card4, enemyContainer;
    private Label dialogue;
    private Button attackButton, guardButton, itemButton, specialButton;
    [SerializeField] private VisualTreeAsset enemyTemplate;
    #endregion

    #region Input and State
    private PlayerInputActions inputActions;
    private List<(VisualElement, BaseCharacter)> cardPlacements = new();
    private readonly List<string> cardStyles = new() { "CardSpread1", "CardSpread2", "CardSpread3", "CardSpread4" };
    private readonly List<string> cardPlacementNames = new() { "Card", "Card2", "Card3", "Card4" };
    private int pressedSelect = -1;
    public bool cardsStacked = true;

    private List<VisualElement> enemyElements = new();
    private int selectedEnemyIndex = -1;
    private bool selectingEnemy=false;
    #endregion

    #region Initialization
    private void Start()
    {
        InitializeUIElements();
        SetupButtonActions();
        RegisterEventListeners();
        root.style.visibility = Visibility.Visible;
        DisableButtons();

        // Initialize input actions and bind Select and Move actions
        inputActions = new PlayerInputActions();
        inputActions.PlayerMenu.Select.performed += ctx => ToggleSelect();
        inputActions.PlayerMenu.Move.performed += ctx => OnMove(ctx.ReadValue<Vector2>());
        inputActions.Disable();
    }

    private void InitializeUIElements()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        bg = root.Q<VisualElement>("Background");
        bgEffect = root.Q<VisualElement>("BackgroundEffects");
        actionBoxContainer = root.Q<VisualElement>("ActionBoxContainer");
        actionBox = root.Q<VisualElement>("ActionBox");
        abBg = root.Q<VisualElement>("AbBackground");
        textBoxContainer = root.Q<VisualElement>("TextBox");
        textBox = root.Q<VisualElement>("DialogueBox");
        dialogue = textBoxContainer.Q<Label>("Text");

        card1 = root.Q<VisualElement>("Card1");
        card2 = root.Q<VisualElement>("Card2");
        card3 = root.Q<VisualElement>("Card3");
        card4 = root.Q<VisualElement>("Card4");

        attackButton = actionBox.Q<Button>("Attack");
        guardButton = actionBox.Q<Button>("Guard");
        itemButton = actionBox.Q<Button>("Item");
        specialButton = actionBox.Q<Button>("Special");

        enemyContainer = root.Q<VisualElement>("EnemyContainer");
    }

    private void SetupButtonActions()
    {
        attackButton.clicked += AttackButtonPressed;
        guardButton.clicked += GuardButtonPressed;
        itemButton.clicked += ItemButtonPressed;
    }

    private void RegisterEventListeners()
    {
        var combatState = CombatState.Instance;
        combatState.OnBattleIntro.AddListener(BattleIntroReceived);
        combatState.OnEnemyTurn.AddListener(EnemyTurnReceived);
        combatState.OnPlayerTurn.AddListener(PlayerTurnReceived);
        combatState.OnBattleLost.AddListener(BattleLostReceived);
        combatState.OnBattleWon.AddListener(BattleWonReceived);
    }

    private void OnDestroy()
    {
        attackButton.clicked -= AttackButtonPressed;
        guardButton.clicked -= GuardButtonPressed;
        itemButton.clicked -= ItemButtonPressed;

        var combatState = CombatState.Instance;
        combatState.OnBattleIntro.RemoveListener(BattleIntroReceived);
        combatState.OnEnemyTurn.RemoveListener(EnemyTurnReceived);
        combatState.OnPlayerTurn.RemoveListener(PlayerTurnReceived);
        combatState.OnBattleLost.RemoveListener(BattleLostReceived);
        combatState.OnBattleWon.RemoveListener(BattleWonReceived);

        inputActions.PlayerMenu.Select.performed -= ctx => ToggleSelect();
        inputActions.PlayerMenu.Move.performed -= ctx => OnMove(ctx.ReadValue<Vector2>());
        inputActions.Disable();
    }
    #endregion

    #region Battle State Handlers
    private void BattleIntroReceived()
    {
        Debug.Log("Battle Start");
        root.style.visibility = Visibility.Visible;
        StartCoroutine(AnimateBGCoroutine());

        InitializeCardPlacements();
        UpdateCards();
        PopulateEnemyContainer();
        
        StartCoroutine(AnimateIntro());
    }

    private void PlayerTurnReceived(){
        ChangeCardsOffense();
        if (CombatManager.Instance.activeCharacter.isDead){
            RemoveDeadEnemyVisuals();
            CombatManager.Instance.NextTurn();
        }
        EnableButtons();
    }

    private void EnemyTurnReceived()
    {
        string text = $"{CombatManager.Instance.activeCharacter.name} attacks!";
        StartCoroutine(EnemyAnimation(text));
    }

    private void BattleWonReceived(){
        StartCoroutine(battleEndAnimation("You Won!"));
    }
    private void BattleLostReceived(){
        StartCoroutine(battleEndAnimation("You Lost!"));
    }
    private IEnumerator battleEndAnimation(String text){
        TextBoxAnimation(text);
        yield return new WaitForSecondsRealtime(1.5f);
        TextBoxAnimation("Restarting!");
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene("CharacterCard");
    }
    #endregion

    #region UI Interactions
    public void ToggleUI()
    {
        if (cardsStacked) ChangeCardsDefense();
        else ChangeCardsOffense();
        cardsStacked = !cardsStacked;
    }

    public void EnableButtons()
    {
        attackButton.SetEnabled(true);
        guardButton.SetEnabled(true);
        itemButton.SetEnabled(true);
        specialButton.SetEnabled(true);
    }

    public void DisableButtons()
    {
        attackButton.SetEnabled(false);
        guardButton.SetEnabled(false);
        itemButton.SetEnabled(false);
        specialButton.SetEnabled(false);
    }
    #endregion

    #region Button Actions
    private void disableButtons() => DisableButtons();
    private void enableButtons() => EnableButtons();

    private void AttackButtonPressed()
    {
        if (CombatState.Instance.CurrentBattleState == CombatState.BattleStates.PLAYERCHOICE &&
            CombatManager.Instance.activeCharacter.isPlayer)
        {
            inputActions.Enable();
            selectingEnemy = true;
            selectedEnemyIndex = 0;
            disableButtons();
            StartCoroutine(selectEnemy());
        }
    }
    
    private IEnumerator selectEnemy()
    {
        while (selectingEnemy)
        {
            if (pressedSelect == 1)
            {
                ConfirmEnemySelection();
                selectingEnemy = false;
            }
            HighlightSelectedEnemy();

            yield return new WaitForSeconds(0.1f);
        }
    }
    private void GuardButtonPressed(){
        CombatManager.Instance.activeCharacter.guard();
        StartCoroutine(AnimateGeneralText(CombatManager.Instance.activeCharacter.name + " guards!"));
    }
    private void ItemButtonPressed() => ChangeCardsOffense();
    #endregion

    #region Card Management
    private void InitializeCardPlacements()
    {
        var sortedParty = CombatManager.Instance.sortedParty;
        if (sortedParty.Count > 0) cardPlacements.Add((card1, sortedParty[0]));
        if (sortedParty.Count > 1) cardPlacements.Add((card2, sortedParty[1]));
        if (sortedParty.Count > 2) cardPlacements.Add((card3, sortedParty[2]));
        if (sortedParty.Count > 3) cardPlacements.Add((card4, sortedParty[3]));

        foreach (var (card, character) in cardPlacements)
            SetCharacterInCard(card, character);
        ChangeCardsOffense();
    }

    private void SetCharacterInCard(VisualElement card, BaseCharacter character)
    {
        var characterCard = character.GetComponent<UIDocument>().rootVisualElement;
        card.Add(characterCard);
    }

    private void UpdateCards()
    {
        for (int i = 0; i < cardPlacements.Count; i++)
        {
            cardPlacements[i].Item1.ClearClassList();
            cardPlacements[i].Item1.AddToClassList(cardPlacementNames[i]);
            cardPlacements[i].Item1.SendToBack();
        }
    }
    public void UpdateCardStack()
    {
        var newStack = new List<(VisualElement, BaseCharacter)>(cardPlacements);
        for (int i = 1; i < cardPlacements.Count; i++)
            newStack[i - 1] = cardPlacements[i];

        newStack[^1] = cardPlacements[0];
        cardPlacements = newStack;
        UpdateCards();
    }

    private void ChangeCardsDefense()
    {
        cardsStacked = false;
        for (int i = 0; i < cardPlacements.Count; i++)
        {
            cardPlacements[i].Item1.ClearClassList();
            cardPlacements[i].Item1.AddToClassList(cardStyles[i]);
            cardPlacements[i].Item2.GetComponent<PartyUIController>().Spread();
        }
    }

    private void ChangeCardsOffense()
    {
        cardsStacked = true;
        foreach (var (card, character) in cardPlacements)
        {
            character.GetComponent<PartyUIController>().Stack();
            card.ClearClassList();
            card.AddToClassList(cardPlacementNames[cardPlacements.IndexOf((card, character))]);
            card.SendToBack();
        }
    }
    #endregion


    #region Animation Coroutines
    private IEnumerator AnimateIntro()
    {
        Debug.Log("Battle Intro Playing");
        TextBoxAnimation("The Enemy Appears!");
        yield return new WaitForSecondsRealtime(2f);
        FinishTextBoxAnimation();
        EnableButtons();
        CombatManager.Instance.BattleStart();
    }

    private IEnumerator AnimateGeneralText(string text)
    {
        TextBoxAnimation(text);
        yield return new WaitForSecondsRealtime(1.3f);
        FinishTextBoxAnimation();
        UpdateCardStack();
        RemoveDeadEnemyVisuals();
        CombatManager.Instance.NextTurn();
    }

    private IEnumerator EnemyAnimation(string text)
    {
        TextBoxAnimation(text);
        yield return new WaitForSecondsRealtime(2f);
        textBoxContainer.AddToClassList("MoveDown");
        ChangeCardsDefense();
        yield return new WaitForSecondsRealtime(1f);
        CombatManager.Instance.EnemyAttack();
        yield return new WaitForSecondsRealtime(2f);
        textBoxContainer.RemoveFromClassList("MoveDown");
        FinishTextBoxAnimation();
        CombatManager.Instance.NextTurn();
    }

    private IEnumerator AnimateBGCoroutine()
    {
        while (true)
        {
            bgEffect.ToggleInClassList("BGEffect2");
            yield return new WaitForSeconds(5f);
        }
    }
    #endregion

    #region Text Box Management
    private void TextBoxAnimation(string text)
    {
        if (cardsStacked== false){
            foreach (var (card, character) in cardPlacements)
            {
                card.AddToClassList("MoveDown");
            }
        }
        DisableButtons();
        if (!actionBoxContainer.ClassListContains("MoveDown")){
            actionBoxContainer.AddToClassList("MoveDown");
        }
        dialogue.text = text;
        textBox.ClearClassList();
    }

    private void FinishTextBoxAnimation()
    {
        if (cardsStacked == false){
            foreach (var (card, character) in cardPlacements)
            {
                card.RemoveFromClassList("MoveDown");
            }
        }
        EnableButtons();
        actionBoxContainer.ClearClassList();
        textBox.AddToClassList("ShrinkBox");
        dialogue.text = "Select";
    }
    #endregion

public void ToggleSelect()
{
    if (selectingEnemy)
    {
        ConfirmEnemySelection();
        selectingEnemy = false;
    }
    else
    {
        selectingEnemy = true;
        selectedEnemyIndex = 0; // Start selection from the first enemy
        HighlightSelectedEnemy();
        disableButtons();
    }
}

private void PopulateEnemyContainer()
{
    enemyElements.Clear();
    foreach (BaseCharacter enemy in CombatManager.Instance.enemies)
    {
        EnemyUIController enemyVisual = enemy.GetComponent<EnemyUIController>();
        VisualElement enemySelect = enemyVisual.root.Q<VisualElement>("SelectBox");

        enemyContainer.Add(enemyVisual.root);
        enemyElements.Add(enemySelect);
    }
}

private void HighlightSelectedEnemy()
{
    // Remove selection from all enemies first
    foreach (var enemy in enemyElements) 
    {
        enemy.RemoveFromClassList("select");
    }

    // Add selection to the current selected enemy if within bounds
    if (selectedEnemyIndex >= 0 && selectedEnemyIndex < enemyElements.Count)
    {
        enemyElements[selectedEnemyIndex].AddToClassList("select");
    }
}

private void OnMove(Vector2 moveInput)
{
    if (selectingEnemy && enemyElements.Count > 0)
    {
        if (selectedEnemyIndex == -1) selectedEnemyIndex = 0;
        else selectedEnemyIndex = (selectedEnemyIndex + (moveInput.x > 0 ? 1 : -1) + enemyElements.Count) % enemyElements.Count;

        HighlightSelectedEnemy();
    }
}

private void ConfirmEnemySelection()
{
    if (selectedEnemyIndex >= 0 && selectedEnemyIndex < enemyElements.Count)
    {
        var selectedEnemy = CombatManager.Instance.enemies[selectedEnemyIndex];
        (string,bool) attackResult = CombatManager.Instance.PlayerAttack(selectedEnemy);
        string attackResultText = attackResult.Item1;
        StartCoroutine(AnimateGeneralText(attackResultText));

        selectedEnemyIndex = -1;
        selectingEnemy = false;

        enableButtons();
        HighlightSelectedEnemy(); // Deselect enemies after confirmation
        inputActions.Disable();
        RemoveDeadEnemyVisuals();
    }
}
private void RemoveDeadEnemyVisuals()
{
    for (int i = 0; i < CombatManager.Instance.enemies.Count; i++)
    {
        BaseCharacter enemy = CombatManager.Instance.enemies[i];

        // If the enemy is dead, remove its visual element
        if (enemy.isDead)
        {
            // Remove enemy UI from the container
            enemyContainer.Remove(enemy.GetComponent<EnemyUIController>().root);

            // Also remove from the enemyElements list
            enemyElements.RemoveAt(i);

            // Optionally update the selectedEnemyIndex if necessary
            if (selectedEnemyIndex >= enemyElements.Count)
                selectedEnemyIndex = enemyElements.Count - 1;
        }
    }
}


}
