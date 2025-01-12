using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyUIController : MonoBehaviour
{
    public VisualElement root;
    private ProgressBar healthBar;
    private VisualElement currentImage;
    private VisualElement selectBox;
    private BaseCharacter character;
    [SerializeField] public Sprite image;
    [SerializeField] private VisualTreeAsset enemyTemplate;

    // Start is called before the first frame update
    public void Start()
    {
        character = GetComponent<BaseCharacter>();


        root = enemyTemplate.CloneTree().contentContainer;

        healthBar = root.Q<ProgressBar>("EnemyHealth");
        currentImage = root.Q<VisualElement>("EnemySprite");
        selectBox = root.Q<VisualElement>("SelectBox");

        currentImage.style.backgroundImage = image.texture;
        healthBar.highValue = character.MaxHealth;
        healthBar.value = character.CurrentHealth;


    }

    public void updateHealth(){
        healthBar.value = character.CurrentHealth;
        Debug.Log("Health Updated" + character.CurrentHealth);
        if (character.CurrentHealth <= 0)
        {
            root.style.display = DisplayStyle.None;
        }
    }
    public void selectEnemy(){
        selectBox.AddToClassList("select");
    }
    public void deselectEnemy(){
        selectBox.ClearClassList();
    }



}
