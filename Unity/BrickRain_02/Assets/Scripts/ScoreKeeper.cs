using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

    private UnityAction brickHit;
	private string ScoreTextStart = "Score: ";
	private string ScoreTextEnd = " / 50";
	private int count = 0;
	private Text text;

    void Awake ()
    {
		text = GetComponent<Text>();
        brickHit = new UnityAction (incrementScore);
		updateScoreText();
    }

    void OnEnable ()
    {
        EventManager.StartListening ("brickHit", brickHit);
    }

    void OnDisable ()
    {
        EventManager.StopListening ("brickHit", brickHit);
    }

    void incrementScore ()
    {
		count++;
        Debug.Log("hit");
        MusicPlayer.Instance.PlayMainMusicTransitions((float)count);


		if (count >= 50) {
            LevelManager.LoadLevel("Win");
		}

		updateScoreText();
    }

	void updateScoreText() {
		text.text = ScoreTextStart + count + ScoreTextEnd;
	}
    
}