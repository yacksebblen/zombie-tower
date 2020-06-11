using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Jackseb.Zombie
{
	public class Sway : MonoBehaviour
	{
		public float intensity;
		public float smooth;

		private Quaternion originRotation;

		private void Start()
		{
			originRotation = transform.localRotation;
		}

		private void Update()
		{
			UpdateSway();
		}

		private void UpdateSway()
		{
			// Controls
			float _xMouse = Input.GetAxis("Mouse X");
			float _yMouse = Input.GetAxis("Mouse Y");

			// Calculate target rotation
			Quaternion _xAdj = Quaternion.AngleAxis(intensity * _xMouse, -Vector3.up);
			Quaternion _yAdj = Quaternion.AngleAxis(intensity * _yMouse, Vector3.right);
			Quaternion _targetRotation = originRotation * _xAdj * _yAdj;

			// Rotate towards target rotation
			transform.localRotation = Quaternion.Lerp(transform.localRotation, _targetRotation, Time.deltaTime * smooth);
		}
	}
}