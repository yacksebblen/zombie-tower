using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Jackseb.Zombie
{
	public class Look : MonoBehaviour
	{
		public static bool cursorLocked = true;

		public Transform player;
		public Transform cams;
		public Transform weapon;

		public float sensitivity;
		public float maxAngle;

		private Quaternion camCenter;

		void Start()
		{
			camCenter = cams.localRotation; // set rotation origin for camera
		}

		void Update()
		{
			SetY();
			SetX();

			UpdateCursorLock();
		}

		void SetY()
		{
			float _input = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
			Quaternion _adj = Quaternion.AngleAxis(_input, -Vector3.right);
			Quaternion _delta = cams.localRotation * _adj;

			if (Quaternion.Angle(camCenter, _delta) < maxAngle)
			{
				cams.localRotation = _delta;
			}

			weapon.rotation = cams.rotation;
		}

		void SetX()
		{
			float _input = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
			Quaternion _adj = Quaternion.AngleAxis(_input, Vector3.up);
			Quaternion _delta = player.localRotation * _adj;
			player.localRotation = _delta;
		}

		void UpdateCursorLock()
		{
			if (cursorLocked)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;

				if (Input.GetKeyDown(KeyCode.Escape))
				{
					cursorLocked = false;
				}
			}
			else
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;

				if (Input.GetKeyDown(KeyCode.Escape))
				{
					cursorLocked = true;
				}
			}
		}
	}
}