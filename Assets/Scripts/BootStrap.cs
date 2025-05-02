using Assets.Scripts.Interface;
using System.Collections;
using UnityEngine;

public class BootStrap : MonoBehaviour
{
	[SerializeField] private GameObject[] services;

	private IEnumerator Start()
	{
		foreach (var item in services)
		{
			if (item.TryGetComponent(out IInit component))
				component.Init();
			yield return null;
		}
	}
}
