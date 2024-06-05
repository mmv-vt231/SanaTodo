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

        public void ChangeRepository(string storage)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext is not null)
            {
                httpContext.Session.SetString("storage", storage);
                Storage = storage;
            }
        }

        public IRepository Load()
        {
            var storage = _httpContextAccessor?.HttpContext?.Session.GetString("storage");

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
