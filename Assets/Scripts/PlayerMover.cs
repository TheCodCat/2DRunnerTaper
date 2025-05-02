using UnityEngine;

public class PlayerMover : MonoBehaviour
{
	[SerializeField] private float[] rotation;
	[SerializeField] private int indexPos;
	[SerializeField] private Rigidbody2D rigidbody2D;

	private void Update()
	{
		if (GameSettingsService.Instance.GameSettings.GameState == Assets.Scripts.enums.GameState.Pause) return;

		if (Input.GetMouseButtonDown(0))
		{
			indexPos = (indexPos + 1) % rotation.Length;
			rigidbody2D.SetRotation(rotation[indexPos]);
		}		
	}
}
