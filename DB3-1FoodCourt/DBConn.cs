using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB3_1FoodCourt
{
    internal class DBConn
    {
        OracleConnection conn;
        public DBConn(string constr)
        {
            try
            {
                conn = new OracleConnection(constr);
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB연결 오류", "DB 연결하지 못했습니다.");
            }
        }

        public OracleConnection getConn()
        {
            return conn;
        }
        public void close()
        {
            conn.Close();
        }
        public OracleDataReader ExecuteReader(string sql)
        {
            OracleCommand cmd = new OracleCommand(sql,conn);
            return cmd.ExecuteReader();
        }
        public void ExecuteNonQuery(string sql)
        {
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }

    }
}
