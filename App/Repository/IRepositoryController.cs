namespace App.Repository
{
    public interface IRepositoryController
    {
        string Storage { get; }

        IRepository Load();
        void ChangeRepository(string storage);
    }
}
