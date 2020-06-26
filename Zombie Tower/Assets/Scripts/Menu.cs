using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Com.Jackseb.Zombie
{
	public class Menu : MonoBehaviour
	{
		public string version;
		public TextMeshProUGUI versionText;

		void Start()
		{
			versionText.SetText(version);
		}

		void Update()
		{

		}

		public void StartGame()
		{
			SceneManager.LoadScene(FloorLibrary.RandomFloor(0).sceneIndex);
		}
	}
}