using System.Collections;
using UnityEngine;

namespace Code.Infrastructure.SceneManagement
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}