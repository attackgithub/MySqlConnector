using System.Collections;
using System.Collections.Generic;
using System.Data.Common;

namespace MySql.Data.MySqlClient
{
	internal sealed class DbBatchCommandList : IList<DbBatchCommand>
	{
		public DbBatchCommandList(List<MySqlDbBatchCommand> batchCommands) => m_batchCommands = batchCommands;

		public void Add(DbBatchCommand item) => m_batchCommands.Add((MySqlDbBatchCommand) item);
		public void Clear() => m_batchCommands.Clear();
		public bool Contains(DbBatchCommand item) => m_batchCommands.Contains((MySqlDbBatchCommand) item);
		public IEnumerator<DbBatchCommand> GetEnumerator() => m_batchCommands.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		public bool Remove(DbBatchCommand item) => m_batchCommands.Remove((MySqlDbBatchCommand) item);
		public int Count => m_batchCommands.Count;
		public bool IsReadOnly => false;
		public int IndexOf(DbBatchCommand item) => m_batchCommands.IndexOf((MySqlDbBatchCommand) item);
		public void Insert(int index, DbBatchCommand item) => m_batchCommands.Insert(index, (MySqlDbBatchCommand) item);
		public void RemoveAt(int index) => m_batchCommands.RemoveAt(index);

		public DbBatchCommand this[int index]
		{
			get => m_batchCommands[index];
			set => m_batchCommands[index] = (MySqlDbBatchCommand) value;
		}

		public void CopyTo(DbBatchCommand[] array, int arrayIndex)
		{
			for (var i = 0; i < m_batchCommands.Count; i++)
				array[arrayIndex + i] = m_batchCommands[i];
		}

		readonly List<MySqlDbBatchCommand> m_batchCommands;
	}
}
