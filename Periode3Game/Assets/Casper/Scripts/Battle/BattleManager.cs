using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{

	[Header("ObjectLists")]
	public List<GameObject> players;
	public List<GameObject> enemies;

	[Header("Other object")]
	public GameObject damageUI;
	public GameObject[] camPathPrefabs;
	public BattleHealth[] enemyHealthUI;
	private GameObject camSlowMotionEffect;
	public GameObject[] selectAtackUI;
	public GameObject atackNameUI;

	[Header("stats")]
	public float[] charge;
	public int[] playerHealth;
	public List<float> enemyHealth;

	[Header("Handy's & helpers")]
	public float timeScale = 1;
	public bool turnAtack = false;
	//[HideInInspector]
	public string state = "normal";
	public int atackingPlayer = 2;
	private float coolDownTimer = 0;
	public Image fadeIn;

	private GameObject victory;

	void Start ()
	{
		fadeIn.color = Color.clear;
		victory = GameObject.Find ("Victory");
		victory.SetActive (false);
		atackingPlayer = 2;
		atackNameUI.SetActive (false);
		for (int i = 0; i < selectAtackUI.Count (); i++) {
			selectAtackUI [i].SetActive (false);
		}
		camSlowMotionEffect = Camera.main.transform.GetChild (0).gameObject;
		camSlowMotionEffect.SetActive (false);
		for (int i = 0; i < GameObject.FindGameObjectsWithTag ("Player").Length; i++) {
			players.Add (GameObject.FindGameObjectsWithTag ("Player") [i]);
		}
		players = players.OrderBy (go => go.name).ToList ();
		for (int i = 0; i < players.Count; i++) {
			players [i].GetComponent<BattleCharacter> ().playerNumber = i + 1;
		}

		for (int i = 0; i < GameObject.FindGameObjectsWithTag ("Enemy").Length; i++) {
			enemies.Add (GameObject.FindGameObjectsWithTag ("Enemy") [i]);
			enemyHealth.Add (enemyHealthUI [i].health);
		}
		enemies = enemies.OrderBy (go => go.name).ToList ();

		//DoDamage (enemies[0].gameObject,100,Random.Range(0.85f,1.15f));
	}

	public void DoDamage (GameObject sandBag, float damage, float random)
	{
		GameObject uidmg = GameObject.Instantiate (damageUI, sandBag.transform.position, Quaternion.identity);
		uidmg.GetComponent<TextMesh> ().text = "" + Mathf.RoundToInt (damage * random);
		if(sandBag.tag == "Enemy"){
		enemyHealth [0] -= Mathf.RoundToInt (damage * random);
		} else if(sandBag.tag == "Player"){
			for(int i = 0; i < players.Count;i++){
				if(players[i] == sandBag){
					playerHealth[i] -= Mathf.RoundToInt (damage * random);
				}
			}
		}
	}

	public void SpawnRandomCam ()
	{
		int i = Random.Range (0, camPathPrefabs.Count ());
		GameObject.Instantiate (camPathPrefabs [i], Vector3.zero, Quaternion.identity);
	}

	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.R)){
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}
		if(Input.GetKeyDown(KeyCode.F)){
			fadeIn.color = Color.white;
		}
		if(fadeIn.color != Color.clear){
			FadeIn ();
		}

		if (turnAtack == false) {
			if (Vector2.SqrMagnitude (new Vector2 (Mathf.Abs (Input.GetAxis ("DPadLeftRight")), Mathf.Abs (Input.GetAxis ("DPadUpDown")))) != 0) {
				if (Input.GetAxis ("DPadLeftRight") > 0) {
					atackingPlayer = 2;
				} else {
					atackingPlayer = 3;
				}
				if (Input.GetAxis ("DPadUpDown") != 0) {
					if (Input.GetAxis ("DPadUpDown") > 0) {
						atackingPlayer = 1;
					} else {
						atackingPlayer = 4;
					}
				}
			}
			if(state == "turnAtack"){
				state = "normal";
			}
		} else {
			state = "turnAtack";
		}
	

		if (turnAtack == true) {
			TurnAtack ();
			camSlowMotionEffect.SetActive (true);
			selectAtackUI [atackingPlayer - 1].SetActive (true);
		} else {
			camSlowMotionEffect.SetActive (false);
			selectAtackUI [atackingPlayer - 1].SetActive (false);
			//}
			if (Input.GetAxis ("DPadUpDown") != 0) {
				bool canAtack = false;
				if (charge [atackingPlayer - 1] >= 100) {
					canAtack = true;
				}
				if (canAtack == true) {
					turnAtack = true;
				}
			}
			if (Input.GetAxis ("DPadLeftRight") != 0) {
				bool canAtack = false;
				if (charge [atackingPlayer - 1] >= 100) {
					canAtack = true;
				}
				if (canAtack == true) {
					turnAtack = true;
				}
			}
		}

		if(state == "enemyAtack"){
			if(atackNameUI.activeSelf == false){
			fadeIn.color = Color.white;
			}
			atackNameUI.SetActive (true);
		}

		if(state == "Victory"){
			victory.SetActive (true);
			for (int i = 0; i < GameObject.Find("Canvas").transform.childCount;i++){
				if(GameObject.Find ("Canvas").transform.GetChild (i).name != "Victory"){
					Destroy (GameObject.Find ("Canvas").transform.GetChild (i).gameObject);
				}
			}
			state = "fuck";
		}

		if(state == "cooldown"){
			coolDownTimer = Mathf.MoveTowards (coolDownTimer,0,Time.deltaTime);
			if(coolDownTimer == 0){
				state = "normal";
			}
		}

		Time.timeScale = timeScale;
	}

	public void BackToNormal(bool coolDown){
		fadeIn.color = Color.white;
		if (coolDown == false) {
			state = "normal";
		} else {
			state = "cooldown";
			coolDownTimer = 0.5f;
		}
		//charge [atackingPlayer - 1] = 0;
		turnAtack = false;
		atackNameUI.SetActive (false);
		selectAtackUI [atackingPlayer - 1].SetActive (false);
	}

	void TurnAtack ()
	{
		if(Time.timeScale != 0){
			fadeIn.color = Color.white;
		}
		timeScale = 0;
	}

	void FadeIn(){
		fadeIn.color = Color.Lerp (fadeIn.color,Color.clear,Time.unscaledDeltaTime * 3);
		if(fadeIn.color.a < 0.5f){
			//fadeIn.color = Color.clear;
		}
	}
}
