using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Jackseb.Zombie
{
	public class Motion : MonoBehaviour
	{
		public float speed;

		private Rigidbody rig;

		private void Start()
		{
			Camera.main.enabled = false;
			rig = GetComponent<Rigidbody>();
		}

		void FixedUpdate()
		{
			float _hMove = Input.GetAxisRaw("Horizontal");
			float _vMove = Input.GetAxisRaw("Vertical");

			Vector3 _direction = new Vector3(_hMove, 0, _vMove);
			_direction.Normalize();

			rig.velocity = transform.TransformDirection(_direction) * speed * Time.deltaTime;
		}
	}
}