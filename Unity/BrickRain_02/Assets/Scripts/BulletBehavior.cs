using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

	// Use this for initialization
	AudioSource bulletSound;
	public AudioClip[] bulletSounds;
	public AudioClip explosion;

	Coroutine co;
	void Start () {
		bulletSound = gameObject.GetComponent<AudioSource>();
		bulletSound.clip = bulletSounds[Random.Range(0, bulletSounds.Length - 1)];
		bulletSound.pitch = Random.Range(0.9f, 1f);
		bulletSound.volume = Random.Range(0.3f, 0.6f);
		bulletSound.Play();
		// add bullt clip length here

		co = StartCoroutine(CleanUp());
	}
	
	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Brick") {
			StopCoroutine(co);
			bulletSound.Stop();
			bulletSound.clip = explosion;
			bulletSound.Play();
			gameObject.GetComponent<BoxCollider>().enabled = false;;
			foreach(Transform child in gameObject.transform.GetComponentsInChildren<Transform>()){
				if (child.gameObject.name == "Point Light" || child.gameObject.name == "Sphere") {
					child.gameObject.SetActive(false);
				}
			}
			StartCoroutine(CleanUp());
		}
	}
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator CleanUp() {
		yield return new WaitForSeconds(bulletSound.clip.length);
		Destroy(gameObject);
	}

}
