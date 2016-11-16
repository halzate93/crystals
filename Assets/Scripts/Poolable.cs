using UnityEngine;
using UnityEngine.Networking;

public class Poolable : NetworkBehaviour 
{
	public void SetActive (bool isActive)
	{
		gameObject.SetActive (isActive);
		RpcSetActive (isActive);
	}

	[ClientRpc]
	private void RpcSetActive (bool isActive)
	{
		gameObject.SetActive (isActive);
	}
}
