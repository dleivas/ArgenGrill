using Argengrill.Core;
using Argengrill.Core.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Argengrill.Infrastructure
{
    public class NewsletterRepository : INewsletterRepository
    {
        public async Task Add(Newsletter p)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Newsletters.Add(p);
                await db.SaveChangesAsync();
            }
        }

        public async Task Edit(Newsletter p)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Entry(p).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        public async Task<Newsletter> FindById(int Id)
        {
            using (var db = new ApplicationDbContext())
            {
                return await db.Newsletters.FirstOrDefaultAsync(x => x.Id == Id);
            }
        }

        public async Task<Newsletter> FindByEmail(string MyEmail)
        {
            using (var db = new ApplicationDbContext())
            {
                return await db.Newsletters.FirstOrDefaultAsync(x => x.Email == MyEmail);
            }
        }

        public async Task<bool> EmailExists(string MyEmail)
        {
            using (var db = new ApplicationDbContext())
            {
                var Exists = await db.Newsletters.FirstOrDefaultAsync(x => x.Email == MyEmail);
                if (Exists == null)
                { return false; }
                else { return true; }
            }
        }

        public async Task<List<Newsletter>> GetProducts()
        {
            using (var db = new ApplicationDbContext())
            {
                return await db.Newsletters.ToListAsync();
            }
        }

        public async Task Remove(int MyId)
        {
            using (var db = new ApplicationDbContext())
            {
                var News = new Newsletter { Id = MyId };

                db.Newsletters.Attach(News);
                db.Entry(News).State = EntityState.Deleted;
                await db.SaveChangesAsync();
            }
        }
    }
}