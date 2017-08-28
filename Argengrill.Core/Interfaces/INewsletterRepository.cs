using System.Collections;

namespace Argengrill.Core.Interfaces
{
    public interface INewsletterRepository
    {
        void Add(Newsletter p);

        void Edit(Newsletter p);

        void Remove(int Id);

        IEnumerable GetProducts(); Newsletter FindById(int Id);
    }
}