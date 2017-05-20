using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour {
	public TurretDate laserTurretData;
	public TurretDate missileTurretData;
	public TurretDate standarTurretData;

	//表示当前选择的炮台
	private TurretDate selectedTurret;
	public Text moneyText;

	public Animator moneyAnimator;

	private int money = 1000;

	void ChangeMoney(int change=0){
		money += change;
		moneyText.text = "￥" + money;
	}

	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			if (!EventSystem.current.IsPointerOverGameObject ()) {
				//开发炮台的建造
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				bool isCollider = Physics.Raycast (ray,out hit ,1000, LayerMask.GetMask ("MapCube"));
				if (isCollider) {
					MapCube mapCube = hit.collider.GetComponent<MapCube>();//得到点击的mapCube
					if (selectedTurret!=null&&mapCube.turretGo == null) {
						//可以创建
						if (money >= selectedTurret.cost) {
							ChangeMoney (-selectedTurret.cost);
							mapCube.BuildTurret (selectedTurret.turretPrefab);	
						} else {
							//提示钱不够
							moneyAnimator.SetTrigger("Flicker");
						}
					} else if(mapCube.turretGo!=null){
						//升级处理
					}
				}
			}
		}
	}
	public void OnLaserSelected(bool isOn){
		if (isOn) {
			selectedTurret = laserTurretData;
		}
	} 
	public void OnMissileSelected(bool isOn){
		if (isOn) {
			selectedTurret = missileTurretData;
		}
	}
	public void OnStandardSelected(bool isOn){
		if (isOn) {
			selectedTurret = standarTurretData;
		}
	}
}
