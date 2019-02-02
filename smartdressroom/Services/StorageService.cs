using smartdressroom.Storage;

namespace smartdressroom.Services
{
    public class StorageService : IStorageService
    {
        public StorageService() => AppContext = new ApplicationContext();
        public ApplicationContext AppContext { get; }
    }
}
