using Assets.Scripts.enums;
using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Settings", menuName = "Settings")]
public class GameSettings : ScriptableObject
{
	public Action<GameState> ChangeState;
	public GameState GameState
	{
		get
		{
			return gameState;
		}
		set
		{
			gameState = value;
			ChangeState?.Invoke(value);
		}
	}
	[SerializeField] private GameState gameState;

	public int indexGame = 0;
	public PointGame Currentpoint => AddOfCurrentDelay[indexGame];
	public PointGame[] AddOfCurrentDelay;

	public void Init()
	{
		indexGame = 0;
		GameState = GameState.Pause;
		Player.ChangeCoin += ChangeGameSetting;

	}
	public void ChangeGameSetting(int coin)
	{
		var point = AddOfCurrentDelay.FirstOrDefault(x => x.NecessaryCoint == coin);
		if(point != null)
			indexGame = AddOfCurrentDelay.ToList().IndexOf(point);
	}
}
