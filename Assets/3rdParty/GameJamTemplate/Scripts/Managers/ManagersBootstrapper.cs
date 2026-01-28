using UnityEngine;
using UnityEngine.SceneManagement;

namespace TG.GameJamTemplate
{
    public static class ManagersBootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Execute()
        {
            const string managersSceneName = "Managers Scene";

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name == managersSceneName) return;
            }

            // TODO: test this in a build
            SceneManager.LoadScene(managersSceneName, LoadSceneMode.Additive);
        }
    }
}