using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T12V2.DataAccess;
using T12V2.DTO;

namespace T12V2.BusinessLogic
{
    internal class UserManager : BaseManager
    {
        public UserDAL userDAL;
        public UserManager() : base()
        {
            userDAL = new UserDAL();
        }
        public User? Login(string username, string password)
        {
            return userDAL.SelectByUsernameAndPassword(username, password);
        }

        public List<User> SelectAll()
        {
            return userDAL.SelectAll();
        }

        public User SelectByID(int id)
        {            
            return userDAL.SelectById(id);
        }
        public List<User> Find(string key)
        {
            int id;
            bool success = int.TryParse(key, out id);
            if (success)
            {
                List<User> users = new List<User>();
                User? user = userDAL.SelectById(id);
                users.Add(user);
                return users;
            }
            else
            {
                return userDAL.SelectByKey(key);
            }
        }
        public int AddNew(User user)
        {
            return userDAL.Insert(user);
        }
        public int Update(User user, string newPass)
        {
            return userDAL.Update(user, newPass);
        }
        public int Update(User user, string field, string newString)
        {
            return userDAL.Update(user, field, newString);
        }

        public User FindId(int id)
        {
            User? user = userDAL.SelectById(id);
            return user;
        }

        public int Delete(User user)
        {
            return userDAL.Delete(user.Id);
        }
        public void CloseConn()
        {
            userDAL.Close();
        }
    }
}
