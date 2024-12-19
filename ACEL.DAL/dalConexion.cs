using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using ACEL.ENT;
using ACEL.ENT.Configuraciones;

namespace ACEL.DAL
{
    public class dalConexion
    {
        #region --> Propiedades <----------------------------
        public SqlConnection sqlConnection;
        public SqlTransaction sqlTransaction;
        public dcACELDataContext dcACEL;
        public bool IsInTransaction
        {
            get { return dcACEL.Transaction != null; }
        }
        #endregion

        #region --> Constructor <----------------------------
        public dalConexion()
        {
            dcACEL = new dcACELDataContext();
        }
        #endregion

        #region --> Metodos <--------------------------------
        protected bool TestConnection()
        {
            bool blSuccess = true;
            ConfiguraConexion entconfiguracion = new ConfiguraConexion();
            sqlConnection = new SqlConnection(entconfiguracion.Config());

            try
            {
                sqlConnection.Open();
            }
            catch (Exception e)
            {
                blSuccess = false;
                throw new Exception(e.ToString());
            }
            finally
            {
                sqlConnection.Close();
            }

            return blSuccess;
        }
        public void BeginTransaction()
        {
            dcACEL.CommandTimeout = 0;

            if (IsInTransaction)
            {
                throw new InvalidOperationException("A transaction is already opened");
            }

            try
            {
                this.dcACEL.Connection.Open();
                dcACEL.Transaction = this.dcACEL.Connection.BeginTransaction();
            }
            catch (Exception e)
            {
                throw new ApplicationException();
            }
        }
        public void Commit()
        {
            if (!IsInTransaction)
            {
                throw new InvalidOperationException("Operation requires an active transaction");
            }

            try
            {
                dcACEL.Transaction.Commit();
                dcACEL.Transaction.Dispose();
            }
            catch (Exception e)
            {
                throw new ApplicationException();
            }
            finally
            {
                if (dcACEL.Connection.State == ConnectionState.Open)
                {
                    dcACEL.Connection.Close();
                }
            }
        }
        public void Rollback()
        {
            if (!IsInTransaction)
            {
                throw new InvalidOperationException("Operation requires an active transaction");
            }

            try
            {
                dcACEL.Transaction.Rollback();
                dcACEL.Transaction.Dispose();
            }
            catch (Exception e)
            {
                throw new ApplicationException();
            }
            finally
            {
                if (dcACEL.Connection.State == ConnectionState.Open)
                {
                    dcACEL.Connection.Close();
                }
            }
        }
        public virtual void Dispose()
        {
            dcACEL.Dispose();
        }
        #endregion
    }
}
