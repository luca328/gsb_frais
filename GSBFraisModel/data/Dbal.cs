﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSBFrais.Model.data
{
    public class Dbal
    {
        MySqlConnection connection;
        public Dbal(string database = "gsb_frais", string uid = "root", string password = "root", string server = "localhost")
        {
            Initialize(database, uid, password, server);
        }
        public void Initialize(string database, string uid, string password, string server)
        {
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "port=3306;" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        //CURQuery: Create, Update, Delete query execution method
        public void CUDQuery(string query)
        {
            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query,connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        public void Insert(string query)
        {
            query = "INSERT INTO " + query;
            CUDQuery(query);
        }
        public void update(string query)
        {
            query = "UPDATE " + query;
            CUDQuery(query);
        }
        public void delete(string query)
        {
            query = "DELETE FROM " + query;
            CUDQuery(query);
        }
        //RQuery: Read query method (to execute SELECT queries)
        private DataSet RQuery(string query)
        {
            DataSet dataset = new DataSet();
            //open connection
            if(this.OpenConnection() == true)
            {
                //add query data in a DataSet
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                adapter.Fill(dataset);
                CloseConnection();
            }
            return dataset;
        }
        public DataTable SelectAll(string table)
        {
            string query = "SELECT * FROM " + table;
            DataSet dataset = RQuery(query);
            return dataset.Tables[0];
        }
        public DataRow SelectById(string table, string id)
        {
            string query = "SELECT * FROM " + table + " where id ='" + id +"'";
            DataSet dataset = RQuery(query);
            return dataset.Tables[0].Rows[0];
        }
        public DataTable SelectByField(string table, string fieldTestCondition)
        {
            string query = " SELECT * FROM " + table +" where " + fieldTestCondition;
            DataSet dataset = RQuery(query);
            return dataset.Tables[0];
        }

        public DataTable SelectByComposedFK2(string table, string keyname1, string keyvalue1, string keyname2, string keyvalue2)
        {
            string query = "SELECT * FROM " + table + " where " + keyname1 + " = '" + keyvalue1 + "' AND " + keyname2 + " = '" + keyvalue2 + "'";
            DataSet dataset = RQuery(query);

            return dataset.Tables[0];
        }

        public DataRow SelectByComposedPK2(string table, string keyname1, string keyvalue1, string keyname2, string keyvalue2)
        {
            string query = "SELECT * FROM " + table + " where " + keyname1 + " = '" + keyvalue1 + "' AND " + keyname2 + " = '" + keyvalue2 + "'";
            DataSet dataset = RQuery(query);
            if (dataset.Tables[0].Rows.Count != 0)
            {
                return dataset.Tables[0].Rows[0];
            }else
                return null;
        }
        public DataTable SelectDistinctByField(string field, string table, string orderBy)
        {
            string query = " SELECT Distinct(" + field + ") FROM " + table + " ORDER BY " + field + " " + orderBy;
            DataSet dataset = RQuery(query);
            return dataset.Tables[0];
        }
    }
}
