namespace DoubleDCore.Finder
{
    public interface IGameObjectFinder
    {
        public TType[] Find<TType>();
    }
}