using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {

	private static bool hasWon = false;
	public void LoadLevelFromGUI(string name) {
		LoadLevel(name);
	}
	public static void LoadLevel(string name){
		Debug.Log ("New Level load: " + name);
		if (name == "Level_1") {
			if (!hasWon) {
				MusicPlayer.Instance.TriggerMainMusic();
				hasWon = false;
			} else {
				MusicPlayer.Instance.RetriggerMainMusic();				
			}
		}
		if (name == "Win") {
			MusicPlayer.Instance.TriggerWinMusic();
			hasWon = true;
		}
		SceneManager.LoadScene(name);
	}

	public void QuitRequestFromGUI() {
		QuitRequest();
	}
	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

}
