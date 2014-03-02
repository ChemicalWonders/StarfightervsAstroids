using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

	public int value;

	private GameController gameController;

	void Start()
	{
			GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
			if (gameControllerObject != null) {
					gameController = gameControllerObject.GetComponent<GameController> ();
			}
			if (gameController == null) {
					Debug.Log ("Cannot find 'GameController' script");
			}
		}
	void OnTriggerExit(Collider other){
			//gameController.MinusScore(value);
			Destroy(other.gameObject);
		}
}
