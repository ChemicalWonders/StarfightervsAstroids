using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject hazard;
	public GameObject hazard2;
	public GameObject hazard3;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText gameOverText;
	public GUIText highScore;
	public GUIText waveText;

	//FOR EMAIL
	private string email;
	
	private int score;
	private int waveNumber;
	private bool restart;
	private bool gameOver;

	void Start()
	{	
        //FOR EMAIL
		email = "example@example.example";
		//MonoBehaviour.enabled = true;
		score = 0;
		waveNumber = 1;
		gameOver = false;
		restart = false;
		gameOverText.text = "";
		if(PlayerPrefs.GetInt("highscore")>0)
		{
			highScore.text = "High Score " + PlayerPrefs.GetInt("highscore");
		}
		else
		{
			PlayerPrefs.SetInt ("highscore",0);
			highScore.text = "High Score 0";
		}
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
        }
         else {
            Debug.Log("WWW Error: "+ www.error);
        }    
    }

	void Update()
	{
	}

	public string getEmail() {
		return email;
	}

	void OnGUI () {
		// Make a background box
		if(restart){
			GUI.Box(new Rect(300,100,150,30), "Astroid Destroyer Menu");
			
			// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
			if(GUI.Button(new Rect(310,130,80,20), "Play Game")) {
				Application.LoadLevel (Application.loadedLevel);
			}
			
			// Make the second button.
			if(GUI.Button(new Rect(310,160,80,20), "Quit Game")) {
				Application.Quit();			
			}
			
			//FOR EMAIL
			if(GUI.Button(new Rect(310,190,200,20), "Send your high score to: ")) {
				print ("Your email is " + email);

				string url = "https://api.sendgrid.com/api/mail.send.json";
		        string api_user = "kchan039";
		        string api_key = "123456";
		        string to = this.getEmail();
		        string subject = "High Score for Astroid Shooter!";
		        string text = "Look at my score! I got " + this.getScore() + "! Can you beat me?";
		        string from = "kchan039@gmail.com";

		        WWWForm form = new WWWForm();
		        form.AddField("api_user", api_user);
		        form.AddField("api_key", api_key);
		        form.AddField("to", to);
		        form.AddField("toName", "");
		        form.AddField("subject", subject);
		        form.AddField("text", text);
		        form.AddField("from", from);
		        WWW www = new WWW(url, form);

				StartCoroutine(WaitForRequest(www));
			}
			email = GUI.TextField(new Rect(310, 220, 200, 20), email, 25);
			
		}
	}
	IEnumerator SpawnWaves(){
		waveText.text = "Wave #" + waveNumber;
		yield return new WaitForSeconds (startWait);
		waveText.text = "";
		while(!gameOver)
		{
			for (int i = 0; i < hazardCount; ++i) 
			{
				int alpha = Random.Range(1, 4);

					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;

					if (alpha == 1){
					Instantiate (hazard, spawnPosition, spawnRotation);
					
				}

				/*else if (alpha == 3  && this.getScore() > 15){
					Instantiate (hazard3, spawnPosition, spawnRotation);
				}*/

				else {
					Instantiate (hazard2, spawnPosition, spawnRotation);
				}

				yield return new WaitForSeconds(spawnWait);
			}

			if(gameOver){
				restart = true;
				break;
			}
			++waveNumber;
			waveText.text = "Wave #" + waveNumber;
			
			yield return new WaitForSeconds(waveWait);
			waveText.text = "";

			
		}
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;

		if(score > PlayerPrefs.GetInt("highscore"))
		{

			PlayerPrefs.SetInt("highscore",score);
			highScore.text = "High Score" + PlayerPrefs.GetInt("highscore");
		}

		UpdateScore ();
	}

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
