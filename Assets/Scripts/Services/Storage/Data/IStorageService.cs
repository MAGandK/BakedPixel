using Zenject;

namespace Services.Storage
{
    public interface IStorageService : IInitializable
    {
        T GetData<T>(string key) where T : class, IStorageData;
    }
}