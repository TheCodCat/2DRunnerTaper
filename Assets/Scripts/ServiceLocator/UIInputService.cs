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
	[SerializeField] private AnimationPanel animationPanel;
	[SerializeField] private AudioSource audioSource;

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

	public void RestartCurrentScene(RectTransform button)
	{
		StartCoroutine(RestartScene(button));
	}

	private IEnumerator RestartScene(RectTransform transform)
	{
		var tween = transform.DORotate(new Vector3(0,0, -360), 0.5f,RotateMode.FastBeyond360)
			.SetEase(Ease.InOutBack);
		yield return tween.WaitForCompletion();

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
		StartCoroutine(TextScaler(value));
	}

	private IEnumerator TextScaler(int value)
	{
		coinViewText.text = value.ToString();

		var anim = DOTween.Sequence();
		anim.Append(coinViewText.rectTransform.DOScale(1.2f, 0.1f).SetEase(Ease.InOutBack))
			.Append(coinViewText.rectTransform.DOScale(1f, 0.1f).SetEase(Ease.InOutBack));

		yield return anim.WaitForCompletion();

	}

	public void PlayAudioSoundButton(AudioClip audioClip)
	{
		audioSource.clip = audioClip;
		audioSource.Play();
	}
}
