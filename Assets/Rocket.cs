using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
	
	Rigidbody rigidBody;
	AudioSource audioSource;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		ProcessInput();
	}

	private void ProcessInput() {
		DealWithThrust();

		if (Input.GetKey(KeyCode.D)) {
			transform.Rotate(new Vector3(0f, 0f, -1f));
		} else if (Input.GetKey(KeyCode.A)) {
			transform.Rotate(new Vector3(0f, 0f, 1f));
		}
	}

	private void DealWithThrust() {
		if (Input.GetKey(KeyCode.Space)) {
			rigidBody.AddRelativeForce(new Vector3(0f, 30f, 0f));
			if (!audioSource.isPlaying) {
				audioSource.Play();
			}
		} else {
			audioSource.Stop();
		}
	}
}
