using SelfHostApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostApi.Abstract
{
    public interface IRepository : IDisposable
    {
        List<Message> GetList();

        IQueryable<Message> CreateQuery();

        IQueryable<Message> FilterQuery(IQueryable<Message> query, Expression<Func<Message, bool>> predicate);

        ApplicationUser GetUser(string email);

        void AddMessage(Message message);
    }
}
