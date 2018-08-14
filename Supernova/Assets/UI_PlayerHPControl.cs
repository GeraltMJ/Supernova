using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHPControl : MonoBehaviour {

	public Slider slider;
	public Text currentHPUI;
	public Text maxHPUI;
	public float currentHP = 18.0f;
	public float maxHP = 23.0f;

	// Use this for initialization
	void Start () {
		currentHPUI.text = currentHP.ToString();
		maxHPUI.text = maxHP.ToString();
		slider.value = currentHP / maxHP;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
