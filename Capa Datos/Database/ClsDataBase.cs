using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
namespace CapaDatos
{
    public class ClsDataBase
    {
        #region Variables privadas

        private MySqlConnection _objMySqlConnection;
        private MySqlDataAdapter _objMySqlDataAdapter;
        private MySqlCommand _objMySqlCommand;
        private DataSet _dsResults;
        private DataTable _dtParameters;
        private string _tableName, _nameSP, _errorDB, _scalarValue, _nameDB;
        private bool _scalar;


        #endregion

        #region Variables publicas

        public MySqlConnection ObjMySqlConnection { get => _objMySqlConnection; set => _objMySqlConnection = value; }
        public MySqlDataAdapter ObjMySqlDataAdapter { get => _objMySqlDataAdapter; set => _objMySqlDataAdapter = value; }
        public MySqlCommand ObjMySqlCommand { get => _objMySqlCommand; set => _objMySqlCommand = value; }
        public DataSet DsResults { get => _dsResults; set => _dsResults = value; }
        public DataTable DtParameters { get => _dtParameters; set => _dtParameters = value; }
        public string TableName { get => _tableName; set => _tableName = value; }
        public string NameSP { get => _nameSP; set => _nameSP = value; }
        public string ErrorDB { get => _errorDB; set => _errorDB = value; }
        public string ScalarValue { get => _scalarValue; set => _scalarValue = value; }
        public string NameDB { get => _nameDB; set => _nameDB = value; }
        public bool Scalar { get => _scalar; set => _scalar = value; }

        #endregion

        #region Constructores

        public ClsDataBase()
        {

            DtParameters = new DataTable("SpParameters");
            DtParameters.Columns.Add("Name");
            DtParameters.Columns.Add("DataType");
            DtParameters.Columns.Add("Value");

            NameDB = string.Empty;

        }

        #endregion

        #region Metodos privados

        private void CrearConexionBaseDatos(ref ClsDataBase ObjDataBase)
        {
            ObjDataBase.NameDB = "petshopdellitoral";
            string cadenaConexion = "Server=localhost; Database=petshopdellitoral; Uid=root; Pwd=;";
            ObjDataBase.ObjMySqlConnection = new MySqlConnection(cadenaConexion);

        }


        private void ValidarConexionBaseDatos(ref ClsDataBase ObjDataBase)
        {

            if (ObjDataBase.ObjMySqlConnection == null)
            {
                CrearConexionBaseDatos(ref ObjDataBase);
            }

            if (ObjDataBase.ObjMySqlConnection.State == ConnectionState.Closed)
            {
                ObjDataBase.ObjMySqlConnection.Open();
            }
            else if (ObjDataBase.ObjMySqlConnection.State == ConnectionState.Broken)
            {
                ObjDataBase.ObjMySqlConnection.Close();
                ObjDataBase.ObjMySqlConnection.Open();
            }
        }


        private void AgregarParametros(ref ClsDataBase ObjDataBase)
        {
            // Esto añade los parámetros que luego le enviaremos a los procedimientos almacenados de MySQL
            if (ObjDataBase.DtParameters != null && ObjDataBase.DtParameters.Rows.Count > 0)
            {
                foreach (DataRow row in ObjDataBase.DtParameters.Rows)
                {
                    MySqlParameter parameter = new MySqlParameter();
                    parameter.ParameterName = row["Name"].ToString();

                    // Asignamos el tipo de dato y el valor correspondiente
                    parameter.Value = row["Value"];
                    ObjDataBase.ObjMySqlCommand.Parameters.Add(parameter);
                }
            }
        }



        private void PrepararConexionBaseDatos(ref ClsDataBase ObjDataBase)
        {
            ValidarConexionBaseDatos(ref ObjDataBase);

            ObjDataBase.ObjMySqlCommand = new MySqlCommand();
            ObjDataBase.ObjMySqlCommand.Connection = ObjDataBase.ObjMySqlConnection;
            ObjDataBase.ObjMySqlCommand.CommandText = ObjDataBase.NameSP;

            // Indicamos que usaremos un Procedimiento Almacenado (Stored Procedure)
            ObjDataBase.ObjMySqlCommand.CommandType = CommandType.StoredProcedure;

            // Llamamos al método que rellena los parámetros
            AgregarParametros(ref ObjDataBase);
        }

        private void EjecutarDataAdapter(ref ClsDataBase ObjDataBase)
        {
            try
            {
                PrepararConexionBaseDatos(ref ObjDataBase);

                ObjDataBase.ObjMySqlDataAdapter = new MySqlDataAdapter(ObjDataBase.ObjMySqlCommand);
                ObjDataBase.DsResults = new DataSet();

                // Rellenamos el DataSet con el nombre de la tabla configurada
                ObjDataBase.ObjMySqlDataAdapter.Fill(ObjDataBase.DsResults, ObjDataBase.TableName);
            }
            catch (Exception ex)
            {
                ObjDataBase.ErrorDB = ex.Message;
            }
            finally
            {
                if (ObjDataBase.ObjMySqlConnection.State == ConnectionState.Open)
                {
                    ObjDataBase.ObjMySqlConnection.Close();
                }
            }
        }

        private void EjecutarCommand(ref ClsDataBase ObjDataBase)
        {
            try
            {
                PrepararConexionBaseDatos(ref ObjDataBase);

                if (ObjDataBase.Scalar)
                {
                    // Si es una consulta que devuelve un solo valor (ej: un COUNT o un MAX)
                    object result = ObjDataBase.ObjMySqlCommand.ExecuteScalar();
                    ObjDataBase.ScalarValue = result != null ? result.ToString() : string.Empty;
                }
                else
                {
                    // Si es un INSERT, UPDATE o DELETE
                    ObjDataBase.ObjMySqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ObjDataBase.ErrorDB = ex.Message;
            }
            finally
            {
                if (ObjDataBase.ObjMySqlConnection.State == ConnectionState.Open)
                {
                    ObjDataBase.ObjMySqlConnection.Close();
                }
            }
        }


        #endregion

        #region Metodos públicos


        #region Metodos públicos

        public bool ProbarConexion(out string mensajeError)
        {
            try
            {
                mensajeError = string.Empty;

                // Llamamos a tu método privado para armar la cadena con "petshopdellitoral"
                ClsDataBase tempDb = this;
                CrearConexionBaseDatos(ref tempDb);

                // Intentamos abrir y cerrar la conexión
                ObjMySqlConnection.Open();
                ObjMySqlConnection.Close();

                return true;
            }
            catch (Exception ex)
            {
                mensajeError = ex.Message;
                return false;
            }
        }


        // Método público puente para consultas de selección (SELECT / Stored Procedures)
        public void EjecutarSelect()
        {
            ClsDataBase tempDb = this;
            EjecutarDataAdapter(ref tempDb);
        }

        // Método público puente para operaciones de inserción, actualización o eliminación (INSERT, UPDATE, DELETE)
        public void EjecutarAccion()
        {
            ClsDataBase tempDb = this;
            EjecutarCommand(ref tempDb);
        }
        #endregion

        #endregion


    }
}
