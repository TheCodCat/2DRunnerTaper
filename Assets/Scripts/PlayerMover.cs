using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
	[SerializeField] private float[] rotation;
	[SerializeField] private int indexPos;
	[SerializeField] private Rigidbody2D rigidbody2D;
	[SerializeField] private AudioSource audioS;
	[SerializeField] private Transform transformPlayer;

	private void Update()
	{
		if (GameSettingsService.Instance.GameSettings.GameState == Assets.Scripts.enums.GameState.Pause) return;

		if (Input.GetMouseButtonDown(0))
		{
			StartCoroutine(ChangerAnim());

			//AudioSource.PlayClipAtPoint(audioClip, transform.position, 1f);
			//indexPos = (indexPos + 1) % rotation.Length;
			//rigidbody2D.SetRotation(rotation[indexPos]);
		}		
	}

	private IEnumerator ChangerAnim()
	{
		var tween = transformPlayer.DOScale(0, 0.1f);
		yield return tween.WaitForCompletion();

		audioS.Play();
		indexPos = (indexPos + 1) % rotation.Length;
		rigidbody2D.SetRotation(rotation[indexPos]);

		transformPlayer.DOScale(1, 0.1f);
	}
}
