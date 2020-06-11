using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Jackseb.Zombie
{
	[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
	public class Gun : ScriptableObject
	{
		public string gunName;
		public float firerate;
		public GameObject prefab;
	}
}