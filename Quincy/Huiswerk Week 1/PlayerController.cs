﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private AudioSource audioSource;

	public float speed { get; set; }
	public float tilt { get; set; }
	public Boundary boundary { get; set; }

	public GameObject shot { get; set; }
	public Transform shotSpawn { get; set; }
	public float fireRate { get; set; }

	private float nextFire;


	void Start ()	{
		rb = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
	}


	void Update ()	{
		if (Input.GetButton("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;

			// Old instantiate code
			// Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

			// New instantiate code (Object Pooler)
			GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("Player Bullet"); 
			if (bullet != null) {
				bullet.transform.position = shotSpawn.transform.position;
				bullet.transform.rotation = shotSpawn.transform.rotation;
				bullet.SetActive(true);
			}

			audioSource.Play ();
		}
	}


	void FixedUpdate ()	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}