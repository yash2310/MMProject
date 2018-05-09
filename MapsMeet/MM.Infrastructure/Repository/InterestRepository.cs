using MM.ApplicationCore.Entities;
using MM.Infrastructure.DatabaseHelper;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MM.Infrastructure.Repository
{
    public class InterestRepository
    {
        private static ISession _session;
        private static ITransaction _transaction;

        public static List<Interest> GetInterests()
        {
            List<Interest> interests = new List<Interest>();
            try
            {
                using (_session = MMDatabaseHelper.Create().Session)
                {
                    interests = _session.CreateCriteria<Interest>().List<Interest>().ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                interests = null;
            }
            return interests;
        }

        public static bool UserInterests(int userId, List<int> intrst)
        {
            bool status;
            try
            {
                using (_session = MMDatabaseHelper.Create().Session)
                {
                    using (_transaction = _session.BeginTransaction())
                    {
                        Users users = _session.CreateCriteria<Users>().List<Users>().FirstOrDefault(u => u.UserId.Equals(userId));
                        if (users != null)
                        {
                            _session.CreateSQLQuery(
                                    $"Delete From User_Interest Where User_id={userId}")
                                .ExecuteUpdate();

                            foreach (int intr in intrst)
                            {
                                _session.CreateSQLQuery(
                                        $"Insert Into User_Interest(User_id, Interests_Id) Values({userId}, {intr})")
                                    .ExecuteUpdate();
                            }
                            _transaction.Commit();
                            status = true;
                        }
                        else
                        {
                            _transaction.Rollback();
                            status = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                status = false;
            }
            return status;
        }
        
    }
}
