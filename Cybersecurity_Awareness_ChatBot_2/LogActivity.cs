using Microsoft.Data.SqlClient;

namespace Cybersecurity_Awareness_ChatBot_2
{
    public class LogActivity
    {
        // Method to add a log entry to the ActivityLog table same as adding a task, but with different parameters and table
        public void AddLogActivity(string userName, string actionType, string details)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(TasksRepo.connection))
                {
                    conn.Open();
                    string query = @"INSERT INTO ActivityLog (Username, ActionType, Details) 
                            VALUES (@userName, @action, @details)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // If username is empty or null, pass DBNull
                        cmd.Parameters.AddWithValue("@userName", string.IsNullOrEmpty(userName) ? DBNull.Value : (object)userName);
                        cmd.Parameters.AddWithValue("@action", actionType);
                        cmd.Parameters.AddWithValue("@details", details);

                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch { 
            
            
            }
        }

        // Method to retrieve activity logs for a specific user, ordered by most recent first
        public List<string> GetActivityLog(string userName) {

            List<string> userLogs = new List<string>();

            using (SqlConnection conn = new SqlConnection(TasksRepo.connection)) // Using the static connection
            {

                conn.Open();
                // Query to get logs for the specific user, newest first
                string query = @"SELECT TOP 10 ActionType, Details, Timestamp 
                        FROM ActivityLog 
                        WHERE Username = @username 
                        ORDER BY Timestamp DESC";// We only select the first 10 fields we need for display, and order by Timestamp descending to show newest logs first, for the current user that is logged in

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", userName);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Counter to number the log entries
                        int count = 1;
                        while (reader.Read())// Loop through each log entry and format it for display, same as when we display tasks, but with different fields and formatting
                        {
                            // Format the log row cleanly into a string
                            string Timestamp = Convert.ToDateTime(reader["Timestamp"]).ToString("yyyy-MM-dd HH:mm:ss");
                            string ActionType = reader["ActionType"].ToString();
                            string Details = reader["Details"].ToString();

                            // format log entry
                            string logEntry = $"{count} -> [{Timestamp}] {ActionType}: {Details}";
                            userLogs.Add(logEntry);
                            count++;
                        }
                    }
                }

            }

            return userLogs;
        }
    }
}