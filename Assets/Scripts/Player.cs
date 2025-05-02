using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
	public static Action<int> ChangeCoin;
	[SerializeField] private int coin;
	[SerializeField] private ParticleSystem ParticleSystem;
	[SerializeField] private Transform skin;
	public int Coin
	{
		get { return coin; }
		set
		{
			coin = value;
			ChangeCoin?.Invoke(value);
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out ItemTrigger component))
		{
			Debug.Log(component.Action.ToString());
			if(component.Action == PlayerActionEnum.AddCoin)
			{
				Coin++;
			}
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out ItemTrigger component))
		{
			if (component.Action == PlayerActionEnum.Dead)
			{
				GameSettingsService.Instance.GameSettings.GameState = Assets.Scripts.enums.GameState.Pause;
				StartCoroutine(FallGamePlayer());
				//ServiceLocator.Current.Get<SceneManagerService>().Restart();
			}
		}
	}

	private IEnumerator FallGamePlayer()
	{
		Tween tween = skin.DOScale(0,0.5f);
		yield return null
			;
		ParticleSystem.Play();
	}
}
