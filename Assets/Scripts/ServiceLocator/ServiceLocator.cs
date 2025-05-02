using System.Collections.Generic;

public class ServiceLocator
{
	public static ServiceLocator Current;
	private Dictionary<string, IService> services = new Dictionary<string, IService>();

	private ServiceLocator()
	{

	}

	public static void Init()
	{
		Current = new ServiceLocator();
	}

	public void Register<T>(T service) where T : IService
	{
		string key = typeof(T).Name;
		if (!services.ContainsKey(key))
		{
			services.Add(key, service);
		}
	}

	public void Unregister<T>(T service) where T: IService
	{
		string key = typeof(T).Name;
		if (services.ContainsKey(key))
		{
			services.Remove(key);
		}
	}

	public T Get<T>()
	{
		string key = typeof(T).Name;

		if (!services.ContainsKey(key)) return default;
		return (T)services[key];
	}
}
