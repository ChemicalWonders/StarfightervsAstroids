using UnityEngine;
using System.Collections;

public class SendGridGetURL : MonoBehaviour {

	private GameController gameController;
 
    public void Start () {

    	GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
				gameController = gameControllerObject.GetComponent<GameController> ();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}

        string url = "https://api.sendgrid.com/api/mail.send.json";
        string api_user = "kchan039";
        string api_key = "123456";
        string to = gameController.getEmail();
        string subject = "High Score for Astroid Shooter!";
        string text = "Look at my score! I got " + gameController.getScore() + "Can you beat me?";
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
}