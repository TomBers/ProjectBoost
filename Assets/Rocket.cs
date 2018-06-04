using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
	
	Rigidbody rigidBody;
	AudioSource audioSource;
	
	enum State { Alive, Dying, Transcending }
	State state = State.Alive;
	
	[SerializeField] float rcsThrust = 100f;
	[SerializeField] float mainThrust = 3000f;
	[SerializeField] AudioClip mainEngine;
	[SerializeField] AudioClip levelLoad;
	[SerializeField] AudioClip deathRattle;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();

	}

	// Update is called once per frame
	void Update () {
		if (state == State.Alive) {
			RespondToThrustInput();
			RespondToRotateInput();
		}
	}

	private void RespondToRotateInput() {
		float rotationThisFrame = rcsThrust * Time.deltaTime;

		rigidBody.freezeRotation = true; // Take manual control
		if (Input.GetKey(KeyCode.D)) {
			transform.Rotate(new Vector3(0f, 0f, -rotationThisFrame));
		} else if (Input.GetKey(KeyCode.A)) {
			transform.Rotate(new Vector3(0f, 0f, rotationThisFrame));
		}
		rigidBody.freezeRotation = false;
	}

	private void RespondToThrustInput() {
		if (Input.GetKey(KeyCode.Space)) {
			ApplyThrust();
		} else {
			audioSource.Stop();
		}
	}

	private void ApplyThrust() {
		float thrustThisFrame = mainThrust * Time.deltaTime;
		rigidBody.AddRelativeForce(new Vector3(0f, thrustThisFrame, 0f));

		if (!audioSource.isPlaying) {
			audioSource.PlayOneShot(mainEngine);
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (state != State.Alive) return;

		switch (collision.gameObject.tag) {
			case "Friendly":
				break;
			case "GOAL":
				StartSuccessSequence();
				break;
			case "Fuel":
				print("Fuel");
				break;
			case "Obsticle":
				Death();
				break;
			default:
				print("Dead");
				break;
		}        
    }

	private void StartSuccessSequence() {
		state = State.Transcending;
		audioSource.Stop();
		audioSource.PlayOneShot(levelLoad);
		Invoke("LoadNextScene", 2f);
	}

	private void Death() {
		state = State.Dying;
		audioSource.Stop();
		audioSource.PlayOneShot(deathRattle);
		Invoke("LoadFirstLevel", 1f);
	}

	private void LoadFirstLevel() {
		SceneManager.LoadScene(0);
		state = State.Alive;
	}

	private void LoadNextScene() {
		SceneManager.LoadScene(1);
		state = State.Alive;
	}
}
