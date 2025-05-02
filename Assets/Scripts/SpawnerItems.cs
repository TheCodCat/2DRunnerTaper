using Assets.Scripts.enums;
using Assets.Scripts.Interface;
using System;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerItems : MonoBehaviour, IInit
{
	[Header("Время")]
	[SerializeField] private float currentTime;
	private float startTime => GameSettingsService.Instance.GameSettings.Currentpoint.ReduceDelay;
	private GameState gameState => GameSettingsService.Instance.GameSettings.GameState;
	[Header("ObjectPool")]
	[SerializeField] private Item right;
	[SerializeField] private Item left;
	public ObjectPool<Item> rightPool;
	public ObjectPool<Item> leftPool;

	public void Init()
	{
		currentTime = startTime;
		rightPool = new ObjectPool<Item>(
			() => Instantiate(right),
			x => x.gameObject.SetActive(true),
			x => x.gameObject.SetActive(false),
			x => DestroyRight(x),
			true, 5, 10
			);

		leftPool = new ObjectPool<Item>(
			() => Instantiate(left),
			x => x.gameObject.SetActive(true),
			x => x.gameObject.SetActive(false),
			x => DestroyLeft(x),
			true, 5, 10
			);

		Debug.Log("Инициализирован");
	}

	private void Update()
	{
		if (gameState != GameState.Game) return;
		currentTime -= Time.deltaTime;
		if(currentTime <= 0)
		{
			currentTime = startTime;
			SpawnItem();
		}
	}

	private void SpawnItem()
	{
		int index = UnityEngine.Random.Range(0, 2);
		if (index.Equals(0))
		{
			Debug.Log("спавн правого");
			var item = rightPool.Get();
			item.Action += ReturnRightItem;
			SetPositionItem(item);

		}
		else
		{
			Debug.Log("спавн левого");
			var item = leftPool.Get();
			item.Action += ReturnLeftItem;
			SetPositionItem(item);
		}
	}

	private void SetPositionItem(Item item)
	{
		item.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
	}

	private void DestroyRight(Item item)
	{
		item.Action -= ReturnRightItem;
		Destroy(item.gameObject);
	}

	private void DestroyLeft(Item item)
	{
		item.Action -= ReturnLeftItem;
		Destroy(item.gameObject);
	}

	public void ReturnRightItem(Item item)
	{
		item.Action -= ReturnRightItem;
		rightPool.Release(item);
	}

	public void ReturnLeftItem(Item item)
	{
		item.Action -= ReturnLeftItem;
		leftPool.Release(item);
	}
}
