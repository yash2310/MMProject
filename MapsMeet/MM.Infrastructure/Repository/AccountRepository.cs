using MM.ApplicationCore.Entities;
using MM.Infrastructure.DatabaseHelper;
using NHibernate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MM.Infrastructure.Repository
{
    public class AccountRepository<T>
    {
        private static ISession _session;

        public static string AddUser(Users item)
        {
            string status = "";
            try
            {
                //Type type = typeof(T);

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
                        //User user= item as User;

                        _session.Save(item);

                        transaction.Commit();
                        status = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message.ToString();
                status = message;
            }

            return status;
        }
    }
}
