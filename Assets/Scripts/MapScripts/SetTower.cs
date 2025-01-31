using System;
using UnityEngine;

[Serializable]
public class SetTower
{
	public string name;
	public int cost;
	public GameObject prefab;

	public SetTower(string _name, int _cost, GameObject _prefab)
	{
		name = _name;
		cost = _cost;
		prefab = _prefab;
	}
}
