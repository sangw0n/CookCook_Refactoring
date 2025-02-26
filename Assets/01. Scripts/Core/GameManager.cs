// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	[Header("# Food ScriptableObject")]
	[SerializeField]
	private FoodSO[] foodScriptableObjects;

	private Food[]	 foods;
	private Food	 currentFood;

	private int		 currentFoodIndex;
	private int      currentMaterialIndex;

	private void Start()
	{
		InitializeFoodData();

		// CurrentFood ���� 
		currentFood = foods[currentFoodIndex];
	}

	/// <summary>
	/// ��ũ���ͺ� ������Ʈ�� �ִ� �����͸� ������ "foods"�� �ʱ�ȭ
	/// </summary>
	private void InitializeFoodData()
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

				foods[index].materials[index2].materialName   = foodScriptableObjects[index].materials[index2].materialName;
				foods[index].materials[index2].materialSprite = foodScriptableObjects[index].materials[index2].materialSprite;
				foods[index].materials[index2].materialCount  = foodScriptableObjects[index].materials[index2].materialCount;

				foods[index].materials[index2].positionOffset = foodScriptableObjects[index].materials[index2].positionOffset;
				foods[index].materials[index2].rotationOffset = foodScriptableObjects[index].materials[index2].rotationOffset;
				foods[index].materials[index2].scale          = foodScriptableObjects[index].materials[index2].scale;
			}
		}
	}

	/// <summary>
	/// ���� ���Ŀ� �ʿ��� ���� ��Ḧ ������ �ɴϴ�
	/// </summary>
	public Material GetRandomMaterial()
	{
		int	randomIndex = Random.Range(0, foods[currentMaterialIndex].materials.Length);
		return foods[currentMaterialIndex].materials[randomIndex];
	}
}
