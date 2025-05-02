using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	public static Action<int> ChangeCoin;
	[SerializeField] private int coin;
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
			else if (component.Action == PlayerActionEnum.Dead)
			{
				ServiceLocator.Current.Get<SceneManagerService>().Restart();
			}
		}
	}
}
