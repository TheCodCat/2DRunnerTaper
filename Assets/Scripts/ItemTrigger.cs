using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
	public PlayerActionEnum Action
	{
		get => actionEnum;
		set
		{
			actionEnum = value;
		}
	}
	[SerializeField] private PlayerActionEnum actionEnum;
}
