using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bgscroller : MonoBehaviour
{
    public GameObject bgTile;
    public Sprite bg0;
    public Sprite bg1;

    public GameObject groundTile;
    public Sprite ground0;
    public Sprite ground1;
    public GameObject background0;
    public GameObject background1;

    private List<GameObject> bgtiles;
    private List<GameObject> groundtiles;

    void generateBGTile() {
    	GameObject baseTile = bgtiles[bgtiles.Count - 1];    	
    	if (Random.Range(0, 2) == 0) {
    		GameObject newtile = Instantiate(background0, new Vector3(baseTile.transform.position.x + 4, baseTile.transform.position.y, 0), Quaternion.identity);
    		newtile.name = "bg";
	    	newtile.GetComponent<SpriteRenderer>().sprite = bg0; 
	    	bgtiles.Add(newtile);
    	} else {
    		GameObject newtile = Instantiate(background1, new Vector3(baseTile.transform.position.x + 4, baseTile.transform.position.y, 0), Quaternion.identity);
    		newtile.GetComponent<SpriteRenderer>().sprite = bg1;
    		newtile.name = "bg";
    		GameObject ad = new GameObject("cool ad");
    		ad.AddComponent<ShowAd>();
    		ad.GetComponent<ShowAd>().player = ad;
    		ad.AddComponent<SpriteRenderer>();
    		// ad.AddComponent<MeshFilter>();
    		// ad.GetComponent<MeshFilter>().mesh = mesh;
    		ad.transform.parent = newtile.transform;
    		ad.transform.localPosition = new Vector3(.12f,7.19f,-1f);
    		ad.transform.localScale = new Vector3(0.8875f, 1.803f,1f);
    		// ad.transform.rotation = new Quaternion(0, 180, 0 ,0);
    		bgtiles.Add(newtile);
    	}
    	
    	
    }

    void generateGroundTile() {
    	GameObject baseTile = groundtiles[groundtiles.Count - 1];
    	GameObject newtile = Instantiate(baseTile, new Vector3(baseTile.transform.position.x + 1, baseTile.transform.position.y, 0), Quaternion.identity);
    	newtile.name = "ground";
    	if (Random.Range(0, 2) == 0) {
	    	newtile.GetComponent<SpriteRenderer>().sprite = ground0; 
    	} else {
    		newtile.GetComponent<SpriteRenderer>().sprite = ground1; 
    	}
    	
    	groundtiles.Add(newtile);
    }


    void Start () {
        bgtiles = new List<GameObject>();
        bgtiles.Add(bgTile);
        for (int i = 0; i < 4; i++) {
        	generateBGTile();
        }

        groundtiles = new List<GameObject>();
        groundtiles.Add(groundTile);
        for (int i = 0; i < 19; i++) {
        	generateGroundTile();
        }
    }

    void Update () {
        if (bgtiles[0].transform.position.x + 128/64 < transform.position.x - 512/64) {
        	GameObject deleteTile = bgtiles[0];
        	bgtiles.RemoveAt(0);
        	Destroy(deleteTile);
        }

        if (groundtiles[0].transform.position.x + 64/64 < transform.position.x - 512/64) {
        	GameObject deleteTile = groundtiles[0];
        	groundtiles.RemoveAt(0);
        	Destroy(deleteTile);
        }

        while (bgtiles.Count < 5) {
        	generateBGTile();
        }

        while (groundtiles.Count < 20) {
        	generateGroundTile();
        }
    }
}