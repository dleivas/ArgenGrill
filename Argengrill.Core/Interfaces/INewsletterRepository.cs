using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Argengrill.Core.Interfaces
{
    public interface INewsletterRepository
    {
        Task Add(Newsletter p);

        Task Edit(Newsletter p);

        Task Remove(int Id);

        Task<List<Newsletter>> GetProducts();

        Task<Newsletter> FindById(int id);

        Task<Newsletter> FindByEmail(string email);
    }
}