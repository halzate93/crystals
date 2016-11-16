using UnityEngine;
using UnityEngine.Networking;

public class Generator : NetworkBehaviour
{
	[SerializeField]
	private GameObject[] prefabs;
	[SerializeField]
	private float spawnTime;
	[SerializeField]
	private float maxRadius;
	private float elapsedTime;
	private bool isActive;

	private void Update () {
		elapsedTime += Time.deltaTime;
		if (elapsedTime >= spawnTime)
		{
			Spawn ();
			elapsedTime = 0f;
		}
	}

	private void Spawn ()
	{
		Vector3 position = transform.position + GetRandomPosition ();
		int p = Random.Range (0, prefabs.Length);
		GameObject instance = PoolManager.Instance.Instantiate (prefabs[p], position, Quaternion.identity);
		NetworkServer.Spawn (instance);
	}

	private Vector3 GetRandomPosition ()
	{
		float angle = Random.Range (0f, 360f);
		Vector3 direction = Quaternion.Euler (0f, angle, 0f) * Vector3.forward;
		float radius = Random.Range (0, maxRadius);
		Vector3 position = direction * radius;
		return position;
	}
}
