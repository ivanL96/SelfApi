using SelfHostApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostApi.Abstract
{
    public interface IRepository : IDisposable
    {
        List<Message> GetList();

        ApplicationUser GetUser(string email);

        void AddMessage(Message message);
    }
}
