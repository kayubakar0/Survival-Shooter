using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public Text warningText;

    public PlayerHealth playerHealth;       
    public float restartDelay = 5f;            

    private Animator anim;                          
    private float restartTimer;

    private bool isGameOver;                 

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            if (!isGameOver)
            {
                anim.SetTrigger("GameOver");
                isGameOver = true;
            }

            restartTimer += Time.deltaTime;

            if (restartTimer >= restartDelay)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void ShowWarning(float enemyDistance)
    {
        warningText.text = string.Format("! {0} m",Mathf.RoundToInt(enemyDistance));
        anim.SetTrigger("Warning");
    }
}