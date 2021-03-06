﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Jackseb.Zombie
{
	public class Weapon : MonoBehaviour
	{
		public Gun[] loadout;
		public Transform weaponParent;
		public Transform normalCam;
		public GameObject bulletHolePrefab;
		public LayerMask canBeShot;

		float currentCooldown;
		int currentIndex;
		GameObject currentWeapon;

		Player pl;
		GameManager gm;

		void Start()
		{
			pl = GetComponent<Player>();
			gm = GameObject.Find("GameManager").GetComponent<GameManager>();

			Equip(0);
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				Equip(0);
			}

			if (currentWeapon != null)
			{
				if (Input.GetMouseButtonDown(0) && currentCooldown <= 0)
				{
					Shoot();
				}

				// Weapon elasticity
				currentWeapon.transform.localPosition = Vector3.Lerp(currentWeapon.transform.localPosition, Vector3.zero, Time.deltaTime * 4f);

				// Cooldown
				if (currentCooldown > 0) currentCooldown -= Time.deltaTime;
			}
		}

		void Equip(int p_ind)
		{
			if (currentWeapon != null) Destroy(currentWeapon);

			currentIndex = p_ind;

			GameObject _newWeapon = Instantiate(loadout[p_ind].prefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
			_newWeapon.transform.localPosition = Vector3.zero;
			_newWeapon.transform.localEulerAngles = Vector3.zero;

			currentWeapon = _newWeapon;
		}

		void Shoot()
		{
			// Bloom
			Vector3 _bloom = normalCam.position + normalCam.forward * 1000f;
			_bloom += Random.Range(-loadout[currentIndex].spread, loadout[currentIndex].spread) * normalCam.up;
			_bloom += Random.Range(-loadout[currentIndex].spread, loadout[currentIndex].spread) * normalCam.right;
			_bloom -= normalCam.position;
			_bloom.Normalize();

			// Raycast
			RaycastHit hit;
			if (Physics.Raycast(normalCam.position, _bloom, out hit, 1000f, canBeShot))
			{
				// Bullet holes
				Debug.Log(hit.collider.gameObject);
				if (hit.collider.gameObject.layer == 11)
				{
					hit.collider.transform.root.GetComponent<Enemy>().ZombieTakeDamage(loadout[currentIndex].damage);
				}
				else
				{
					GameObject _newHole = Instantiate(bulletHolePrefab, hit.point + hit.normal * 0.001f, Quaternion.identity) as GameObject;
					_newHole.transform.LookAt(hit.point + hit.normal);
					Destroy(_newHole, 5f);
					//Alert zombies
					if (gm.currentState == GameManager.State.Zombies)
					{
						Collider[] hitColliders = Physics.OverlapSphere(_newHole.transform.position, pl.bulletHoleSoundRadius, pl.zombieLayerMask);
						for (int i = 0; i < hitColliders.Length; i++)
						{
							hitColliders[i].transform.root.GetComponent<Enemy>().SetTarget(pl.transform);
						}
					}
				}
			}

			// Gun FX
			currentWeapon.transform.Rotate(-loadout[currentIndex].rotationRecoil, 0, 0);
			currentWeapon.transform.position -= currentWeapon.transform.forward * loadout[currentIndex].movementRecoil;

			// Cooldown
			currentCooldown = loadout[currentIndex].firerate;

			//Alert zombies
			if (gm.currentState == GameManager.State.Zombies)
			{
				Collider[] hitColliders = Physics.OverlapSphere(pl.transform.position, pl.shootSoundRadius, pl.zombieLayerMask);
				for (int i = 0; i < hitColliders.Length; i++)
				{
					hitColliders[i].transform.root.GetComponent<Enemy>().SetTarget(pl.transform);
				}
			}
		}
	}
}