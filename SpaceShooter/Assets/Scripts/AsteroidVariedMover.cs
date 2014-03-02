using UnityEngine;
using System.Collections;

public class AsteroidVariedMover : MonoBehaviour {

	public GameController gameController;
	public float speed;

	void Start () {

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
			if (gameControllerObject != null) {
					gameController = gameControllerObject.GetComponent<GameController> ();
			}
			if (gameController == null) {
					Debug.Log ("Cannot find 'GameController' script");
			}

		if (gameController.getScore() < 10){
		rigidbody.velocity = transform.forward*speed;
		}
		else {
			rigidbody.velocity = transform.forward*(speed - gameController.getScore());
		}
	}
}
