// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class PlateSpawner : MonoBehaviour
{
	[SerializeField]
	private Transform    plateLeftSpawnPoint;
	[SerializeField]
	private Transform	 plateRightSpawnPoint;
	[SerializeField]
	private GameObject	 platePrefab;
	[SerializeField]
	private float	     plateMovementSpeed;
	[SerializeField]
	private float		 spawnTime;

	private void Start()
	{
		StartCoroutine(SpawnPlate());
	}

	private IEnumerator SpawnPlate()
	{
		WaitForSeconds spawnTime = new WaitForSeconds(this.spawnTime);

		while(true)
		{
			// ���� ��ġ ����
			bool		isLeftSpawn = Random.Range(0, 2) == 0 ? true : false;
			Vector3		spawnPoint  = isLeftSpawn == true ? plateLeftSpawnPoint.position 
														  : plateRightSpawnPoint.position;

			// ���� ��ȯ
			GameObject  clone       = Instantiate(platePrefab, spawnPoint, Quaternion.identity);
			Vector3		movementDir = isLeftSpawn == true ? Vector3.right : Vector3.left;

			// ���� ����
			clone.GetComponent<MaterialPlate>().Initialize(plateMovementSpeed, movementDir);

			yield return spawnTime;
		}
	}
}