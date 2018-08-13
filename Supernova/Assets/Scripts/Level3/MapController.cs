using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

	// Use this for initialization
	public float firstChange = 10f;
	public float secondChange = 45f;
	public float changeDuration = 10f;
	private float timer = 0;
	public Sprite poisonImage, areaImage, windImage;
	

	void Start () {
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer > secondChange + changeDuration)
		{
			WindToPoisonArea();
		}
		else if(timer > secondChange)
		{
			PoisonAreaToWind();
		}
		else if(timer > firstChange + changeDuration)
		{
			WindToPoisonArea();
		}
		else if(timer > firstChange)
		{
			PoisonAreaToWind();
		}
	}

	void WindToPoisonArea()
	{
		GameObject[] poisonPics = GameObject.FindGameObjectsWithTag("Wind_Poison");
		GameObject[] areaPics = GameObject.FindGameObjectsWithTag("Wind_Area");
		foreach( GameObject poisonPic in poisonPics)
		{
			poisonPic.tag = "PoisonPic";
			poisonPic.GetComponent<SpriteRenderer>().sprite = poisonImage;
		}
		foreach( GameObject areaPic in areaPics)
		{
			areaPic.tag = "AreaPic";
			areaPic.GetComponent<SpriteRenderer>().sprite = areaImage;
		}

	}

	void PoisonAreaToWind()
	{
		GameObject[] poisonPics = GameObject.FindGameObjectsWithTag("PoisonPic");
		GameObject[] areaPics = GameObject.FindGameObjectsWithTag("AreaPic");
		foreach( GameObject poisonPic in poisonPics)
		{
			poisonPic.tag = "Wind_Poison";
			poisonPic.GetComponent<SpriteRenderer>().sprite = windImage;
		}
		foreach( GameObject areaPic in areaPics)
		{
			areaPic.tag = "Wind_Area";
			areaPic.GetComponent<SpriteRenderer>().sprite = windImage;
		}

	}
}
