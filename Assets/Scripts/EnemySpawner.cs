﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public static int CountEnemyAlive = 0;
	public Wave[] waves;
	public Transform START;
	public float waveRate = 0.3f;
	private Coroutine coroutine;
	void Start(){
		coroutine=StartCoroutine (SpawnEnymy());
	}

	public void Stop(){
		//StopCoroutine ("SpawnEnymy");
		StopCoroutine (coroutine);
	}

	IEnumerator SpawnEnymy(){
		foreach (Wave wave in waves) {
			for (int i = 0; i < wave.count; i++) {
				GameObject.Instantiate (wave.enemyPrefab, START.position,Quaternion.identity);
				CountEnemyAlive++;
				if(i!=wave.count-1)
					yield return new WaitForSeconds (wave.rate);
			}
			while (CountEnemyAlive > 0) {
				yield return 0;
			}
			yield return new WaitForSeconds (waveRate);
		}

		while (CountEnemyAlive > 0) {
			yield return 0;
		}
		GameManager.Instance.Win ();
	}
}
