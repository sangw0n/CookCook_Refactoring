// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class MaterialPlate : MonoBehaviour
{
	private float		movementSpeed;
	private Vector3		movementDirection;

	private void Update()
	{
		MoveTo();
	}

	public void Initialize(float movementSpeed, Vector3 movementDirection)
	{
		this.movementSpeed     = movementSpeed;
		this.movementDirection = movementDirection;
	}

	private void MoveTo()
	{
		transform.position += movementSpeed * movementDirection * Time.deltaTime;
	}
}
