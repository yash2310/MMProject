using MM.ApplicationCore.Entities;
using MM.Infrastructure.DatabaseHelper;
using NHibernate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MM.Infrastructure.Repository
{
    public class AccountRepository<T>
    {
        private static ISession _session;

        public static Users AddUser(Users item)
        {
            Users users = new Users();
            try
            {
                using (_session = MMDatabaseHelper.Create().Session)
                {
                    ValidationContext context = new ValidationContext(item, null, null);
                    List<ValidationResult> results = new List<ValidationResult>();
                    bool valid = Validator.TryValidateObject(item, context, results, true);

                    if (!valid)
                    {
                        throw new Exception(results.Count > 0 ? results[0].ToString() : "");
                    }

                    using (var transaction = _session.BeginTransaction())
                    {
                        _session.Save(item);

                        transaction.Commit();
                        users = item;
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message.ToString();
                users = null;
            }

            return users;
        }

        public static Users CheckUser(string token)
        {
            Users users = null;
            try
            {
                using (_session = MMDatabaseHelper.Create().Session)
                {
                    users = _session.CreateCriteria<Users>().List<Users>().FirstOrDefault(u => u.Token.Equals(token));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                users = null;
            }

            return users;
        }
    }
}
