namespace SqlStreamStore
{
    using Npgsql;
    using System.Data;
    using System.Threading;
    using System.Threading.Tasks;

    internal static class DbTransactionExtensions
    {
        public static Task CommitAsync(this IDbTransaction transaction, CancellationToken cancellationToken)
        {
            if(transaction is NpgsqlTransaction npgsqlTransaction)
            {
                return npgsqlTransaction.CommitAsync(cancellationToken);
            }

            if (cancellationToken.IsCancellationRequested)
                return Task.FromCanceled(cancellationToken);

            transaction.Commit();
            return Task.CompletedTask;
        }

        public static Task RollbackAsync(this IDbTransaction transaction, CancellationToken cancellationToken)
        {
            if (transaction is NpgsqlTransaction npgsqlTransaction)
            {
                return npgsqlTransaction.RollbackAsync(cancellationToken);
            }

            if (cancellationToken.IsCancellationRequested)
                return Task.FromCanceled(cancellationToken);

            transaction.Rollback();
            return Task.CompletedTask;
        }
    }
}
