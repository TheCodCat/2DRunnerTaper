using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class Item : MonoBehaviour
{
	public Action<Item> Action;
	private Rigidbody2D rigidbody2D;
	private float speed;
	private void Start()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void OnEnable()
	{
		speed = GameSettingsService.Instance.GameSettings.Currentpoint.SpeedItem;
	}

	private void FixedUpdate()
	{
		if(GameSettingsService.Instance.GameSettings.GameState == Assets.Scripts.enums.GameState.Pause)
			rigidbody2D.velocity = Vector2.zero;
		else
		{
			rigidbody2D.velocity = Vector2.down * speed;

			if(transform.position.y <= -5)
			{
				Action?.Invoke(this);
			}
		}
	}
}
