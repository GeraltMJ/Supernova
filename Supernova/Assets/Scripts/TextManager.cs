using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {
	public Text beginText;

	// Use this for initialization
	void Start () {
		beginText.text = "第二套全国中学生广播体操";	
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > 14.0f)
		{
			beginText.text = "";
		}
		else if(Time.time > 13.5f)
		{
			beginText.text = "预备起！";
		}
		else if(Time.time > 12.5f)
		{
			beginText.text = "1!!!";
		}
		else if(Time.time > 11.5f)
		{
			beginText.text = "2!!!";
		}
		else if(Time.time > 10.5f)
		{
			beginText.text = "3!!!";
		}
		else if(Time.time > 7.0f)
		{
			beginText.text = "登登登登登登";
		}
		else if(Time.time > 5.0f)
		{
			beginText.text = "时代在召唤";
		}
	}
}
