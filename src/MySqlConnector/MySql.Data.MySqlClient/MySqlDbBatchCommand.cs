using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NET45 || NET461 || NET471 || NETSTANDARD1_3 || NETSTANDARD2_0 || NETCOREAPP2_1
namespace System.Data.Common
{
	public abstract class DbBatchCommand
	{
		public abstract string CommandText { get; set; }
		public abstract CommandType CommandType { get; set; }
		public abstract CommandBehavior CommandBehavior { get; set; }
		public abstract int RecordsAffected { get; set; }

		public DbParameterCollection Parameters => DbParameterCollection;
		protected abstract DbParameterCollection DbParameterCollection { get; }
	}
}
#endif

namespace MySql.Data.MySqlClient
{
	public sealed class MySqlDbBatchCommand : DbBatchCommand
	{
		public MySqlDbBatchCommand()
			: this(null)
		{
		}

		public MySqlDbBatchCommand(string commandText)
		{
			CommandText = commandText;
			CommandType = CommandType.Text;
		}

		public override string CommandText { get; set; }
		public override CommandType CommandType { get; set; }
		public override CommandBehavior CommandBehavior { get; set; }
		public override int RecordsAffected { get; set; }
		protected override DbParameterCollection DbParameterCollection => Parameters;

		public new MySqlParameterCollection Parameters
		{
			get
			{
				if (m_parameterCollection is null)
					m_parameterCollection = new MySqlParameterCollection();
				return m_parameterCollection;
			}
		}

		MySqlParameterCollection m_parameterCollection;
	}
}
