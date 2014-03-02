using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject hazard;
	public GameObject hazard2;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText nextWaveText;

	private int score;
	private bool restart;
	private bool gameOver;
	private int waveNumber;

	void Start()
	{
		score = 0;
		waveNumber = 1;
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		nextWaveText.text = "";
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	void Update()
	{
		if (restart) {
		
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}


	IEnumerator SpawnWaves(){
		nextWaveText.text = "Wave #1";
		yield return new WaitForSeconds (startWait);
		nextWaveText.text = "";
		while(true)
		{
			for (int i = 0; i < hazardCount; ++i) 
			{
				int alpha = Random.Range(1, 3);

					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;

				if (alpha == 1){
					Instantiate (hazard, spawnPosition, spawnRotation);		
				}

				else {
					Instantiate (hazard2, spawnPosition, spawnRotation);
				}



				yield return new WaitForSeconds(spawnWait);
			}
			if(gameOver){
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
			++waveNumber;
			nextWaveText.text = "Wave #" + waveNumber;
			if(this.getScore() > 15){
				hazardCount *=2;
				spawnWait /=2;
			}
			
			yield return new WaitForSeconds(waveWait);
			nextWaveText.text = "";
			
		}
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	/*public void MinusScore(int ScoreValue)
	{
		if(this.getScore() > 0)
		{
			score -= ScoreValue;
			UpdateScore();
		}
		else{
			return;
		}
	}*/

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	public void GameOver()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}

	public int getScore(){
		return score;
	}
}
