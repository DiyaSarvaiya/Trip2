using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using Trip2.Areas.Users_Area.Models;

namespace Trip2.DAL
{
    public class DAL_Base : DAL_Helper
    {
        public DataTable PR_Users_Login(string UserName, string Password)
        {
            try
            {
                SqlDatabase sql = new SqlDatabase(connStr);
                DbCommand dbCmd = sql.GetStoredProcCommand("PR_Login");
                sql.AddInParameter(dbCmd, "UserName", SqlDbType.VarChar, UserName);
                sql.AddInParameter(dbCmd, "Password", SqlDbType.VarChar, Password);

                DataTable dt = new DataTable();
                using (IDataReader dr = sql.ExecuteReader(dbCmd))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable PR_Users_SignIn(UsersModel userModel)
        {
            try
            {
                SqlDatabase sql = new SqlDatabase(connStr);
                DbCommand dbCmd = sql.GetStoredProcCommand("PR_SignIn");
                sql.AddInParameter(dbCmd, "Username", SqlDbType.VarChar, userModel.Username);
                sql.AddInParameter(dbCmd, "Password", SqlDbType.VarChar, userModel.Password);
                sql.AddInParameter(dbCmd, "Email", SqlDbType.VarChar, userModel.Email);
                sql.AddInParameter(dbCmd, "PhoneNo", SqlDbType.VarChar, userModel.PhoneNo);

                DataTable dt = new DataTable();
                using (IDataReader dr = sql.ExecuteReader(dbCmd))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
