using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Com.Jackseb.Zombie
{
	public class Enemy : MonoBehaviour
	{
		public int damage;
		public float attackCooldown;
		public LayerMask canBeAttacked;
		public Transform center;

		NavMeshAgent nm;
		Transform target;
		float currentCooldown;


		// Start is called before the first frame update
		void Start()
		{
			nm = GetComponent<NavMeshAgent>();

		}

		// Update is called once per frame
		void Update()
		{
			if (GameObject.Find("Player(Clone)") != null) target = GameObject.Find("Player(Clone)").transform;

			if (target != null)
			{
				nm.SetDestination(target.position);

				if (currentCooldown <= 0)
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
			if (Physics.Raycast(center.position, center.forward, out _hit, 1f, canBeAttacked))
			{
				_hit.collider.transform.root.GetComponent<Motion>().TakeDamage(damage);
			}

			currentCooldown = attackCooldown;
		}
	}
}