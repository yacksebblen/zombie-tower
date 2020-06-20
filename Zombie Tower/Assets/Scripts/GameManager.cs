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
		public Transform playerSpawn;

		public GameObject zombiePrefab;
		public Transform[] zombieSpawnPoints;

		[SerializeField]
		bool spawnPlayer = true;

		void Start()
		{
			currentState = State.Pregame;

			if (spawnPlayer) Spawn();
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.U))
			{
				SpawnZombie(1);
			}
		}

		public void Spawn()
		{
			Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
		}

		public void SpawnZombie(int numOfTimes)
		{
			for (int i = 0; i < numOfTimes; i++)
			{
				int num = Random.Range(0, zombieSpawnPoints.Length);
				Instantiate(zombiePrefab, zombieSpawnPoints[num].position, zombieSpawnPoints[num].rotation);
			}
		}
	}
}