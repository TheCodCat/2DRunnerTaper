using Assets.Scripts.Interface;
using UnityEngine;

public class GameSettingsService : MonoBehaviour, IInit
{
    public static GameSettingsService Instance;
	[SerializeField] private GameSettings gameSettings;
	public GameSettings GameSettings => gameSettings;

	public void Init()
	{
		GameSettings.Init();
		Debug.Log($"{this.name} Инициализирован");
	}

	private void OnDisable()
	{
		Player.ChangeCoin -= GameSettings.ChangeGameSetting;
	}

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
			Destroy(gameObject);
	}
}
