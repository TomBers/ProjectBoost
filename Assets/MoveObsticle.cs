using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObsticle : MonoBehaviour {

	Transform transform;
	Rigidbody rigidBody;
	[SerializeField] bool rising = true;
	
	[SerializeField] float speed = 200f;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y > 15f) {
			rising = false;
		} else if (transform.position.y < 2f) {
			rising = true;
		}
	
		if (rising) {
			float speedThisFrame = speed * Time.deltaTime;
			rigidBody.AddRelativeForce(new Vector3(0f, speedThisFrame, 0f));
		}
		
	}
}
