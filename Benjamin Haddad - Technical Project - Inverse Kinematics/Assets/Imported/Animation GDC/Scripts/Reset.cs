using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


/// <summary>
/// Resets the game
/// </summary>
public class Reset : MonoBehaviour {
	void OnGUI() 
	{
		if (GUI.Button(new Rect(Screen.width-65, 0, 65, 20),"Reset"))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
