﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Com.Jackseb.Zombie
{
	public class GameManager : MonoBehaviour
	{
		public enum State
		{
			Pregame,
			Zombies,
			Elevator
		}

		public State currentState;

		public GameObject playerPrefab;
		public Transform playerSpawn;

		public GameObject zombiePrefab;
		public Transform[] zombieSpawnPoints;

		[SerializeField]
		bool spawnPlayer = true;

		Transform uiState;
		int currentZombieCount = -1;

		void Start()
		{
			currentState = State.Pregame;

			if (spawnPlayer) Spawn();

			uiState = GameObject.Find("HUD/GameInfo/State").transform;

			StartCoroutine(StateDelay(10));
		}

		void Update()
		{
			uiState.GetComponent<TextMeshProUGUI>().SetText(currentState.ToString());
			
			if (currentZombieCount == 0)
			{
				currentState = State.Elevator;
			}

			// test code
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
			for (int i = 0; i < zombieSpawnPoints.Length; i++)
			{
				zombieSpawnPoints[i].gameObject.SetActive(true);
			}

			for (int i = 0; i < numOfTimes; i++)
			{
				int num = Random.Range(0, zombieSpawnPoints.Length);
				if (zombieSpawnPoints[num].gameObject.activeSelf == true)
				{
					Instantiate(zombiePrefab, zombieSpawnPoints[num].position, zombieSpawnPoints[num].rotation);
					zombieSpawnPoints[num].gameObject.SetActive(false);
				}
			}
		}

		public void ChangeZombieCount(int amt)
		{
			currentZombieCount += amt;
		}

		IEnumerator StateDelay(float sec)
		{
			yield return new WaitForSeconds(sec);
			currentState = State.Zombies;
			currentZombieCount = Random.Range(3, 9);
			SpawnZombie(currentZombieCount);
		}
	}
}