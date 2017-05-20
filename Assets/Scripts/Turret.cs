using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	public List<GameObject> enemys = new List<GameObject> ();

	void OnTriggerEnter(Collider col){
		if (col.tag == "Enemy") {
			enemys.Add (col.gameObject);
		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag == "Enemy") {
			enemys.Remove (col.gameObject);
		}
	}

	public float attackRateTime = 1;  //多少秒攻击一次
	private float timer = 0;

	public Transform firePosition;
	public GameObject bulletPrefab;//子弹

	void Start(){
		timer = attackRateTime;
	}
	void Update(){
		timer += Time.deltaTime;
		if (enemys.Count>0&& timer >= attackRateTime) {
			timer -= attackRateTime;
			Attack ();
		}
	}

	void Attack(){
		GameObject.Instantiate (bulletPrefab, firePosition.position, firePosition.rotation);
	}
}
