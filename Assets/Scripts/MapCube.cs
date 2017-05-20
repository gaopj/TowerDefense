using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCube : MonoBehaviour {
	[HideInInspector]
	public GameObject turretGo;//保存当前Cube身上炮台
	public GameObject buildEffect;
	public void BuildTurret(GameObject turretPrefab){
		if(turretPrefab!=null)
		turretGo= GameObject.Instantiate (turretPrefab,transform.position,Quaternion.identity);
		GameObject effect= GameObject.Instantiate (buildEffect,transform.position,Quaternion.identity);
		Destroy (effect, 1);
	}
}
