﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CapaEntidades;



namespace CapaDatos
{
    public class D_InicioSesion
    {
        readonly SqlConnection Conectar = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);

        public bool IniciarSesion(string Usuario, string Contraseña)
        {
            SqlCommand Comando = new SqlCommand("spuIniciarSesion", Conectar)
            {
                CommandType = CommandType.StoredProcedure
            };

            Conectar.Open();
            Comando.Parameters.AddWithValue("@Usuario", Usuario);
            Comando.Parameters.AddWithValue("@Contraseña", Contraseña);

            SqlDataReader LeerFilas = Comando.ExecuteReader();
            if (LeerFilas.HasRows)
            {
                while (LeerFilas.Read())
                {
                    E_InicioSesion.Perfil = (byte[])LeerFilas.GetValue(0);
                    E_InicioSesion.Usuario = LeerFilas.GetString(1);
                    E_InicioSesion.Contraseña = LeerFilas.GetString(2);
                    E_InicioSesion.Acceso = LeerFilas.GetString(3);
                    E_InicioSesion.Datos = LeerFilas.GetString(4);
                }
                return true;
            }
            else
                return false;
        }
    }
}
