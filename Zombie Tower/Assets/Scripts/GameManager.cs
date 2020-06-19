using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Jackseb.Zombie
{
	public class GameManager : MonoBehaviour
	{
		public enum State
		{
			Pregame,
			NewFloor,
			Zombies,
			PVP,
			Elevator
		}

		public State currentState;

		public GameObject playerPrefab;
		public Transform[] spawnPoints;

		[SerializeField]
		bool spawnPlayer = true;

		void Start()
		{
			currentState = State.Pregame;

			if (spawnPlayer) Spawn();
		}

		void Update()
		{

		}

		public void Spawn()
		{
			int num = Random.Range(0, 3);
			Instantiate(playerPrefab, spawnPoints[num].position, spawnPoints[num].rotation);
		}
	}
}