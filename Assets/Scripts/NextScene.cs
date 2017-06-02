using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {
	public string SceneName = "flappanda";
	// Use this for initialization
	private bool clicked = false;
	public void ChangeScene (string sceneName) {
		SceneManager.LoadScene (sceneName);
	}
}
