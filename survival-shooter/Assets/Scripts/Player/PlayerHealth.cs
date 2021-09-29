using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    public bool isDead;                                                
    bool damaged;     

    GameManager gameManager;                                          

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();

        currentHealth = startingHealth;

        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;

            damaged = false;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

        UpdateHealthBar();

        playerAudio.Play();

        if (currentHealth <= 0 && !isDead)
        {
            currentHealth = 0;

            Death();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        UpdateHealthBar();

        if (currentHealth >= 0 && !isDead)
        {
            currentHealth = startingHealth;
        }
    }

    public void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;
    }


    private void Death()
    {
        isDead = true;

        playerShooting.DisableEffects();

        anim.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }
}
