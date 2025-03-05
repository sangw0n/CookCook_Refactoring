// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	[SerializeField]
	private FoodPlate   foodPlate;

	[Header("# Food ScriptableObject")]
	[SerializeField]
	private FoodSO[]	foodScriptableObjects;

	[Header("# Effect Prefab")]
	[SerializeField]
	private GameObject  requiredMaterialEffect;
	[SerializeField]
	private GameObject  successEffect;
	[SerializeField]
	private GameObject  failEffect;

	private Food[]		foods;

	private int			currentFoodIndex;
	private int			currentMaterialIndex;

	public override void Awake()
	{
		base.Awake();

		SetFoodData();
	}

	/// <summary>
	/// ��ũ���ͺ� ������Ʈ�� �ִ� �����͸� ������ "foods"�� �ʱ�ȭ�մϴ�
	/// </summary>
	private void SetFoodData()
	{
		// �迭 ���� ����
		foods = new Food[foodScriptableObjects.Length];
		
		// �ʱ�ȭ 
		for(int index = 0 ; index < foods.Length; index++)
		{
			foods[index] = new Food();

			foods[index].foodName			= foodScriptableObjects[index].foodName;
			foods[index].cookingTimeLimit   = foodScriptableObjects[index].cookingTimeLimit;
			foods[index].foodSprite			= foodScriptableObjects[index].foodSprite;

			// ��� �ʱ�ȭ
			int materialsArrayLength		= foodScriptableObjects[index].materials.Length;
			foods[index].materials			= new Material[materialsArrayLength];

			for(int index2 = 0; index2 < foods[index].materials.Length; index2++)
			{
				foods[index].materials[index2] = new Material();

				foods[index].materials[index2].materialName      = foodScriptableObjects[index].materials[index2].materialName;
				foods[index].materials[index2].materialSprite    = foodScriptableObjects[index].materials[index2].materialSprite;
				foods[index].materials[index2].materialMaxCount  = foodScriptableObjects[index].materials[index2].materialMaxCount;
				foods[index].materials[index2].materialCount     = foodScriptableObjects[index].materials[index2].materialMaxCount;

				foods[index].materials[index2].positionOffset    = foodScriptableObjects[index].materials[index2].positionOffset;
				foods[index].materials[index2].rotationOffset    = foodScriptableObjects[index].materials[index2].rotationOffset;
				foods[index].materials[index2].scale             = foodScriptableObjects[index].materials[index2].scale;
			}
		}
	}

	/// <summary>
	/// ���� ���� �����͸� ��ȯ�մϴ�
	/// </summary>
	public Food	GetCurrentFood()
	{
		return foods[currentFoodIndex];
	}

	/// <summary>
	/// ���� ���� ��� �� �������� ��Ḧ ��ȯ�մϴ�
	/// </summary>
	public Material GetRandomMaterial()
	{
		int	randomIndex = Random.Range(0, foods[currentFoodIndex].materials.Length);
		return foods[currentFoodIndex].materials[randomIndex];
	}

	/// <summary>
	/// ���� ���Ŀ� �ʿ��� ��� �����͸� ��ȯ�մϴ�
	/// </summary>
	public Material GetCurrentMaterial()
	{
		return foods[currentFoodIndex].materials[currentMaterialIndex];
	}

	/// <summary>
	/// ���� ���Ŀ� �ʿ��� ����� �̸��� ��ȯ�մϴ�
	/// </summary>
	public string GetCurrentMaterialName()
	{
		return foods[currentFoodIndex].materials[currentMaterialIndex].materialName;
	}

	/// <summary>
	/// ���� �丮�� �ʿ��� �� ��� ������ ��ȯ�մϴ�
	/// </summary>
	public int GetCurrentFoodTotalMaterialCount()
	{
		int  result		 = 0;
		Food currentFood = foods[currentFoodIndex];

		for(int i = 0 ; i < currentFood.materials.Length; i++)
		{
			result += currentFood.materials[i].materialCount;
		}

		return result;
	}

	/// <summary>
	/// ���� ���Ŀ� �ʿ��� ��Ḧ ������ ���ҽ�Ų��
	/// </summary>
	public void ReduceMaterialCount()
	{
		Food	 currentFood     = foods[currentFoodIndex];
		Material currentMaterial = currentFood.materials[currentMaterialIndex];

		// ��� ���� 
		currentMaterial.materialCount--;

		// ���� ���� �� ��ᰡ "0"�� ��
		if(currentMaterial.materialCount <= 0)
		{
			// �� �̻� ���� �� ��ᰡ ���ٸ�
			if (currentMaterialIndex == currentFood.materials.Length - 1)
			{
				MoveNextFood();
				return;
			}

			currentMaterialIndex++;
		}
	}

	/// <summary>
	/// ���� �丮�� �̵��Ѵ�
	/// </summary>
	public void MoveNextFood()
	{
		MaterialPlatePoolManager.Instance.ReturnAllPlate();

		if(currentFoodIndex == foods.Length - 1)
		{
			// �������� Ŭ����
			return;
		}

		currentFoodIndex++;
		currentMaterialIndex = 0;

		foodPlate.Initilalize();
	}

	#region ����Ʈ ��ȯ ���� �Լ�
	/// <summary>
	/// ���ϴ� ��ġ�� Effect�� �����մϴ�
	/// </summary>
	public GameObject SpawnEffect(EffectType effectType, Vector3 position)
	{
		GameObject effect;

		switch(effectType)
		{
			case EffectType.SuccessEffect:
				effect = Instantiate(successEffect, position, Quaternion.identity);
				return effect;

			case EffectType.FailEffect:
				effect = Instantiate(failEffect, position, Quaternion.identity);
				return effect;

			case EffectType.RequiredMaterialEffect:
				effect = Instantiate(requiredMaterialEffect, position, Quaternion.identity);
				return effect;

			default:
				Debug.Log("Can't Find Effect");
				return null;
		}
	}

	/// <summary>
	/// ���ϴ� ��ġ�� Effect�� �����ϰ� ���ӽð��� �����մϴ�
	/// </summary>
	public GameObject SpawnEffect(EffectType effectType, Vector3 position, float duration)
	{
		GameObject effect;

		switch (effectType)
		{
			case EffectType.SuccessEffect:
				effect = Instantiate(successEffect, position, Quaternion.identity);
				Destroy(effect, duration);
				return effect;

			case EffectType.FailEffect:
				effect = Instantiate(failEffect, position, Quaternion.identity);
				Destroy(effect, duration);
				return effect;


			case EffectType.RequiredMaterialEffect:
				effect = Instantiate(requiredMaterialEffect, position, Quaternion.identity);
				Destroy(effect, duration);
				return effect;

			default:
				Debug.Log("Can't Find Effect");
				return null;
		}

	}
	#endregion
}
