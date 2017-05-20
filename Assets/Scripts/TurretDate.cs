using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretDate  {

	public GameObject turretPrefab;
	public GameObject turretUpgradedPrefab;
	public int cost;
	public int costUpgrade;
	public TurretType type;
}

public enum TurretType{
	LaserTurret,
	MissileTurret,
	StandarTurret
}