using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeItRain : MonoBehaviour {
	// Use this for initialization
	public int StartingHeight = 10;
	public GameObject BrickNS;
	public GameObject BrickEW;
	private float nextGeneration = 0.25f;

	private Vector3[] StartPositionsNS = new Vector3[10];
	private Vector3[] StartPositionsEW = new Vector3[10];

	void Awake() {
	}
	void Start () {
		int countNS = 0;
		for (int i = -4; i <= 4; i = i + 2) {
			StartPositionsNS[countNS] = new Vector3(i, StartingHeight, 4);
			countNS++;
			StartPositionsNS[countNS] = new Vector3(i, StartingHeight, -4);
			countNS++;
		}
		int countEW = 0;
		for (int i = -4; i <= 4; i = i + 2) {
			StartPositionsEW[countEW] = new Vector3(4, StartingHeight, i);
			countEW++;
			StartPositionsEW[countEW] = new Vector3(-4, StartingHeight, i);
			countEW++;
		}
	}
	void Update () {
		if(Time.time >= nextGeneration){
			setNextGeneration();
			GenerateBrick();
		}		
	}

	void setNextGeneration() {
			nextGeneration = Time.time + Random.Range(0.25f, 1f);
	}
	void GenerateBrick() {
			GameObject brick; 
			int startPositionNS = Random.Range(0, StartPositionsNS.Length);
			int startPositionEW = Random.Range(0, StartPositionsNS.Length);

			if (Random.Range(0, 2) == 0) {
				brick = Instantiate(BrickNS, StartPositionsNS[startPositionNS], transform.rotation) as GameObject;
			} else {
				Quaternion rotation = Quaternion.Euler(0, 90, 0);
				brick = Instantiate(BrickEW, StartPositionsEW[startPositionEW], rotation) as GameObject;
			}

			StartCoroutine(CleanUp(brick));
	}
	IEnumerator CleanUp(GameObject thing) {
		yield return new WaitForSeconds(15);
		Destroy(thing);
	}
}
