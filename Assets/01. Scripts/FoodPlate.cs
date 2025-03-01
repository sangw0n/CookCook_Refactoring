// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class FoodPlate : MonoBehaviour
{
	[SerializeField, Range(0.0f, 1.0f)]
	private float		   startAlpha;

	private string		   foodName;
	private int			   foodCompleteness;
	private float		   cookingTimeLimit;

	private SpriteRenderer spriteRenderer;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();

		// 초기 투명도값 설정
		spriteRenderer.color = new Color(spriteRenderer.color.r, 
										 spriteRenderer.color.g,
										 spriteRenderer.color.b,
										 startAlpha);

	}

	private void Start()
	{
		Initilalize();
	}

	public void Initilalize()
	{
		Food currentFood = GameManager.Instance.GetCurrentFood();

		// 현재 음식에 맞게 Sprite 설정
		spriteRenderer.sprite = currentFood.foodSprite;

		foodName              = currentFood.foodName;
		foodCompleteness	  = Constants.MaxFoodCompleteness;
		cookingTimeLimit	  = currentFood.cookingTimeLimit;
	}

	public void UpdateAlpha()
	{
		int maxCount	   = GameManager.Instance.GetCurrentMaterial().materialMaxCount;
		int remainingCount = GameManager.Instance.GetCurrentMaterial().materialCount;

		float alpha = Mathf.Lerp(startAlpha, 1f, 1f - (float)remainingCount / maxCount);
		Color color = spriteRenderer.color;
		color.a = alpha;
		spriteRenderer.color = color;
	}
}
