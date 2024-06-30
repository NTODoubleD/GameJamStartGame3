using DoubleDTeam.UI;
using UnityEngine;

namespace Game.UI
{
    public class PageActivator : MonoBehaviour
    {
        [SerializeField] private MonoPage _page;

        public void ClosePage()
        {
            _page.Close();
        }
    }
}