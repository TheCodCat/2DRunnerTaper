using System.Collections;
using UnityEngine;
using DG.Tweening;

public class AnimationPanel : MonoBehaviour
{
	[SerializeField] private RectTransform rightPanel;
	[SerializeField] private RectTransform leftPanel;

	private void Start()
	{
		StartCoroutine(StartLoadPanel());
	}

	private IEnumerator StartLoadPanel()
	{
		var anim = DOTween.Sequence();

		anim.Append(rightPanel.DOAnchorPosX(540, 2f).SetEase(Ease.InBounce))
			.Join(leftPanel.DOAnchorPosX(-540, 2f).SetEase(Ease.InBounce));
		yield return anim.WaitForCompletion();
	}

	public YieldInstruction ClocePanel()
	{
		var anim = DOTween.Sequence();

		 return anim.Append(rightPanel.DOAnchorPosX(0, 2f).SetEase(Ease.InBounce))
			.Join(leftPanel.DOAnchorPosX(0, 2f).SetEase(Ease.InBounce))
			.WaitForCompletion();
	}
}
