using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickContact : MonoBehaviour {
	private Renderer rend;

	public AudioClip[] boxToBoxHits;
	public AudioClip[] boxToFloorHits;
	public AudioClip[] sphereToBoxHits;
	private AudioSource[] audioSources;

	private AudioSource humAudioSource;
	private float rawVelocity = 2f;

	Coroutine humloop;
	public AudioClip humloopClip;

	bool lowerHum = false;
	float startLowerHumTime = 0;
	void Start () {
        rend = GetComponentInChildren<Renderer>();
		audioSources = GetComponents<AudioSource>();
		humAudioSource = audioSources[2];
		humloop = StartCoroutine(BrickHumLoop());
	}
	
	// Update is called once per frame
	void Update () {
		if (lowerHum && startLowerHumTime <= 1) {
			humAudioSource.volume = Mathf.Lerp(1, 0, startLowerHumTime);
			startLowerHumTime += 0.1f;
		}
	}

	void playRandomClip(AudioClip[] clips) {
		float volume = Mathf.Min(Mathf.Max(rawVelocity / 4, 0.25f), 1);
		float pitch = Random.Range(1 - (volume / 10), 1 + (volume / 10));

		int audioSourceToPlay = audioSources[0].isPlaying ? 1 : 0;
		int randomIndex = Random.Range(0, clips.Length - 1);


		AudioClip clip = clips[Random.Range(0, clips.Length - 1)];		
		audioSources[audioSourceToPlay].volume = volume;
		audioSources[audioSourceToPlay].pitch = pitch;
		audioSources[audioSourceToPlay].clip = clip;
		audioSources[audioSourceToPlay].Play();
	}
	void OnCollisionEnter (Collision col)
    {
		rawVelocity = gameObject.GetComponent<Rigidbody>().velocity.magnitude;	

        if(col.gameObject.tag == "sphere")
        {
			EventManager.TriggerEvent("brickHit");
			StartCoroutine(DestroyBrick());
			playRandomClip(sphereToBoxHits);
        }
		if(col.gameObject.tag == "Floor") {
			playRandomClip(boxToFloorHits);
		}
		if(col.gameObject.tag == "Brick") {
			playRandomClip(boxToBoxHits);
		}

    }
	IEnumerator DestroyBrick() {		
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_SpecColor", Color.red);		
		yield return new WaitForSeconds(0.75f);
		lowerHum = true;
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}
	IEnumerator BrickHumLoop() {
		humAudioSource.volume = Random.Range(0.2f, 0.6f);
		humAudioSource.pitch = Random.Range(0.7f, 1.03f);
		humAudioSource.Play();
		yield return new WaitForSeconds(humAudioSource.clip.length);
		humAudioSource.clip = humloopClip;
		humAudioSource.loop = true;
		yield return null;		
	}
}
