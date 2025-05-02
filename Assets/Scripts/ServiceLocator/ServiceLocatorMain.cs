using Assets.Scripts.Interface;
using UnityEngine;

public class ServiceLocatorMain : MonoBehaviour, IInit
{
	[SerializeField] private UIInputService uiInputService;
	[SerializeField] private SceneManagerService sceneManagerService;
	public void Init()
	{
		ServiceLocator.Init();

		ServiceLocator.Current.Register<UIInputService>(uiInputService);
		ServiceLocator.Current.Register<SceneManagerService>(sceneManagerService);
		Debug.Log("Инициализированны");
	}
}
