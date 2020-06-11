﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Jackseb.Zombie
{
	public class Weapon : MonoBehaviour
	{
		public Gun[] loadout;
		public Transform weaponParent;

		private GameObject currentWeapon;

		void Start()
		{

		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Alpha1)) Equip(0);
		}

		void Equip(int p_ind)
		{
			if (currentWeapon != null) Destroy(currentWeapon);

			GameObject _newWeapon = Instantiate(loadout[p_ind].prefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
			_newWeapon.transform.localPosition = Vector3.zero;
			_newWeapon.transform.localEulerAngles = Vector3.zero;

			currentWeapon = _newWeapon;
		}
	}
}