using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
namespace CapaDatos.Database;

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
            ObjDataBase.ObjMySqlConnection = new MySqlConnection(); 

            string cadenaConexion = "Server=localhost; Database=petshopdellitoral; Uid=root; Pwd=;";
            ObjDataBase.ObjMySqlConnection = new MySqlConnection(cadenaConexion);
    }

       
        private void ValidarConexionBaseDatos(ref ClsDataBase ObjDataBase)
        { 
        }


        private void AgregarParametros(ref ClsDataBase ObjDataBase)     
        {
        }

        

        private void PrepararConexionBaseDatos(ref ClsDataBase ObjDataBase)
        {   
        }

        private void EjecutarDataAdapter(ref ClsDataBase ObjDataBase)
        {
        }

        private void EjecutarCommand(ref ClsDataBase ObjDataBase)
        {
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

    #endregion

    #endregion


}
