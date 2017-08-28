using Argengrill.Core;
using Argengrill.Core.Interfaces;
using System.Collections;
using System.Linq;

namespace Argengrill.Infrastructure
{
    public class NewsletterRepository : INewsletterRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public void Add(Newsletter p)
        {
            context.Newsletters.Add(p);
            context.SaveChanges();
        }

        public void Edit(Newsletter p)
        {
            context.Entry(p).State = System.Data.Entity.EntityState.Modified;
        }

        public Newsletter FindById(int Id)
        {
            var result = (from r in context.Newsletters where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetProducts()
        {
            return context.Newsletters;
        }

        public void Remove(int Id)
        {
            Newsletter p = context.Newsletters.Find(Id); context.Newsletters.Remove(p); context.SaveChanges();
        }
    }
}