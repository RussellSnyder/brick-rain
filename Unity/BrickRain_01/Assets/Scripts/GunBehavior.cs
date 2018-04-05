using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : MonoBehaviour {

    public float shotForce = 1500f;
    public GameObject projectile;
   
	private GameObject camera;
	private AudioSource gunShot;
	void Start() {
		gunShot = GetComponent<AudioSource>();
		camera = GameObject.FindGameObjectWithTag("MainCamera");
	}
    void Update () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
			GameObject throwThis; 
			throwThis = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
			StartCoroutine(FireProjectile(throwThis));
        }

    }

	IEnumerator FireProjectile(GameObject bullet) {
		gunShot.pitch = Random.Range(0.98f, 1.02f);
		gunShot.volume = Random.Range(0.9f, 1f);
		gunShot.PlayOneShot(gunShot.clip);			

		bullet.GetComponent<Rigidbody>().AddForce(camera.transform.forward * shotForce);

		yield return null;
	}
	

}
