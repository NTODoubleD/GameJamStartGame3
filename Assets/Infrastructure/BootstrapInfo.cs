namespace Infrastructure
{
    public class BootstrapInfo
    {
        public readonly string NextSceneName;

        public BootstrapInfo(string nextSceneName)
        {
            NextSceneName = nextSceneName;
        }
    }
}