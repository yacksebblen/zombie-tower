using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Jackseb.Zombie
{
	[CreateAssetMenu(fileName = "New Floor", menuName = "Floor")]
	public class Floor : ScriptableObject
	{
		public string floorName;
		public int sceneIndex;
		[TextArea]
		public string description;
	}
}