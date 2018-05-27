using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
	
	Rigidbody rigidBody;
	AudioSource audioSource;
	
	[SerializeField] float rcsThrust = 100f;
	[SerializeField] float mainThrust = 3000f;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		Thrust();
		Rotate();
	}

	private void Rotate() {
		float rotationThisFrame = rcsThrust * Time.deltaTime;

		rigidBody.freezeRotation = true; // Take manual control
		if (Input.GetKey(KeyCode.D)) {
			transform.Rotate(new Vector3(0f, 0f, -rotationThisFrame));
		} else if (Input.GetKey(KeyCode.A)) {
			transform.Rotate(new Vector3(0f, 0f, rotationThisFrame));
		}
		rigidBody.freezeRotation = false;
	}

	private void Thrust() {
		float thrustThisFrame = mainThrust * Time.deltaTime;
		if (Input.GetKey(KeyCode.Space)) {
			rigidBody.AddRelativeForce(new Vector3(0f, thrustThisFrame, 0f));
			if (!audioSource.isPlaying) {
				//audioSource.Play();
			}
		} else {
			audioSource.Stop();
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		switch (collision.gameObject.tag) {
			case "Friendly":
				print("OK");
				break;
			case "Fuel":
				print("Fuel");
				break;
			case "Obsticle":
				print("Bang");
				break;
			default:
				print("Dead");
				break;
		}        
    }
}
