using SelfHostApi.Abstract;
using SelfHostApi.Models;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace SelfHostApi.Repository
{
    public class MessageDapperRepository : IRepository
    {
        private SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString);

        public List<Message> GetList()
        {
            if (db.State == ConnectionState.Closed)
                db.Open();

            var list = db.Query<Message>("SELECT AspNetUsers.*, Messages.* FROM AspNetUsers, Messages WHERE Messages.ApplicationUser = AspNetUsers").ToList();

            db.Close();
            return list;
        }

        public ApplicationUser GetUser(string email)
        {
            if (db.State == ConnectionState.Closed)
                db.Open();
            
            var users = db.Query<ApplicationUser>("SELECT * From AspNetUsers");
            var user = users.FirstOrDefault(x => x.UserName == email);

            db.Close();
            return user;
        }

        public void AddMessage(Message message)
        {
            if (db.State == ConnectionState.Closed)
                db.Open();

            db.Query<Message>("INSERT Messages VALUES (@Name, @Description, @Created, @ApplicationUserId)", new { message.Name , message.Description , message.Created , message.ApplicationUserId});

            db.Close();
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
