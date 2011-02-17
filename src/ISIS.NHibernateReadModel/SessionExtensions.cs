using System;
using NHibernate;

namespace ISIS.NHibernateReadModel
{
    public static class SessionExtensions
    {

        internal static TResult Transact<TResult>(this IStatelessSession session, Func<TResult> func)
        {
            if (!session.Transaction.IsActive)
            {
                TResult result;
                using (var tx = session.BeginTransaction())
                {
                    result = func.Invoke();
                    tx.Commit();
                }
                return result;
            }
            return func.Invoke();
        }

        internal static void Transact(this IStatelessSession session, Action action)
        {
            session.Transact<bool>(() =>
            {
                action.Invoke();
                return false;
            });
        }

        internal static TResult Transact<TResult>(this ISession session, Func<TResult> func)
        {
            if (!session.Transaction.IsActive)
            {
                TResult result;
                using (var tx = session.BeginTransaction())
                {
                    result = func.Invoke();
                    tx.Commit();
                }
                return result;
            }
            return func.Invoke();
        }

        internal static void Transact(this ISession session, Action action)
        {
            session.Transact<bool>(() =>
            {
                action.Invoke();
                return false;
            });
        }

    }
}
