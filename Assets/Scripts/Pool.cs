using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Pool : MonoBehaviour 
{
	[SerializeField]
	private GameObject prefab;
	[SerializeField]
	private int count;
	private Queue<GameObject> instances;
	
	public GameObject Prefab
	{
		get
		{
			return prefab;
		}
	}

	private void Awake ()
	{
		instances = new Queue<GameObject> ();
	}

	private void Start () 
	{
		for (int i = 0; i < count; i++)
		{
			GameObject instance = CreateInstance ();
			Release (instance);
		}
	}
	
	public GameObject Allocate (Vector3 position, Quaternion rotation)
	{
		GameObject instance = (instances.Count > 0)? instances.Dequeue () : CreateInstance ();
		Poolable poolable = instance.GetComponent<Poolable> ();
		poolable.SetActive (true);
		instance.transform.position = position;
		instance.transform.rotation = rotation;
		return instance;
	}

	public void Release (GameObject instance)
	{
		if (instance.name == prefab.name)
		{
			Poolable poolable = instance.GetComponent<Poolable> ();
			poolable.SetActive (false);	
			instances.Enqueue (instance);
		}
		else
		{
			Debug.LogWarning ("Tried to release non-pooled object");
			Destroy (instance);
		}
	}

	private GameObject CreateInstance ()
	{
		GameObject instance = Instantiate (prefab) as GameObject;
		instance.name = prefab.name;
		instance.transform.SetParent (transform);
		NetworkServer.Spawn (instance);
		return instance;
	}
}
