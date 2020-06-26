using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Jackseb.Zombie
{
	public class ElevatorDoors : MonoBehaviour
	{
		public GameManager gm;

		public Vector3 openPos;
		public Vector3 closePos;
		public float smoothness;

		[HideInInspector]
		public bool open;

		void Start()
		{
			gm = GameObject.Find("GameManager").GetComponent<GameManager>();

			open = false;
		}

		void Update()
		{
			if (open)
			{
				transform.position = Vector3.Lerp(transform.position, openPos, Time.deltaTime * smoothness);
			}
			else
			{
				transform.position = Vector3.Lerp(transform.position, closePos, Time.deltaTime * smoothness);
			}
		}

		void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.layer == 9 && gm.currentState == GameManager.State.Elevator)
			{
				open = false;
				StartCoroutine(FloorDelay());
			}
		}

		IEnumerator FloorDelay()
		{
			yield return new WaitForSeconds(2);
			gm.NewFloor();
		}

		void OnTriggerExit(Collider other)
		{
			if (other.gameObject.layer == 9)
			{
				open = false;
			}
		}
	}
}