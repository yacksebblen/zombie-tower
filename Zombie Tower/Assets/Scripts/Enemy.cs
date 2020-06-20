using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Com.Jackseb.Zombie
{
	public class Enemy : MonoBehaviour
	{
		[Header("Stats")]
		public int maxHealth;
		public int damageMin;
		public int damageMax;
		public float attackCooldown;
		public float range;

		[Header("Other")]
		public LayerMask canBeAttacked;
		public Transform center;

		NavMeshAgent nm;
		Transform target;
		float currentCooldown;
		bool stuckInPlayer = false;
		int currentHealth;


		// Start is called before the first frame update
		void Start()
		{
			nm = GetComponent<NavMeshAgent>();
			currentHealth = maxHealth;
		}

		// Update is called once per frame
		void Update()
		{
			if (GameObject.Find("Player(Clone)") != null) target = GameObject.Find("Player(Clone)").transform;

			if (target != null)
			{
				nm.SetDestination(target.position);

				if (currentCooldown <= 0 || stuckInPlayer == false)
				{
					Attack();
				}

				// Cooldown
				if (currentCooldown > 0) currentCooldown -= Time.deltaTime;
			}
		}

		void Attack()
		{
			RaycastHit _hit;
			if (Physics.Raycast(center.position, center.forward, out _hit, range, canBeAttacked))
			{
				stuckInPlayer = true;
				_hit.collider.transform.root.GetComponent<Player>().TakeDamage(Random.Range(damageMin, damageMax + 1));
			}
			else
			{
				stuckInPlayer = false;
			}

			currentCooldown = attackCooldown;
		}

		public void ZombieTakeDamage(int _damage)
		{
			currentHealth -= _damage;

			if (currentHealth <= 0)
			{
				Die();
			}
		}

		public void Die()
		{
			Destroy(transform.root.gameObject);
		}
	}
}