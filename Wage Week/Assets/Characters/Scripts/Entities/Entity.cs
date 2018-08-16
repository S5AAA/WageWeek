using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public float health;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DoDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            OnDeath();
    }

    void DoMovement()
    {

    }

    protected void OnDeath()
    {
        Destroy(this.gameObject);
    }

}
