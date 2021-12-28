using UnityEngine;

namespace Services.Assets
{
  public class AssetProvider : IAssetProvider
  {
    public GameObject Instantiate(string prefabPath)
    {
      var prefab = Resources.Load<GameObject>(prefabPath);
      return Object.Instantiate(prefab);
    }

    public T Instantiate<T>(T prefab) where T : Object => 
      Object.Instantiate(prefab);

    public T Instantiate<T>(T prefab, Vector3 at) where T : Object => 
      Object.Instantiate(prefab, at, Quaternion.identity);

    public T Instantiate<T>(T prefab, Transform parent) where T : Object => 
      Object.Instantiate(prefab, parent);

    public T Instantiate<T>(T prefab, Vector3 at, Quaternion rotation, Transform parent) where T : Object => 
      Object.Instantiate(prefab, at, rotation, parent);
  }
}