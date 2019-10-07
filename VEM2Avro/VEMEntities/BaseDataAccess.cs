using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace VEM2Avro.VEMEntities
{
    public class SQLDataAccess
    {

        protected string ConnectionString { get; set; }

        public SQLDataAccess()
        {

            this.ConnectionString = "Data Source=10.218.65.16;Initial Catalog=HIS_Vem_Lite;Persist Security Info=True;User ID=VEM;Password=VemTest2019";
        }

        private SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            return connection;
        }
        public DbDataReader GetDataReader(string commandText, List<SqlParameter> parameters)
        {
            DbDataReader dr;

            try
            {
                SqlConnection connection = this.GetConnection();
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = commandText;

                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }

                    dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
            catch (System.Exception ex)
            {
                throw;
            }

            return dr;
        }
        public DataSet GetDataSetReader(string commandText, List<SqlParameter> parameters)
        {
            DataSet _dataset;

            try
            {
                SqlConnection connection = this.GetConnection();
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = commandText;

                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter(commandText, connection);

                    _dataset = new DataSet();
                    adapter.Fill(_dataset, "DataSet");

                }
            }
            catch (System.Exception ex)
            {
                throw;
            }

            return _dataset;
        }

        //public DbDataReader GetDataReader(string procedureName,List<SqlParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
        //{
        //    DbDataReader dr;

        //    try
        //    {
        //        DbConnection connection = this.GetConnection();
        //        {
        //            DbCommand cmd = this.GetCommand(connection, procedureName, commandType);
        //            if (parameters != null && parameters.Count > 0)
        //            {
        //                cmd.Parameters.AddRange(parameters.ToArray());
        //            }

        //            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw;
        //    }

        //    return dr;
        //}
    }
}
