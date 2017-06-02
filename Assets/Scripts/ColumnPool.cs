using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPool : MonoBehaviour {

	public GameObject columnPrefab;
	public int columnPoolSize = 10;
	public float spawnRate = 3f;
	public float columnMin = -1f;
	public float columnMax = 3.5f;

	private GameObject[] columns;
	//private List<GameObject> columns;
	private int currentColumn = 0;
	private GameObject pandicorn; 

	private Vector2 objectPoolPosition = new Vector2 (-15, -25);
	private float spawnXPosition = 10f;

	private float timeSinceLastSpawned;

//	void generateColumns() {
//		GameObject baseColumn = columns [columns.Count - 1];
//		GameObject newColumn = Instantiate (baseColumn, new Vector3 (baseColumn.transform.position.x + 4, baseColumn.transform.position.y, 0), Quaternion.identity);
//		columns.Add (columnPool);
//	}
	// Use this for initialization
	void Start () {
		pandicorn = GameObject.Find("pandicorn_0");
		timeSinceLastSpawned = 0f;
		columns = new GameObject[columnPoolSize];
		for (int i = 0; i < columnPoolSize; i++) {
			columns [i] = (GameObject)Instantiate (columnPrefab, objectPoolPosition, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceLastSpawned += Time.deltaTime;
		if (timeSinceLastSpawned >= spawnRate) {
			timeSinceLastSpawned = 0f;
			float spawnYPosition = Random.Range (columnMin, columnMax);
			columns [currentColumn].transform.position = new Vector2 (pandicorn.transform.position.x+8, spawnYPosition);
			currentColumn++;
			if (currentColumn >= columnPoolSize) {
				currentColumn = 0;
			}
		}
	}
}
