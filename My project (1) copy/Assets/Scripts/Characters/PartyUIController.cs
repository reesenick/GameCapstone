using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PartyUIController : MonoBehaviour
{
    private VisualElement root;
    private VisualElement healthBar;
    private VisualElement currentImage;
    private VisualElement frame1;
    private VisualElement frame2;
    private VisualElement frame3;
    
    [SerializeField] private Sprite BGImage;
    private int frameIndex = 0;
    private BaseCharacter character;


    List<VisualElement> frames = new List<VisualElement>();
    [SerializeField] private Sprite[] animationSprites; 
    [SerializeField] private float frameDuration = 0.3f;

    private VisualElement health;
    private Label ouch;

    private VisualElement spread;
    private Label currentHP;
    private Label maxHP;
    private Label charName;
    private VisualElement damage;
    public bool needsDamageUpdate = false;


    void Start()
    {
        character = GetComponent<BaseCharacter>();
        root = GetComponent<UIDocument>().rootVisualElement;

        healthBar = root.Q<VisualElement>("HealthBar");
        currentImage = root.Q<VisualElement>("CharacterImage");
        frame1 = currentImage.Q<VisualElement>("Frame1");
        frame2 = currentImage.Q<VisualElement>("Frame2");
        frame3 = currentImage.Q<VisualElement>("Frame3");

        health = healthBar.Q<VisualElement>("Health");
        charName = root.Q<Label>("Name");
        charName.text = character.name;
        //health.AddToClassList("s1");

        spread = root.Q<VisualElement>("Spread");
        currentHP = root.Q<Label>("CurrentHp");
        maxHP = root.Q<Label>("MaxHp");
        damage = root.Q<VisualElement>("Damage");
        currentHP.text = "";
        currentHP.text = character.CurrentHealth.ToString();
        maxHP.text = "";
        maxHP.text = character.MaxHealth.ToString();

        ouch = root.Q<Label>("Ouch");
        ouch.text = "";

        currentImage.style.backgroundImage = BGImage.texture;


        frames.Add(frame1);
        frames.Add(frame2);
        frames.Add(frame3);

        

        for (int i = 0; i < animationSprites.Length; i++)
        {
            frames[i].style.backgroundImage = animationSprites[i].texture;
            frames[i].AddToClassList("Invisible");
        }
        frameIndex = Random.Range(0, animationSprites.Length - 1);

    } 


    public void Spread(){
        spread.ClearClassList();
    }
    public void Stack(){
        if (!spread.ClassListContains("MoveDown")){
            spread.AddToClassList("MoveDown");
        }

    }
    public void updateHealth(){
       //healthBar.value = character.CurrentHealth;
        currentHP.text = "";
        currentHP.text = character.CurrentHealth.ToString();
        maxHP.text = "";
        maxHP.text = character.MaxHealth.ToString();
    
        float newscale = (float)character.CurrentHealth / (float)character.MaxHealth;
        health.style.scale = Vector2.one;
        health.style.scale = new StyleScale(new Scale(new Vector2(newscale,1)));

        if (character.CurrentHealth <= 15)
        {
            StartCoroutine(ToggleFramesCoroutine());
        }
    }

    
    public void NormalMode(){
        StopCoroutine(ToggleFramesCoroutine());
        for (int i = 0; i < animationSprites.Length; i++)
        {
        frames[i].style.visibility = Visibility.Hidden;
        }
        // currentImage.style.visibility = Visibility.Visible;
        
    }


    public void TakeDamage(){
        StartCoroutine(TakeDamageCoroutine());
        updateHealth();
    }
    private IEnumerator TakeDamageCoroutine()
    {
        ouch.text = "Ouch...";
        yield return new WaitForSecondsRealtime(2.5f);
        ouch.text = "";

    }

    public void die(){
        currentImage.AddToClassList("dead");
    }

    private IEnumerator ToggleFramesCoroutine()
    {
        while (!character.isDead)
        {
            if (frameIndex == 0 ){
                frames[animationSprites.Length - 1].AddToClassList("Invisible");
            }
            else{
            frames[frameIndex -1].AddToClassList("Invisible");
            }
            frames[frameIndex].RemoveFromClassList("Invisible");
            frameIndex = (frameIndex + 1) % frames.Count;
            yield return new WaitForSeconds(frameDuration); // Adjust the interval as needed
        }
    }



}
