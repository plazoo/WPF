using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGOUtil;
using SGOEntities;
using System.Data;
using System.Data.SqlClient;

namespace SGODataAccess
{
    public class DALog
    {
        public int DAInsLog(BELog oEntData)
        {
            int inResult = 0;
            using (var cnn = new SqlConnection(Util.GetStringConnection(Util.CnnType.CnnSGO)))
            {
                using (var cmd = new SqlCommand("usp_INS_TBL_LOG", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@vcException", SqlDbType.NVarChar).Value = oEntData.vcException;
                    cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = oEntData.idUsuario;

                    cnn.Open();
                    inResult = cmd.ExecuteNonQuery();

                    cnn.Close();

                }
            }
            return inResult;

        }
    }
}
