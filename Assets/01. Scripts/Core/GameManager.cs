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

		// CurrentFood 설정 
		currentFood = foods[currentFoodIndex];
	}

	/// <summary>
	/// 스크립터블 오브젝트에 있는 데이터를 가져와 "foods"에 초기화
	/// </summary>
	private void InitializeFoodData()
	{
		// 배열 공간 생성
		foods = new Food[foodScriptableObjects.Length];
		
		// 초기화 
		for(int index = 0 ; index < foods.Length; index++)
		{
			foods[index] = new Food();

			foods[index].foodName			= foodScriptableObjects[index].foodName;
			foods[index].cookingTimeLimit   = foodScriptableObjects[index].cookingTimeLimit;
			foods[index].foodSprite			= foodScriptableObjects[index].foodSprite;

			// 재료 초기화
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
	/// 현재 음식에 필요한 랜덤 재료를 가지고 옵니다
	/// </summary>
	public Material GetRandomMaterial()
	{
		int	randomIndex = Random.Range(0, foods[currentMaterialIndex].materials.Length);
		return foods[currentMaterialIndex].materials[randomIndex];
	}
}
