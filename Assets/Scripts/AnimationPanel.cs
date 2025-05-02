using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class AnimationPanel : MonoBehaviour
{
	[SerializeField] private AudioClip clipOpen;
	[SerializeField] private AudioClip clipClose;
	[SerializeField] private Image rightPanel;
	[SerializeField] private Image leftPanel;
	[SerializeField] private Ease ease;

	private void Start()
	{
		StartCoroutine(StartLoadPanel());
	}

	private IEnumerator StartLoadPanel()
	{
		var anim = DOTween.Sequence();
		anim.Append(rightPanel.DOFillAmount(0, 1.5f).SetEase(ease))
			.Join(leftPanel.DOFillAmount(0, 1.5f).SetEase(ease));
		yield return anim.WaitForCompletion();
	}

	public YieldInstruction ClocePanel()
	{
		var anim = DOTween.Sequence();

		 return anim.Append(rightPanel.DOFillAmount(1, 2f).SetEase(ease))
			.Join(leftPanel.DOFillAmount(1, 2f).SetEase(ease))
			.WaitForCompletion();
	}
}
