
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using CRS.BusinessEntities;

namespace CRS.Dal
{
	internal class Helpers
	{
		const string idParamName = "@id";
		const string concurrencyParamName = "@concurrencyId";
        /*
		internal static void SetSaveParameters(SqlCommand command, ICRSBase businessBase)
		{
			DbParameter idParam = command.CreateParameter();
			idParam.DbType = DbType.Int32;
			idParam.Direction = ParameterDirection.InputOutput;
			idParam.ParameterName = idParamName;
			if (businessBase.Id == 0)
			{
				idParam.Value = DBNull.Value;
			}
			else
			{
				idParam.Value = businessBase.Id;
			}
			command.Parameters.Add(idParam);

			DbParameter rowVersion = command.CreateParameter();
			rowVersion.ParameterName = concurrencyParamName;
			rowVersion.Direction = ParameterDirection.InputOutput;
			rowVersion.DbType = DbType.Binary;
			rowVersion.Size = 8;

			if (businessBase.ConcurrencyId == null)
			{
				rowVersion.Value = DBNull.Value;
			}
			else
			{
				rowVersion.Value = businessBase.ConcurrencyId;
			}
			command.Parameters.Add(rowVersion);

			DbParameter returnValue = command.CreateParameter();
			returnValue.Direction = ParameterDirection.ReturnValue;
			command.Parameters.Add(returnValue);
		}
        */
		internal static byte[] GetConcurrencyId(SqlCommand command)
		{
			return (byte[])command.Parameters[concurrencyParamName].Value;
		}

		internal static int GetBusinessBaseId(SqlCommand command)
		{
			return (int)command.Parameters[idParamName].Value;
		}
	}
}
