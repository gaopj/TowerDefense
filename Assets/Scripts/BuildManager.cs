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

	//场景中显示UI的炮台
	private MapCube selectedMapCube;

	public Text moneyText;

	public Animator moneyAnimator;

	private int money = 1000;

	public GameObject upgradeCanvas;
	public Button buttonUpgrade;

	private Animator upgradeCanvasAnimator;

	void Start(){
		upgradeCanvasAnimator = upgradeCanvas.GetComponent<Animator> ();
	}

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
							mapCube.BuildTurret (selectedTurret);	
						} else {
							//提示钱不够
							moneyAnimator.SetTrigger("Flicker");
						}
					} else if(mapCube.turretGo!=null){
						//升级处理

						if (mapCube == selectedMapCube && upgradeCanvas.activeInHierarchy) {
							StartCoroutine(HideUpgradeUI  ());
						} else {
							ShowUpgradeUI(mapCube.transform.position,mapCube.isUpgraded);
						}
						selectedMapCube = mapCube;
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

	void ShowUpgradeUI(Vector3 pos, bool isDisableUpgrade = false){
		StopCoroutine ("HideUpgradeUI");
		upgradeCanvas.SetActive (false);
		upgradeCanvas.SetActive (true);
		upgradeCanvas.transform.position = pos;
		buttonUpgrade.interactable = !isDisableUpgrade;
	}

	IEnumerator HideUpgradeUI(){
		upgradeCanvasAnimator.SetTrigger ("Hide");
		yield return new WaitForSeconds (0.8f);
		upgradeCanvas.SetActive (false);
	}

	public void OnUpgradeButtonDown(){
		selectedMapCube.UpgradeTurret ();
		StartCoroutine(HideUpgradeUI ());
	}

	public void OnDestroyButtonDown(){
		selectedMapCube.DestroyTurret ();
		StartCoroutine(HideUpgradeUI ());
	}
}
