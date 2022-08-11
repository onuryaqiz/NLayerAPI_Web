namespace NLayer.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {

        Task CommitAsync();//DBContext'in SaveChangeAsync metodunu çağırmış oluyoruz.

        void Commit(); //DBContext'in SaveChange metodunu metodunu çağırmış oluyoruz.
    }
}
