using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Orb : Entity {

    public TextMeshPro healthText;

	// Use this for initialization
	void Start () {
        healthText.text = health.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        healthText.text = health.ToString();
    }

    new void DoDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            base.OnDeath();
    }
}
