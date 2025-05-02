using Assets.Scripts.enums;
using DG.Tweening;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIInputService : MonoBehaviour, IService
{
	[SerializeField] private TMP_Text coinViewText;
	[SerializeField] private Transform restartButton;
	[SerializeField] protected AnimationPanel animationPanel;

	private GameState gameState = GameState.Pause;

	private void OnEnable()
	{
		Player.ChangeCoin += ChnageCountCoin;
		GameSettingsService.Instance.GameSettings.ChangeState += RestartGameButton;
	}

	private void OnDisable()
	{
		Player.ChangeCoin -= ChnageCountCoin;
		GameSettingsService.Instance.GameSettings.ChangeState -= RestartGameButton;
	}

	public void StartGameButton(Transform transform)
	{
		StartCoroutine(StartChangeGame(transform));
	}

	public void RestartCurrentScene()
	{
		StartCoroutine(RestartScene());
	}

	private IEnumerator RestartScene()
	{
		yield return animationPanel.ClocePanel(); 

		ServiceLocator.Current.Get<SceneManagerService>().Restart();
	}

	private IEnumerator StartChangeGame(Transform transform)
	{
		Tween tween = transform.DOScale(0, 0.5f).SetEase(ease: Ease.InOutBack);
		yield return tween.WaitForCompletion();

		GameSettingsService.Instance.GameSettings.GameState = Assets.Scripts.enums.GameState.Game;
	}

	private void RestartGameButton(GameState gameState)
	{
		this.gameState = gameState == GameState.Game ? GameState.Game : this.gameState;

		if (this.gameState == GameState.Game && gameState == GameState.Pause)
			StartCoroutine(RestartState());
	}

	private IEnumerator RestartState()
	{
		restartButton.gameObject.SetActive(true);
		Tween tween = restartButton.DOScale(1f, 0.2f).SetEase( ease: Ease.InOutBack );
		yield return tween.WaitForCompletion();
	}

	private void ChnageCountCoin(int value)
	{
		coinViewText.text = value.ToString();
	}
}
