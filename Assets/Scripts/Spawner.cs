using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] prefabs;
	public Vector3 spawnValues;
	public int hazardCount = 10;
	public float spawnWait = 1f;
	public float startWait = 1f;
	public float waveWait = 5f;
	public bool active;


	void Start () {
		StartCoroutine (SpawnWaves ());
	}

	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);
		while (active) {
			for (int i = 0; i < hazardCount; i++) {
				GameObject random = (GameObject) GameObjectUtil.Instantiate (prefabs [Random.Range (0, prefabs.Length)], transform.position);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
		}
	}
}