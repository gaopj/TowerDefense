﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
	public int hp = 150;
	private int totalHp ;
	public float speed = 10;

	public GameObject explosionEffect;
	private Transform[] positions;
	private int index = 0;

	private Slider hpSlider;
	// Use this for initialization
	void Start () {
		totalHp = hp;
		positions = WayPoints.positions;
		hpSlider = GetComponentInChildren<Slider> ();
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	void Move(){
		if (index > positions.Length - 1)
			return;
		transform.Translate ((positions [index].position - transform.position).normalized * Time.deltaTime * speed);
		if (Vector3.Distance (positions[index].position,transform.position)<0.5) {
			index++;
		}
		if (index > positions.Length - 1) {
			ReachDestination ();
		}
	}

	void ReachDestination(){
		GameObject.Destroy (this.gameObject);
	}

	void OnDestroy(){
		EnemySpawner.CountEnemyAlive--;
	}

	public void TakeDamage(int damage){
		if (hp <= 0)
			return;
		hp -= damage;
		hpSlider.value = (float)hp / totalHp;
		if (hp <= 0) {
			Die ();
		}
	}

	void Die(){
		GameObject effect= GameObject.Instantiate (explosionEffect, transform.position, transform.rotation);
		Destroy (effect,1.5f);
		Destroy (this.gameObject);
	}
}
