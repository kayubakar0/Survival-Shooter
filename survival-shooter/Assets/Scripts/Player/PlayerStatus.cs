using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerStatus : MonoBehaviour
{
    [Header("Power Up Values")]
    [HideInInspector] public float moveSpeedMultiplier;
    [HideInInspector] public float moveSpeedPowerUpDuration;
    [HideInInspector] public bool isOnMoveSpeedPowerUp;
    private float timerMoveSpeed;
    [Space]
    [HideInInspector] public float attackSpeedMultiplier;
    [HideInInspector] public float attackSpeedPowerUpDuration;
    [HideInInspector] public bool isOnAttackSpeedPowerUp;
    private float timerAttackSpeed;
    [Space]
    [HideInInspector] public int healValue;

    [Space]

    [Header("Power Up UI")]
    public GameObject powerUpLabelMoveSpeed;
    public GameObject powerUpLabelAttackSpeed;
    [Space]
    public Image powerUpProgressBarMoveSpeed;
    public Image powerUpProgressBarAttackSpeed;
    [Space]
    public Animator popUpHealAnim;

    // Script Refs
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;

    [Space]
    [Header("Script References")]
    [SerializeField] private PlayerShooting playerShooting;

    [Space]
    [Header("UnityEvents")]
    [SerializeField] private UnityEvent OnPowerUpMoveSpeed;
    [Space]
    [SerializeField] private UnityEvent OnPowerUpAttackSpeed;
    [Space]
    [SerializeField] private UnityEvent OnHealed;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        powerUpLabelMoveSpeed.SetActive(false);
        powerUpLabelAttackSpeed.SetActive(false);

        popUpHealAnim.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isOnMoveSpeedPowerUp)
            MoveSpeedPowerUpHandling();

        if (isOnAttackSpeedPowerUp)
            AttackSpeedPowerUpHandling();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PowerUpMoveSpeed"))
        {
            other.gameObject.GetComponent<PowerUpMoveSpeed>().GetPowerUp();

            powerUpLabelMoveSpeed.SetActive(true);
            powerUpLabelMoveSpeed.transform.SetSiblingIndex(1);

            isOnMoveSpeedPowerUp = true;
            timerMoveSpeed = moveSpeedPowerUpDuration;

            OnPowerUpMoveSpeed.Invoke();

            Debug.Log("Got Power Up! : Speedness lvl 1");
        }
        else if (other.gameObject.CompareTag("PowerUpAttackSpeed"))
        {
            other.gameObject.GetComponent<PowerUpAttackSpeed>().GetPowerUp();

            powerUpLabelAttackSpeed.SetActive(true);
            powerUpLabelAttackSpeed.transform.SetSiblingIndex(1);

            isOnAttackSpeedPowerUp = true;
            timerAttackSpeed = attackSpeedPowerUpDuration;

            OnPowerUpAttackSpeed.Invoke();

            Debug.Log("Got Power Up! : Attack Speed lvl 1");
        }
        else if (other.gameObject.CompareTag("PowerUpHeal"))
        {
            other.gameObject.GetComponent<PowerUpHeal>().GetPowerUp();

            playerHealth.Heal(healValue);
            
            popUpHealAnim.gameObject.SetActive(true);
            popUpHealAnim.GetComponent<Text>().text = "+ " + healValue.ToString();
            popUpHealAnim.SetTrigger("Pop");

            OnHealed.Invoke();
            Debug.Log("Got Power Up! : Healing lvl 1");
        }
    }

    private void MoveSpeedPowerUpHandling()         // Movement Speed Multiplier
    {
        timerMoveSpeed -= Time.deltaTime;

        powerUpProgressBarMoveSpeed.fillAmount = timerMoveSpeed / moveSpeedPowerUpDuration;

        playerMovement.multiplier = moveSpeedMultiplier;

        if (timerMoveSpeed <= 0)
        {
            playerMovement.multiplier = 1f;

            powerUpLabelMoveSpeed.SetActive(false);

            isOnMoveSpeedPowerUp = false;
        }
    }

    private void AttackSpeedPowerUpHandling()       // Attack Speed Multiplier
    {
        timerAttackSpeed -= Time.deltaTime;

        powerUpProgressBarAttackSpeed.fillAmount = timerAttackSpeed / attackSpeedPowerUpDuration;

        playerShooting.attackSpeedMultiplier = attackSpeedMultiplier;

        if (timerAttackSpeed <= 0)
        {
            playerShooting.attackSpeedMultiplier = 1f;

            powerUpLabelAttackSpeed.SetActive(false);

            isOnAttackSpeedPowerUp = false;
        }
    }
}
