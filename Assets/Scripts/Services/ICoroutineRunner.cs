using System.Collections;
using UnityEngine;

namespace Bootstrapp
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
    void StopAllCoroutines();
  }
}