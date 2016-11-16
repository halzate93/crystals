using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class Collector : NetworkBehaviour 
{
	[SerializeField]
	private string collectableTag;
	private Dictionary<string, int> items;

	private void Awake ()
	{
		items = new Dictionary<string, int> ();
	}

	private void OnTriggerEnter (Collider trigger)
	{
		if (isServer && trigger.tag == collectableTag)
		{
			RpcAdd (trigger.name);
			NetworkServer.Destroy (trigger.gameObject);
		}
	}

	[ClientRpc]
	private void RpcAdd (string item)
	{
		if (!isLocalPlayer) return;
		if (items.ContainsKey (item))
			items[item]++;
		else
			items[item] = 1;
		Inventory.Instance.UpdateUI (items);
	}
}
