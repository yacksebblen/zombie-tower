using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	public Transform spawnPoint;

	[SerializeField]
	bool spawnPlayer;
	
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
		Instantiate(playerPrefab, spawnPoint.position, spawnPoint.localRotation);
	}
}
