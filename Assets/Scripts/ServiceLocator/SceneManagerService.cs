using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerService : MonoBehaviour, IService
{
	public void SceneChange(string name)
	{
		StartCoroutine(Loader(name));
	}

	public void Restart()
	{
		StartCoroutine(Loader(SceneManager.GetActiveScene().buildIndex));
	}

	private IEnumerator Loader(string name)
	{
		SceneManager.LoadSceneAsync(name);
		yield return null;
	}

	private IEnumerator Loader(int index)
	{
		SceneManager.LoadSceneAsync(index);
		yield return null;
	}
}
