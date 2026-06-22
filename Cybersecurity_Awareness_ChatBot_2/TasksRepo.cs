using Microsoft.Data.SqlClient;

namespace Cybersecurity_Awareness_ChatBot_2
{

	public class TasksRepo
    {
        // Connection string to the local SQL Server database
        public static readonly string connection =
            @"Data Source=(localdb)\MSSQLLocalDB;
             Initial Catalog=Tasksdb;
             Integrated Security=True";

        // Method to add a new task to the database and return the generated TaskId
        public int AddTask(string title, string description, DateTime? reminderDate) {

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = @"INSERT INTO Tasks(TaskTitle, TaskDescription, TaskReminderDate)
                                OUTPUT INSERTED.TaskId
                                VALUES(@title, @description, @reminderDate)";

                SqlCommand cmd = new SqlCommand(query, conn);
                
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@reminderDate", reminderDate.HasValue ? (object)reminderDate.Value : DBNull.Value);
                    

                int taskPK = (int)cmd.ExecuteScalar();// Execute the command and retrieve the generated TaskId
                return taskPK;
                
            }
        
        }
        // Method to retrieve tasks from the database based on the provided TaskId
        public List<CyberTasks> GetTasks(int idPart)
        {
            List<CyberTasks> tasks = new List<CyberTasks>();

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = @"SELECT TaskTitle, TaskDescription, TaskReminderDate
                                FROM Tasks
                                WHERE TaskId = @idPart";// Query to select tasks based on the provided TaskId

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idPart", idPart);// Add the TaskId parameter to the command


                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Read the results from the database and populate the list of CyberTasks
                    while (reader.Read())
                    {
                       tasks.Add(new CyberTasks
                        {
                            TaskTitle = reader["TaskTitle"].ToString(),// Retrieve the TaskTitle from the database and convert it to a string
                           TaskDescription = reader["TaskDescription"].ToString(),
                            TaskReminderDate = reader["TaskReminderDate"] == DBNull.Value ? null :Convert.ToDateTime(reader["TaskReminderDate"])
                       });
                        
                    }
                }

                return tasks;
            }
        }

        // Method to mark a task as completed in the database based on the provided TaskId
        public void CompletedTask(int taskID) {

            using (SqlConnection conn = new SqlConnection(connection)) {

                conn.Open();
                string query = @"UPDATE Tasks
                               SET TaskStatus = 1
                               WHERE TaskId = @taskID";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@taskID", taskID);
                cmd.ExecuteNonQuery();

            }
        
        }

        // Method to delete a task from the database based on the provided TaskId
        public void DeleteTask(int taskID)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = @"DELETE FROM Tasks
                               WHERE TaskId = @taskID";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@taskID", taskID);
                cmd.ExecuteNonQuery();
            }



        }
}
}