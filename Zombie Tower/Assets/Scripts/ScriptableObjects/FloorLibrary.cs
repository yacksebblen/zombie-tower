using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Jackseb.Zombie
{
	public class FloorLibrary : MonoBehaviour
	{
		public Floor[] allFloors;
		public static Floor[] floors;

		private void Awake()
		{
			floors = allFloors;
		}

		public static Floor FindFloor(string name)
		{
			foreach (Floor a in floors)
			{
				if (a.name.Equals(name)) return a;
			}

			return floors[0];
		}

		public static Floor RandomFloor(int prev)
		{
			int floorNum = Random.Range(0, floors.Length);

			while (floorNum == prev)
			{
				floorNum = Random.Range(0, floors.Length);
			}

			return floors[floorNum];
		}
	}
}
