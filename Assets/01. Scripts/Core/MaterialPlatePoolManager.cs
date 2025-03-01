// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class MaterialPlatePoolManager : Singleton<MaterialPlatePoolManager>
{
	[SerializeField]
	private GameObject			platePrefab;
	[SerializeField]
	private Transform			plateParent;
	[SerializeField]
	private int					generationCount;

	private Queue<GameObject>	platePool;

	public override void Awake()
	{
		base.Awake();

		platePool = new Queue<GameObject>();
		for (int count = 0 ; count < generationCount; count++)
		{
			platePool.Enqueue(CreateMaterialPlate());
		}
	}

	private GameObject CreateMaterialPlate()
	{
		GameObject clone = Instantiate(platePrefab, plateParent);
		clone.SetActive(false);
		return clone;
	}

	public GameObject GetMaterialPlate()
	{
		GameObject result;

		// Queue 안에 오브젝트가 남아있으면 Queue 에서 반환
		if (platePool.Count > 0)
		{
			result = platePool.Dequeue();
		}
		// Queue 안에 남은 오브젝트가 없으면 새로 생성해서 반환
		else
		{
			 result = CreateMaterialPlate();
		}

		result.SetActive(true);
		result.transform.SetParent(null);
		return result;
	}

	public void ReturnMaterialPlate(GameObject obj)
	{
		obj.SetActive(false);
		obj.transform.SetParent(plateParent);
		platePool.Enqueue(obj);
	}
}
