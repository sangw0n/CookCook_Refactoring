// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class MaterialPlate : MonoBehaviour
{
	[SerializeField]
	private GameObject	material;
	private string		materialName;		

	private float		movementSpeed;
	private Vector3		movementDirection;

	private void Update()
	{
		MoveTo();
	}

	public void Initialize(Vector3 spawnPoint, float movementSpeed, Vector3 movementDirection, Material material)
	{
		transform.position     = spawnPoint;
		this.movementSpeed     = movementSpeed;
		this.movementDirection = movementDirection;

		// 재료 설정 
		this.materialName	   = material.materialName;
		this.material.GetComponent<SpriteRenderer>().sprite = material.materialSprite;
		
		// 재료 오프셋 설정
		this.material.transform.position  += material.positionOffset;
		this.material.transform.rotation   = Quaternion.Euler(this.material.transform.rotation.eulerAngles + 
													material.rotationOffset);
		//this.material.transform.localScale = material.scale == Vector3.zero ? Vector3.one : material.scale; 
	}

	private void MoveTo()
	{
		transform.position += movementSpeed * movementDirection * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D trigger)
	{
		if (trigger.CompareTag(Constants.MainPlateTag))
		{
			MaterialPlatePoolManager.Instance.ReturnMaterialPlate(gameObject);
		}
	}
}
