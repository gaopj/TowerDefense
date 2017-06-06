using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	private List<GameObject> enemys = new List<GameObject> ();

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
	public Transform head;

	public bool userLaser = false;
	public float damageRate = 70;
	public LineRenderer laserRender;
	public GameObject laserEffect;

	void Start(){
		timer = attackRateTime;
	}
	void Update(){
		timer += Time.deltaTime;
		if (enemys.Count > 0 && enemys [0] != null ) {
			Vector3 targetPosition = enemys [0].transform.position;
			targetPosition.y = head.position.y;
			head.LookAt (targetPosition);
		}
		if (!userLaser) {
			timer += Time.deltaTime;
			if (enemys.Count > 0 && timer >= attackRateTime) {
				timer = 0;
				Attack ();
			}
		} else if (enemys.Count > 0) {
			if (enemys [0] == null) {
				UpdateEnemys ();
			}
			if (enemys.Count > 0) {
				if (laserRender.enabled == false) {
					laserRender.enabled = true;
					laserEffect.SetActive (true);
				}
	
				laserRender.SetPositions (new Vector3[]{ firePosition.position, enemys [0].transform.position });
				enemys[0].GetComponent<Enemy> ().TakeDamage (damageRate *Time.deltaTime);
				laserEffect.transform.position = enemys [0].transform.position;
				Vector3 pos = transform.position;
				pos.y = enemys [0].transform.position.y;
				laserEffect.transform.LookAt (pos);
			}
		} else {
			laserEffect.SetActive (false);
			laserRender.enabled = false;
		}

	}

	void Attack(){
		if (enemys [0] == null) {
			UpdateEnemys ();
		}
		if (enemys.Count > 0) {
			GameObject bullet = GameObject.Instantiate (bulletPrefab, firePosition.position, firePosition.rotation);
			bullet.GetComponent<Bullet> ().SetTarget (enemys [0].transform);
		} else {
			timer = attackRateTime;
		}
	}

	void UpdateEnemys(){
		List<int> emptyIndex = new List<int>();
		for (int i = 0; i < enemys.Count; i++) {
			if (enemys [i] == null) {
				emptyIndex.Add (i);
			}
		}

		for (int i = 0; i < emptyIndex.Count; i++) {
			enemys.RemoveAt (emptyIndex [i]-i);
		}
	}
}
