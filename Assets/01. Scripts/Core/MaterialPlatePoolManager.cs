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

		// Queue �ȿ� ������Ʈ�� ���������� Queue ���� ��ȯ
		if (platePool.Count > 0)
		{
			result = platePool.Dequeue();
		}
		// Queue �ȿ� ���� ������Ʈ�� ������ ���� �����ؼ� ��ȯ
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
