using UnityEngine;

namespace Game.UI
{
    public class GameCloser : MonoBehaviour
    {
        public void CloseGame()
        {
            Application.Quit();
        }
    }
}