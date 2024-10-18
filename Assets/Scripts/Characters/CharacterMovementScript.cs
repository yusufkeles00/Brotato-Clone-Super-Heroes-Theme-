using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterMovementScript : MonoBehaviour
{
	private CharacterStatsSc characterStats;
	private EnemySpawnerSc enemySpawnerSc;

	public GameObject upgradeManager;

	public Rigidbody2D characterRb;
	public Animator characterAnim;

	public ParticleSystem dustParticle;
	public ParticleSystem.EmissionModule dustEmission;

	private GameObject popUpTextManagerObj;

	public Color healthDamageTextColor;

	public Vector2 moveDirection;

	[SerializeField] private int waveIndex;

	public int characterMaxHealth;
	public int characterCurrentHealth;
	public int characterLevel = 1;
	public int currentXp;
	public int xpRequired; //XP Required = (level + 3)^2
	public int totalXpRequired;
	public int characterArmor;
	public int characterRange; // this gonna be additional stat (multiplied by 0.12)
	public int characterDodge;
	public int characterDamage;
	public int characterCriticalDamage;
	public int characterLifeSteal;
	public float characterAttackSpeed;

	private float damageRecieved; // 1 / (1 + (characterArmor / 15))
	public float damageReduction; // waveIndex * ((1 - (%damageRecieved)) * 100)
	private int dodgeRandom;

	public float characterBaseMoveSpeed;
	public float characterMoveSpeed;
	public float characterMoveSpeedAdditional;

	float horizontalMove;
	float verticalMove;
	public float lifeStealCooldown = 1;

	private bool isFaceRight = false;

	private void Start()
	{
		characterStats = FindObjectOfType<CharacterStatsSc>();
		enemySpawnerSc = FindObjectOfType<EnemySpawnerSc>();

		characterMaxHealth = characterStats._maxHP;
		characterCurrentHealth = characterMaxHealth;

		dustEmission = dustParticle.emission;
		xpRequired = (characterLevel + 3) * (characterLevel + 3);

		popUpTextManagerObj = GameObject.FindGameObjectWithTag("PopUpManager");
		upgradeManager.GetComponent<UpgradeManager>();
	}

	private void Update()
	{
		lifeStealCooldown -= Time.deltaTime;

		// Initializing the variables
		waveIndex = enemySpawnerSc.currentWaveCount + 1;

		characterArmor = characterStats._armor;
		damageRecieved = 1 / (1 + (characterArmor / 15f));
		damageReduction = waveIndex * ((1 - (damageRecieved)) * 100f);

		characterMaxHealth = characterStats._maxHP;

		characterRange = characterStats._range;

		characterDamage = characterStats._damage;

		characterAttackSpeed = characterStats._attackSpeed;

		characterCriticalDamage = characterStats._criticalDamage;

		characterLifeSteal = characterStats._lifeSteal;

		characterMoveSpeed = characterStats._moveSpeed;
		characterMoveSpeedAdditional = characterBaseMoveSpeed + (characterBaseMoveSpeed * (characterMoveSpeed / 100));

		characterDodge = characterStats._dodge;
		dodgeRandom = Random.Range(0, 100);

		if (Time.timeScale != 0)
		{
			TakeInput();
		}
		FlipCharacter();

		if (characterCurrentHealth <= 0)
		{
			CharacterDeath();
		}
		if (characterCurrentHealth >= characterMaxHealth)
		{
			characterCurrentHealth = characterMaxHealth;
		}

		if (currentXp >= xpRequired)
		{
			CharacterLevelUp();
		}
	}

	private void FixedUpdate()
	{
		CharacterMovement();
	}

	public void CharacterTakeDamage(int damage)
	{
		int reducedDamage = Convert.ToInt32(damage - (damage * (damageReduction / 100f)));

		// chance to dodge
		if (!(dodgeRandom < characterDodge))
		{
			if(reducedDamage < 1) 
			{
				reducedDamage = 1;
			}
			characterCurrentHealth -= reducedDamage;
			popUpTextManagerObj.GetComponent<PopUpText>().CreateText("-", reducedDamage, gameObject.transform, new Vector3(1.5f, 1.5f, 0f), healthDamageTextColor);
			CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
		}
	}

	void CharacterDeath()
	{

	}

	void CharacterLevelUp()
	{
		characterLevel++;

		currentXp = currentXp - xpRequired;
		xpRequired = (characterLevel + 3) * (characterLevel + 3);

		//Open Upgrade Panel
		upgradeManager.gameObject.SetActive(true);
		upgradeManager.GetComponent<UpgradeManager>().LoadUpgradePanels();
		Time.timeScale = 0;
	}

	void TakeInput()
	{
		horizontalMove = Input.GetAxisRaw("Horizontal");
		verticalMove = Input.GetAxisRaw("Vertical");

		moveDirection = new Vector2(horizontalMove, verticalMove).normalized;
	}

	void CharacterMovement()
	{
		characterRb.velocity = new Vector2(moveDirection.x * characterMoveSpeedAdditional, moveDirection.y * characterMoveSpeedAdditional);

		if (horizontalMove != 0 || verticalMove != 0)
		{
			characterAnim.SetBool("isWalking", true);
			dustEmission.rateOverTime = 10f;

		}
		else
		{
			characterAnim.SetBool("isWalking", false);
			dustEmission.rateOverTime = 0f;
		}
	}

	void FlipCharacter()
	{
		if (horizontalMove > 0 && isFaceRight == false)
		{
			isFaceRight = !isFaceRight;
			transform.Rotate(0f, 180f, 0f);
		}
		else if (horizontalMove < 0 && isFaceRight == true)
		{
			isFaceRight = !isFaceRight;
			transform.Rotate(0f, 180f, 0f);
		}
	}
}
