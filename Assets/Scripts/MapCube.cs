using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour {
	[HideInInspector]
	public GameObject turretGo;//保存当前Cube身上炮台
	public GameObject buildEffect;

	private Renderer renderers;

	void Start(){
		renderers = GetComponent<Renderer> ();
	}
	public void BuildTurret(GameObject turretPrefab){
		if(turretPrefab!=null)
		turretGo= GameObject.Instantiate (turretPrefab,transform.position,Quaternion.identity);
		GameObject effect= GameObject.Instantiate (buildEffect,transform.position,Quaternion.identity);
		Destroy (effect, 1);
	}

	void OnMouseEnter(){
		if (turretGo == null&&!EventSystem.current.IsPointerOverGameObject()) {
			renderers.material.color = Color.red;
		}
	}
	void OnMouseExit(){
		renderers.material.color = Color.white;
	}
}
