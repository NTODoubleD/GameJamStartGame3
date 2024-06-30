using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI
{
    public class SceneChanger : MonoBehaviour
    {
        [SerializeField] private int _sceneIndex;

        public void LoadScene()
        {
            SceneManager.LoadScene(_sceneIndex, LoadSceneMode.Single);
        }
    }
}