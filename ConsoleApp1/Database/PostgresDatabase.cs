using System;
using System.Collections.Generic;
using Npgsql;

namespace ConsoleApp1.Database
{
    public class PostgresDatabase
    {
        public string GetVersion()
        {
            var tmpConnection = GetPostgresConnection(PostgresContract.Host, PostgresContract.User, PostgresContract.Password);
            var selectVersionQuery = "SELECT version()";
            
            var selectVersionCommand = new NpgsqlCommand(
                selectVersionQuery
                ,tmpConnection);
            
            var version = selectVersionCommand.ExecuteScalar().ToString();

            return version;
        }

        public NpgsqlConnection GetPostgresConnection(string host, string username, string password)
        {
            NpgsqlConnection tmpConnection;
            try
            {
                string connStr = $"Host={host};Username={username};Password={password};";
                
                tmpConnection = new NpgsqlConnection(connStr);
                tmpConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

            return tmpConnection;
        }
        
        public NpgsqlConnection ConnectToDatabase(string host, string username, string password, string database)
        {
            NpgsqlConnection tmpConnection;
            try
            {
                string connStr = $"Host={host};Username={username};Password={password};Database={database}";
                
                tmpConnection = new NpgsqlConnection(connStr);
                tmpConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

            return tmpConnection;
        }

        public bool CheckDatabaseExists(string databaseName)
        {
            var tmpConnection = GetPostgresConnection(PostgresContract.Host, PostgresContract.User, PostgresContract.Password);
            
            var command = new NpgsqlCommand(
                $"SELECT datname FROM pg_database WHERE datname = '{databaseName}';"
                , tmpConnection);
            
            var result = command.ExecuteScalar();

            return result != null && result.ToString() == databaseName;
        }

        public bool CheckTableExists(string tableName)
        {
            var tmpConnection = ConnectToDatabase(PostgresContract.Host, PostgresContract.User, 
                PostgresContract.Password, PostgresContract.DBname);
            
            var command = new NpgsqlCommand(
                $"SELECT table_name FROM information_schema.tables WHERE table_name = '{tableName}';"
                , tmpConnection);
            
            var result = command.ExecuteScalar();
            
            return result != null && result.ToString() == tableName;
        }

        public void CreateDatabase(string databaseName)
        {
            var tmpConnection = GetPostgresConnection(PostgresContract.Host, PostgresContract.User, PostgresContract.Password);
            
            var m_createdb_cmd = new NpgsqlCommand(
                $@"
                        CREATE DATABASE {databaseName}
                        WITH OWNER = {PostgresContract.User}
                        ENCODING = 'UTF8'
                        CONNECTION LIMIT = -1;
                        ", tmpConnection);
            
            m_createdb_cmd.ExecuteNonQuery();
        }

        public void CreateTable()
        {
            var tmpConnection = ConnectToDatabase(PostgresContract.Host, PostgresContract.User,
                PostgresContract.Password, PostgresContract.DBname);
            
            var m_createdb_cmd = new NpgsqlCommand(
                $@"CREATE TABLE {PostgresContract.TableName}(
                            Name TEXT
                            CONSTRAINT Name PRIMARY KEY,
                            JsonData Text
                        )"
                , tmpConnection);
            m_createdb_cmd.ExecuteNonQuery();
        }

        public void SaveWorldBehavior(string name, string jsonData)
        {
            var tmpConnection = ConnectToDatabase(PostgresContract.Host, PostgresContract.User,
                PostgresContract.Password, PostgresContract.DBname);
            
            var m_createdb_cmd = new NpgsqlCommand(
                $@"INSERT INTO {PostgresContract.TableName} (Name, JsonData) VALUES(
                            '{name}', '{jsonData}'
                        )"
                , tmpConnection);
            m_createdb_cmd.ExecuteNonQuery();
        }
        
        public List<string> GetWorldBehaviorByName(string name)
        {
            var tmpConnection = ConnectToDatabase(PostgresContract.Host, PostgresContract.User,
                PostgresContract.Password, PostgresContract.DBname);
            
            var m_createdb_cmd = new NpgsqlCommand(
                $@"SELECT * FROM {PostgresContract.TableName} WHERE Name = '{name}'"
                , tmpConnection);
            
            var reader = m_createdb_cmd.ExecuteReader();
            var listForResult = new List<string>();

            if (!reader.Read())
                return null;
            
            int ordName = reader.GetOrdinal("name");
            int ordJsonData = reader.GetOrdinal("jsondata");
            
            listForResult.Add(reader.GetString(ordName));
            listForResult.Add(reader.GetString(ordJsonData));

            if (reader.Read())
                throw new InvalidOperationException("Multiple records were returned.");
            
            return listForResult;
        }
        
        public void UpdateWorldBehaviorByName(string name, string jsonData)
        {
            var tmpConnection = ConnectToDatabase(PostgresContract.Host, PostgresContract.User,
                PostgresContract.Password, PostgresContract.DBname);
            
            var m_createdb_cmd = new NpgsqlCommand(
                $@"UPDATE {PostgresContract.TableName} SET JsonData = '{jsonData}' WHERE name = '{name}'"
                , tmpConnection);
            m_createdb_cmd.ExecuteNonQuery();
        }

        public void DeleteWorldBehaviorByName(string name)
        {
            var tmpConnection = ConnectToDatabase(PostgresContract.Host, PostgresContract.User,
                PostgresContract.Password, PostgresContract.DBname);
            
            var m_createdb_cmd = new NpgsqlCommand(
                $@"DELETE FROM {PostgresContract.TableName} WHERE name = '{name}'"
                , tmpConnection);
            m_createdb_cmd.ExecuteNonQuery();
        }
    }
}