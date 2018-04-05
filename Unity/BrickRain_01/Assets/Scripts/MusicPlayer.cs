using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using System;
#if UnityEditor
using UnityEditor;
#endif
public class MusicPlayer : MonoBehaviour {
	public static MusicPlayer instance = null;

	public AudioMixer mixer;
	public List<AudioMixerSnapshot> introSnapshots;
	public List<AudioMixerSnapshot> mainSectionSnapshots;

	private AudioSource[] aSources;

	Coroutine co;
	public static MusicPlayer Instance
     {
         get
         { 
             return instance; 
         }
     }

	void Awake() {
		if (instance == null)
			instance = this;
            
            else if (instance != this)
                Destroy(gameObject);    
            
            DontDestroyOnLoad(gameObject);

		aSources = GetComponents<AudioSource>();
		introSnapshots[0].TransitionTo(0);
		TriggerIntroMusic();
	}

	public void TriggerIntroMusic() {
		co = StartCoroutine(PlayIntro());
	}
	IEnumerator PlayIntro() {
		yield return new WaitForSeconds(aSources[0].clip.length);
		introSnapshots[1].TransitionTo(0);
	}
	public void TriggerMainMusic() {
		if (co != null) {
			StopCoroutine(co);
		}
		co = StartCoroutine(PlayMainMusic());
	}

	public void replayMain() {

	}

	private float getTimeToWait(int beatDivision = 8) {
		AudioSource aSource = aSources[0];
		float clipLength = aSource.clip.length;
		float beatLength = clipLength / beatDivision;
		float percentDone = aSource.time / clipLength;
		int currentBeat = (int)Mathf.Floor(percentDone * beatDivision);
		float nextBeatTime = (currentBeat + 1) * beatLength;
		return nextBeatTime - aSource.time;  
	}
	IEnumerator PlayMainMusic() {
		Debug.Log("I fired 1");
		AudioSource aSource = aSources[0];
		float clipLength = aSource.clip.length;

		yield return new WaitForSeconds(getTimeToWait());	
			foreach(AudioSource mySource in aSources) {
				mySource.Play();
			}
			Debug.Log("snapshot index 2 fire");
			mainSectionSnapshots[0].TransitionTo(0);
	}
	
	public void PlayMainMusicTransitions(float count){
		StopCoroutine(co);

		float[] weights = new float[3];
		if (count == 15) {
			Debug.Log("Enter next level music");
		}
		if (count == 35) {
			Debug.Log("Max musix playing");
		}
        if (count > 0 && count <= 15) {			
			weights[0] = 1f - (count / 10f);
			weights[1] = count / 10f;
			weights[2] = 0;
			mixer.TransitionToSnapshots(mainSectionSnapshots.ToArray(), weights, 0.5f);
        } else if (count > 15 && count < 35) {
			weights[0] = 0;
			weights[1] = 1f - (count / 35f);
			weights[2] = count / 35f;
			mixer.TransitionToSnapshots(mainSectionSnapshots.ToArray(), weights, 1.5f);
        }

	}

	public void TriggerWinMusic() {
		// float timeUntilEndOfClip = aSources[0].clip.length - aSources[0].time;
		// float idealTransitionTime = aSources[0].clip.length / 4;
		// float transitionTime = timeUntilEndOfClip < idealTransitionTime ? timeUntilEndOfClip : idealTransitionTime;
		// float timeToWait = timeUntilEndOfClip - transitionTime;

		// StopCoroutine(co);
		// co = StartCoroutine(PlayWinMusic());
	}

	IEnumerator PlayWinMusic() {
//		yield return new WaitForSeconds(getTimeToWait(8));	

		introSnapshots[0].TransitionTo(getTimeToWait(4));
		yield return null;
	}

	public void RetriggerMainMusic() {
		// foreach(AudioSource mySource in aSources) {
		// 	mySource.Play();
		// }
		// Debug.Log("snapshot index 2 fire");
		// mainSectionSnapshots[0].TransitionTo(0);
	}
}