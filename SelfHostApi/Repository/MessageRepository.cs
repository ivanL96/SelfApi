using SelfHostApi.Abstract;
using SelfHostApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostApi.Repository
{
    public class MessageRepository : IRepository
    {
        private DataContext db = new DataContext();

        public List<Message> GetList() => db.Messages.ToList();

        public IQueryable<Message> CreateQuery()
        {
            return db.Messages.AsQueryable();
        }

        public IQueryable<Message> FilterQuery(IQueryable<Message> query, Expression<Func<Message, bool>> predicate)
        {
            return query.Where(predicate);
        }

        public ApplicationUser GetUser(string email)
        {
            return db.Users.FirstOrDefault(x=>x.UserName == email);
        }

        public void AddMessage(Message message)
        {
            db.Messages.Add(message);
            db.SaveChanges();
        }


        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
