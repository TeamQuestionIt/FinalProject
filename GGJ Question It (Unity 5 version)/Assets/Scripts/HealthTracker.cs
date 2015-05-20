using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthTracker : MonoBehaviour {

    public float maxHealth = 100;
	private static float savedHealth = 100f;
    public bool dead = false;
    public bool isPlayer = false;
    private GameManager gameManager;
    private float displayedHealth;
    private float currentHealth;

    

    public GameObject healthBar;
	public GameObject enemyHealthBar;
	
	void Awake ()
    {
        if (isPlayer)
            SetHealth(savedHealth);

        gameManager = FindObjectOfType<GameManager>();
        displayedHealth = maxHealth;
        currentHealth = maxHealth;
	}
	

	void Update () 
    {
        if (healthBar != null && displayedHealth != currentHealth) //animate health bar
        {
            if (displayedHealth < currentHealth)
                displayedHealth += Time.deltaTime * 50f;
            else
                displayedHealth -= Time.deltaTime * 50f;

            if (Mathf.Abs(displayedHealth - currentHealth) < 1)
                displayedHealth = currentHealth;
            
            healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(displayedHealth, 100f);
        }
	}

    //returns true if the target dies
    public bool dealDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            dead = true;

            if (isPlayer)
                gameManager.EndGame();
        }

        savedHealth = currentHealth;
        return dead;
    }

    public void SetHealth(float amount)
    {
        currentHealth = amount;
        savedHealth = amount;
        displayedHealth = amount;
        if (healthBar != null)
            healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(amount, 100f);
    }




}
