using Cysharp.Threading.Tasks;

namespace DoubleDCore.GameResources.Base
{
    public interface IAsyncResource : IReleasable
    {
        public UniTask Load();
    }
}