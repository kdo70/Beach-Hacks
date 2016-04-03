using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] prefabs;
	public Vector3 spawnValues;
	public int hazardCount = 10;
	public float spawnWait = 1f;
	public float startWait = 1f;
	public float waveWait = 5f;
	public float xOffset = 10f;
	public float yOffset = 7f;
	public bool active;


	void Awake () {

	}

	void Start () {
		StartCoroutine (SpawnWaves ());
	}

	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);
		while (active) {
			for (int i = 0; i < hazardCount; i++) {

				Vector3 spawnPosition = new Vector3 (0, 0, 0);
				Quaternion spawnRotation = Quaternion.identity;
				GameObject random = (GameObject) Instantiate (prefabs [Random.Range (0, prefabs.Length)], spawnPosition,spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
		}
	}
}