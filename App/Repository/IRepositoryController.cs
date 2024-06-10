namespace App.Repository
{
    public interface IRepositoryController
    {
        string Storage { get; }

        public IRepository Load();
        string ChangeRepository(string storage);
    }
}
