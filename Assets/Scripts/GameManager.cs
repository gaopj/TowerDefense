using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject endUI;
	public Text endMessage;
	public static GameManager Instance;
	private EnemySpawner enemySpawner;

	void Awake(){
		Instance = this;
		enemySpawner = GetComponent<EnemySpawner> ();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Win(){
		endUI.SetActive (true);
		endMessage.text="Victory";
	}

	public void Failed(){
		enemySpawner.Stop ();
		endUI.SetActive (true);
		endMessage.text="Failure";
	}

	public void OnButtonRetry(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}
		
	public void OnButtonMenu(){
		SceneManager.LoadScene (0);
	}

}
