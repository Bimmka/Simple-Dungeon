using UnityEngine;

namespace Services.Assets
{
  public class AssetProvider : IAssetProvider
  {
    public T Instantiate<T>(string prefabPath) where T : Object
    {
      var prefab = Resources.Load<T>(prefabPath);
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