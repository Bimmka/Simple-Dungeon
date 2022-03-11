using System.Collections;
using UnityEngine;

namespace Services
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
    void StopAllCoroutines();
    void StopCoroutine(Coroutine coroutine);
  }
}