using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : MonoBehaviour 
{
	[SerializeField]
	private Text display;
	[SerializeField]
	private GameObject root;

	public static Inventory Instance
	{
		get; private set;
	}

	private void Awake ()
	{
		Instance = this;
	}

	private void Start ()
	{
		display.text = "";
		root.SetActive (false);
	}

	private void Update ()
	{
		if (Input.GetKeyDown (KeyCode.I))
			root.SetActive (!root.activeSelf);
	}

	public void UpdateUI (Dictionary<string, int> items)
	{
		string text = "";
		foreach (string item in items.Keys)
			text += string.Format ("{0}: {1}\n", item, items[item]);
		display.text = text;
	}
}
