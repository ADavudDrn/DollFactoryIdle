using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private int SceneIndex = 0;
        
        public void LoadScene()
        {
            SceneManager.LoadScene(SceneIndex);
        }
    }
}