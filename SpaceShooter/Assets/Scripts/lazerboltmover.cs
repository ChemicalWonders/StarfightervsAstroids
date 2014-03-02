using UnityEngine;
using System.Collections;

public class lazerboltmover : MonoBehaviour {
	public float speed;
	void Start(){
		rigidbody.velocity = transform.forward* speed;
	}
}
