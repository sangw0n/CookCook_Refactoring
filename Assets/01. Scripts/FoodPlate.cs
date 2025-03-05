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

	private int			   maxMaterialCount;
	private int			   collectedMaterialCount;

	private SpriteRenderer spriteRenderer;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();

		// �ʱ� ������ ����
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

		// ���� ���Ŀ� �°� Sprite ����
		spriteRenderer.sprite  = currentFood.foodSprite;
		foodName               = currentFood.foodName;
		foodCompleteness	   = Constants.MaxFoodCompleteness;
		cookingTimeLimit	   = currentFood.cookingTimeLimit;

		maxMaterialCount	   = GameManager.Instance.GetCurrentFoodTotalMaterialCount();
		collectedMaterialCount = 0;

		UpdateAlpha();
	}

	public void AddMaterialCount()
	{
		collectedMaterialCount++;
	}

	public void UpdateAlpha()
	{
		float t     = (float)collectedMaterialCount / maxMaterialCount;
		float alpha = Mathf.Lerp(startAlpha, 1f, t);

		Color color          = spriteRenderer.color;
		color.a              = alpha;
		spriteRenderer.color = color;
	}
}
