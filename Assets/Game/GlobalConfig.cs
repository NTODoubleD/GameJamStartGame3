using Game.Infrastructure.Storage;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "NewGlobalConfig", menuName = "GlobalConfig", order = 0)]
    public class GlobalConfig : ScriptableObject
    {
        [field: SerializeField] public ItemStorageInfo TestInfo { get; private set; }
        [field: SerializeField] public string LyricsSceneName { get; private set; } = "Lyrics";
        [field: SerializeField] public string GameSceneName { get; private set; } = "SampleScene";
        [field: SerializeField] public string MainMenuSceneName { get; private set; } = "MainMenu";
    }
}