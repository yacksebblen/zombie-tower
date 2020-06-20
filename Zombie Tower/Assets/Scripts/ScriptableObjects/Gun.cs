using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Jackseb.Zombie
{
	[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
	public class Gun : ScriptableObject
	{
		[Header("General")]
		public string gunName;
		public GameObject prefab;

		[Header("Shooting")]
		public int damage;
		public float firerate;
		public float spread;

		[Header("Recoil")]
		public float movementRecoil;
		public float rotationRecoil;
	}
}