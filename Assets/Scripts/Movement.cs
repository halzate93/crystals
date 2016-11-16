using UnityEngine;
using UnityEngine.Networking;

[RequireComponent (typeof (NavMeshAgent))]
public class Movement : NetworkBehaviour 
{
	private NavMeshAgent agent;

	private void Awake ()
	{
		agent = GetComponent<NavMeshAgent> ();
	}
	
	private void Update () {
		if (isLocalPlayer && Input.GetMouseButtonDown (0))
			GoToPosition ();
	}

	public override void OnStartLocalPlayer ()
	{
		FollowTarget.Instance.Target = transform;
	}

	private void GoToPosition ()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit))
			agent.destination = hit.point;
	}
}
