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
	private GameObject  effectObject;

	private float		movementSpeed;
	private Vector3		movementDirection;

	private void OnDisable()
	{
		if(effectObject != null)
		{
			Destroy(effectObject);
		}
	}

	private void Update()
	{
		if(effectObject != null && materialName != GameManager.Instance.GetCurrentMaterialName())
		{
			Destroy(effectObject);
		}	
		
		if(effectObject == null && materialName == GameManager.Instance.GetCurrentMaterialName())
		{
			effectObject = GameManager.Instance.SpawnEffect(EffectType.RequiredMaterialEffect, transform.position);
			effectObject.transform.SetParent(transform);
		}

		MoveTo();
	}

	public void Initialize(Vector3 spawnPoint, float movementSpeed, Vector3 movementDirection, Material material)
	{
		transform.position     = spawnPoint;
		this.movementSpeed     = movementSpeed;
		this.movementDirection = movementDirection;

		// ��� ���� 
		this.materialName	   = material.materialName;
		this.material.GetComponent<SpriteRenderer>().sprite = material.materialSprite;
		
		// ��� ������ ����
		this.material.transform.position  += material.positionOffset;
		this.material.transform.rotation   = Quaternion.Euler(this.material.transform.rotation.eulerAngles + 
													material.rotationOffset);
		//this.material.transform.localScale = material.scale == Vector3.zero ? Vector3.one : material.scale; 

		// ���� �丮�� �ʿ��� ����� �� ��ƼŬ ����
		if (materialName == GameManager.Instance.GetCurrentMaterialName())
		{
			effectObject = GameManager.Instance.SpawnEffect(EffectType.RequiredMaterialEffect, transform.position);
			effectObject.transform.SetParent(transform);
		}
	}

	private void MoveTo()
	{
		transform.position += movementSpeed * movementDirection * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D trigger)
	{
		if (trigger.CompareTag(Constants.MainPlateTag))
		{
			// �丮�� �ʿ��� ����� ��
			if(materialName == GameManager.Instance.GetCurrentMaterialName())
			{
				GameManager.Instance.ReduceMaterialCount();

				// ����Ʈ ó��
				GameManager.Instance.SpawnEffect(EffectType.SuccessEffect, trigger.transform.position, 1.0f);

				// ���� �丮 ���� ó��
				trigger.GetComponent<FoodPlate>().UpdateAlpha();

				// ���� ��� ���� �߰�
				trigger.GetComponent<FoodPlate>().AddMaterialCount();

			}
			// �丮�� �ʿ��� ��ᰡ �ƴ� ��
			else
			{
				// ����Ʈ ó��
				GameManager.Instance.SpawnEffect(EffectType.FailEffect, trigger.transform.position, 1.0f);
			}

			MaterialPlatePoolManager.Instance.ReturnMaterialPlate(gameObject);
		}
	}
}
