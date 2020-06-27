using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Jackseb.Zombie
{
	public class Player : MonoBehaviour
	{
		[Header("Stats")]
		public float speed;
		public float walkModifier;
		public float jumpForce;
		public float maxHealth;

		[Header("Alerting Zombies")]
		public float runSoundRadius;
		public float shootSoundRadius;
		public float bulletHoleSoundRadius;

		[Header("Other")]
		public Transform groundDetect;
		public LayerMask ground;
		public LayerMask zombieLayerMask;

		Rigidbody rig;

		float currentHealth;
		Transform uiHealthBar;

		Weapon wpn;
		GameManager gm;

		void Start()
		{
			wpn = GetComponent<Weapon>();
			gm = GameObject.Find("GameManager").GetComponent<GameManager>();

			rig = GetComponent<Rigidbody>();
			currentHealth = PlayerPrefs.GetFloat("health", 100);

			uiHealthBar = GameObject.Find("HUD/Health/Bar").transform;
			RefreshHealth();
		}

		void Update()
		{
			// Axes
			float _hMove = Input.GetAxisRaw("Horizontal");
			float _vMove = Input.GetAxisRaw("Vertical");

			// Controls
			bool walk = Input.GetKey(KeyCode.LeftShift);
			bool jump = Input.GetKeyDown(KeyCode.Space);

			// States
			bool isGrounded = Physics.Raycast(groundDetect.position, Vector3.down, 0.1f, ground);
			bool isJumping = jump && isGrounded;
			bool isWalking = walk && !isJumping && isGrounded;

			if (gm.currentState == GameManager.State.Zombies && !isWalking)
			{
				Collider[] hitColliders = Physics.OverlapSphere(transform.position, runSoundRadius, zombieLayerMask);
				for (int i = 0; i < hitColliders.Length; i++)
				{
					hitColliders[i].transform.root.GetComponent<Enemy>().SetTarget(transform);
				}
			}

			// Jumping
			if (isJumping)
			{
				rig.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
			}

			// UI Refreshes
			if (gm.currentState == GameManager.State.Elevator)
			{
				float newHealth = Mathf.Ceil(currentHealth / 20) * 20;
				currentHealth = Mathf.Lerp(currentHealth, newHealth, Time.deltaTime * 8f);
			}

			RefreshHealth();
		}

		void FixedUpdate()
		{
			// Axes
			float _hMove = Input.GetAxisRaw("Horizontal");
			float _vMove = Input.GetAxisRaw("Vertical");

			// Controls
			bool walk = Input.GetKey(KeyCode.LeftShift);
			bool jump = Input.GetKeyDown(KeyCode.Space);

			// States
			bool isGrounded = Physics.Raycast(groundDetect.position, Vector3.down, 0.1f, ground);
			bool isJumping = jump && isGrounded;
			bool isWalking = walk && !isJumping && isGrounded;
			
			// Movement
			Vector3 _direction = new Vector3(_hMove, 0, _vMove);
			_direction.Normalize();

			float _adjustedSpeed = speed;
			if (isWalking) _adjustedSpeed *= walkModifier;

			Vector3 _targetVelocity = transform.TransformDirection(_direction) * _adjustedSpeed * Time.deltaTime;
			_targetVelocity.y = rig.velocity.y;
			rig.velocity = _targetVelocity;
		}

		void RefreshHealth()
		{
			float _healthRatio = (float)currentHealth / (float)maxHealth;
			uiHealthBar.localScale = Vector3.Lerp(uiHealthBar.localScale, new Vector3(_healthRatio, 1, 1), Time.deltaTime * 8f);
			PlayerPrefs.SetFloat("health", currentHealth);
		}

		public void TakeDamage(int _damage)
		{
			currentHealth -= _damage;
			RefreshHealth();

			if (currentHealth <= 0)
			{
				Die();
			}
		}

		public void Die()
		{
			gm.Spawn();
			Destroy(transform.root.gameObject);
		}
	}
}