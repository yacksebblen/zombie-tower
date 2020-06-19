using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Jackseb.Zombie
{
	public class Motion : MonoBehaviour
	{
		public float speed;
		public float walkModifier;
		public float jumpForce;
		public int maxHealth;

		public Transform groundDetect;
		public LayerMask ground;

		Rigidbody rig;

		int currentHealth;

		GameManager gm;

		void Start()
		{
			gm = GameObject.Find("GameManager").GetComponent<GameManager>();

			rig = GetComponent<Rigidbody>();
			currentHealth = maxHealth;
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

			// Jumping
			if (isJumping)
			{
				rig.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
			}
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

		public void TakeDamage(int _damage)
		{
			currentHealth -= _damage;
			Debug.Log(currentHealth);

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