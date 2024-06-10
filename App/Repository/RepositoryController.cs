namespace App.Repository
{
    public class RepositoryController : IRepositoryController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IXMLRepository _xmlRepository;
        private readonly IRepository _repository;

        public string Storage { get; private set; } = "db";

        public RepositoryController(
            IHttpContextAccessor httpContextAccessor,
            IXMLRepository xmlRepository,
            IRepository repository)
        {
            _httpContextAccessor = httpContextAccessor;
            _xmlRepository = xmlRepository;
            _repository = repository;
        }

        public string ChangeRepository(string storage)
        {
            switch (storage)
            {
                case "xml":
                    Storage = "xml";
                    break;

                default:
                    Storage = "db";
                    break;
            }

            return storage;
        }

        public IRepository Load()
        {
            var headerStorage = _httpContextAccessor.HttpContext?.Request.Headers["storage"];
            var storage = string.IsNullOrEmpty(headerStorage) ? Storage : headerStorage.ToString();

            switch (storage)
            {
                case "xml":
                    Storage = "xml";
                    return _xmlRepository;

                default:
                    Storage = "db";
                    return _repository;
            }
        }
    }
}
