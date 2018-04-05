using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickContact : MonoBehaviour {
	private Renderer rend;

	public AudioClip[] boxToBoxHits;
	public AudioClip[] boxToFloorHits;
	public AudioClip[] sphereToBoxHits;
	public AudioClip humstartClip;
	public AudioClip humloopClip;
	private AudioSource[] audioSources;

	private AudioSource humAudioSource;
	private AudioSource boxOnBoxAudioSource;
	private AudioSource boxHitAudioSource;

	private float rawVelocity = 2f;

	private Coroutine humloop;

	bool lowerHum = false;
	float startLowerHumTime = 0;

	bool brickIsHit = false;
	void Start () {
        rend = GetComponentInChildren<Renderer>();
		audioSources = GetComponents<AudioSource>();
		boxOnBoxAudioSource = audioSources[0];
		boxHitAudioSource = audioSources[1];
		humAudioSource = audioSources[2];
		humAudioSource.clip = humstartClip;

		humloop = StartCoroutine(BrickHumLoop());
	}
	
	// Update is called once per frame
	void Update () {
		if (lowerHum && startLowerHumTime <= 1) {
			humAudioSource.volume = Mathf.Lerp(1, 0, startLowerHumTime);
			startLowerHumTime += 0.1f;
		}
	}

	void playRandomClip(AudioClip[] clips, AudioSource aSource) {
		float volume = Mathf.Min(Mathf.Max(rawVelocity / 4, 0.25f), 1);
		float pitch = Random.Range(1 - (volume / 10), 1 + (volume / 10));

		int randomIndex = Random.Range(0, clips.Length - 1);

		AudioClip clip = clips[randomIndex];		
		aSource.volume = volume;
		aSource.pitch = pitch;
		aSource.clip = clip;
		aSource.Play();
	}
	void OnCollisionEnter (Collision col)
    {
		rawVelocity = gameObject.GetComponent<Rigidbody>().velocity.magnitude;	

		if(col.gameObject.tag == "sphere" && !brickIsHit)
		{
			EventManager.TriggerEvent("brickHit");
			StartCoroutine(DestroyBrick());
			playRandomClip(sphereToBoxHits, boxHitAudioSource);
			StopCoroutine(humloop);
			brickIsHit = true;
		}
		if(col.gameObject.tag == "Floor") {
			playRandomClip(boxToFloorHits, boxOnBoxAudioSource);
		}
		if(col.gameObject.tag == "Brick") {
			playRandomClip(boxToBoxHits, boxOnBoxAudioSource);
		}

    }
	IEnumerator DestroyBrick() {		
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_SpecColor", Color.red);		
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
