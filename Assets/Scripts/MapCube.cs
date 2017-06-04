using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour {
	[HideInInspector]
	public GameObject turretGo;//保存当前Cube身上炮台
	private TurretDate turretData;

	[HideInInspector]
	public bool isUpgraded = false;

	public GameObject buildEffect;

	private Renderer renderers;

	void Start(){
		renderers = GetComponent<Renderer> ();
	}
	public void BuildTurret(TurretDate turretData){
		this.turretData = turretData;

		if(turretData.turretPrefab!=null)
			turretGo= GameObject.Instantiate (turretData.turretPrefab,transform.position,Quaternion.identity);
		GameObject effect= GameObject.Instantiate (buildEffect,transform.position,Quaternion.identity);
		isUpgraded = false;
		Destroy (effect, 1.5f);
	}

	public void UpgradeTurret(){
		if (isUpgraded == true) {
			return;
		}
		Destroy (turretGo);
		turretGo= GameObject.Instantiate (turretData.turretUpgradedPrefab,transform.position,Quaternion.identity);
		GameObject effect= GameObject.Instantiate (buildEffect,transform.position,Quaternion.identity);
		Destroy (effect, 1.5f);
		isUpgraded = true;
	}

	public void DestroyTurret(){
		Destroy (turretGo);
		GameObject effect= GameObject.Instantiate (buildEffect,transform.position,Quaternion.identity);
		Destroy (effect, 1.5f);
		isUpgraded = false;
		turretGo = null;
		turretData = null;
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
