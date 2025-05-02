using TMPro;
using UnityEngine;

public class UIInputService : MonoBehaviour, IService
{
	[SerializeField] private TMP_Text coinViewText;

	private void OnEnable()
	{
		Player.ChangeCoin += ChnageCountCoin;
	}
	private void OnDisable()
	{
		Player.ChangeCoin -= ChnageCountCoin;
	}

	public void StartGameButton()
	{
		GameSettingsService.Instance.GameSettings.GameState = Assets.Scripts.enums.GameState.Game;
	}

	private void ChnageCountCoin(int value)
	{
		coinViewText.text = value.ToString();
	}
}
