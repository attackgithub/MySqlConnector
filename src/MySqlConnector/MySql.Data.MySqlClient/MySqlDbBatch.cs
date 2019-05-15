using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

#if NET45 || NET461 || NET471 || NETSTANDARD1_3 || NETSTANDARD2_0 || NETCOREAPP2_1
namespace System.Data.Common
{
	public abstract class DbBatch : IDisposable
	{
		public IList<DbBatchCommand> BatchCommands => DbBatchCommands;
		protected abstract IList<DbBatchCommand> DbBatchCommands { get; }

		#region Execution (mirrors DbCommand)

		public abstract DbDataReader ExecuteReader();
		public abstract Task<DbDataReader> ExecuteReaderAsync(CancellationToken cancellationToken = default);

		public abstract int ExecuteNonQuery();
		public abstract Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken = default);

		public abstract object ExecuteScalar();
		public abstract Task<object> ExecuteScalarAsync(CancellationToken cancellationToken = default);

		#endregion

		#region Execution properties (mirrors DbCommand)

		public abstract int Timeout { get; set; }

		// Delegates to DbConnection
		public DbConnection Connection { get; set; }
		protected abstract DbConnection DbConnection { get; set; }

		// Delegates to DbTransaction
		public DbTransaction Transaction { get; set; }
		protected abstract DbTransaction DbTransaction { get; set; }

		#endregion

		#region Other methods mirroring DbCommand

		public abstract void Prepare();
		public abstract Task PrepareAsync(CancellationToken cancellationToken = default);
		public abstract void Cancel();
		public abstract Task CancelAsync(CancellationToken cancellationToken = default);

		#endregion

		#region Standard dispose pattern

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing) { }

		#endregion
	}
}
#endif

namespace MySql.Data.MySqlClient
{
	public sealed class MySqlDbBatch : DbBatch
	{
		public MySqlDbBatch()
			: this(null, null)
		{
		}

		public MySqlDbBatch(MySqlConnection connection = null, MySqlTransaction transaction = null)
		{
			Connection = connection;
			Transaction = transaction;
			m_batchCommands = new List<MySqlDbBatchCommand>();
		}

		public new MySqlConnection Connection { get; set; }
		public new MySqlTransaction Transaction { get; set; }

		protected override DbConnection DbConnection
		{
			get => Connection;
			set => Connection = (MySqlConnection) value;
		}

		protected override DbTransaction DbTransaction
		{
			get => Transaction;
			set => Transaction = (MySqlTransaction) value;
		}

		protected override IList<DbBatchCommand> DbBatchCommands
		{
			get
			{
				if (m_dbBatchCommands is null)
					m_dbBatchCommands = new DbBatchCommandList(m_batchCommands);
				return m_dbBatchCommands;
			}
		}

		public override DbDataReader ExecuteReader() => throw new NotImplementedException();

		public override Task<DbDataReader> ExecuteReaderAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();

		public override int ExecuteNonQuery() => throw new NotImplementedException();

		public override Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();

		public override object ExecuteScalar() => throw new NotImplementedException();

		public override Task<object> ExecuteScalarAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();

		public override int Timeout { get; set; }

		public override void Prepare() => throw new NotImplementedException();

		public override Task PrepareAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();

		public override void Cancel() => throw new NotImplementedException();

		public override Task CancelAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();

		readonly List<MySqlDbBatchCommand> m_batchCommands;
		IList<DbBatchCommand> m_dbBatchCommands;
	}
}
