using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Model;

namespace TicTacToe.DataAccess
{
    public class PlayerData 
    {
        public static int Count = 0;
        public static string currentToken = null;
        public static bool Add(Player Player)
        {
            try
            {
                using (var connection = new SqlConnection("Data Source=TAVDESK005;Initial Catalog=TicTacToe;User ID=sa;Password=test123!@#"))
                {
                    Count++;
                    connection.Open();
                    string sql = "INSERT INTO Players(Name,Email,Token,Status) VALUES(@Name,@Email,@Token,@Status)";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@Name", Player.Name);
                    cmd.Parameters.AddWithValue("@Email", Player.Email);
                    cmd.Parameters.AddWithValue("@Token", GetToken());
                    if (GetCount() <= 2)
                    {
                        cmd.Parameters.AddWithValue("@Status", true);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Status", false);
                    }
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    Player.ID = GetCurrentUserID();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static int GetCount()
        {
            try
            {
                string connectionString = "Data Source=TAVDESK005;Initial Catalog=TicTacToe;User ID=sa;Password=test123!@#";

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM Players");
                sqlCommand.Connection = sqlConnection;

                int RecordCount = Convert.ToInt32(sqlCommand.ExecuteScalar());
                return RecordCount;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int GetCurrentUserID()
        {
            try
            {
                string connectionString = "Data Source=TAVDESK005;Initial Catalog=TicTacToe;User ID=sa;Password=test123!@#";

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("Select ID from Players where Token = "+currentToken+"");
                sqlCommand.Connection = sqlConnection;

                int currentUserID = Convert.ToInt32(sqlCommand.ExecuteScalar());
                return currentUserID;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static string GetCurrentUserToken(int id)
        {
            try
            {
                string connectionString = "Data Source=TAVDESK005;Initial Catalog=TicTacToe;User ID=sa;Password=test123!@#";

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("Select Token from Players where ID = " + id + "");
                sqlCommand.Connection = sqlConnection;

                string currentUserToken = sqlCommand.ExecuteScalar().ToString();
                return currentUserToken;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static string GetToken()
        {
            //Guid g = Guid.NewGuid();
            //currentToken = Convert.ToBase64String(g.ToByteArray());
            //currentToken = currentToken.Replace("=", "");
            //currentToken = currentToken.Replace("+", "");
            currentToken = Guid.NewGuid().ToString();
            //currentToken = currentToken.Remove('-');
            return currentToken;
        }
        public static bool TokenExistsOrNot(string apiKey)
        {
            try
            {
                string key = null;
                int count = 0;
                using (var connection = new SqlConnection("Data Source=TAVDESK005;Initial Catalog=TicTacToe;User ID=sa;Password=test123!@#"))
                {
                    Count++;
                    connection.Open();
                    string sql = "Select * from Players";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            key = Convert.ToString(dataReader[3]);
                            key = key.Trim();
                            if (key.Equals(apiKey))
                            {
                                count++;
                                break;
                            }
                        }
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            //return true;
        }
    }
}
