using Dapper;
using Microsoft.Data.SqlClient;
using BlazorApp1.Models;

namespace BlazorApp1.Data
{
    public class TaskRepository
    {
        private readonly string _connectionString;

        public TaskRepository(string cns)
        {
            _connectionString = cns;
        }

        // Get all TaskItems for the logged-in user
        public async Task<IEnumerable<TaskItem>> GetTasksByUserAsync(string userEmail)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
                SELECT T.*
                FROM Tasks T
                INNER JOIN UserTasks UT ON T.Id = UT.TaskId
                WHERE UT.EmailAddress = @EmailAddress";
            return await connection.QueryAsync<TaskItem>(query, new { EmailAddress = userEmail });
        }

        // Get a single TaskItem by ID for the logged-in user
        public async Task<TaskItem?> GetTaskByIdAsync(int taskId, string userEmail)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
                SELECT T.*
                FROM Tasks T
                INNER JOIN UserTasks UT ON T.Id = UT.TaskId
                WHERE T.Id = @Id AND UT.EmailAddress = @EmailAddress";
            return await connection.QueryFirstOrDefaultAsync<TaskItem>(query, new { Id = taskId, EmailAddress = userEmail });
        }

        // Create a TaskItem and associate it with the logged-in user
        public async Task AddTaskAsync(TaskItem task, string userEmail)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync(); // Test the connection to the database

                var query = @"INSERT INTO Tasks (Name, Description, DueDate, IsCompleted, Priority, Complexity)
                      OUTPUT INSERTED.Id
                      VALUES (@Name, @Description, @DueDate, @IsCompleted, @Priority, @Complexity)";
                var taskId = await connection.ExecuteScalarAsync<int>(query, task);

                var userTaskQuery = @"INSERT INTO UserTasks (EmailAddress, TaskId) VALUES (@EmailAddress, @TaskId)";
                await connection.ExecuteAsync(userTaskQuery, new { EmailAddress = userEmail, TaskId = taskId });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while adding task: {ex.Message} , {task.Priority}");
                // Optionally log the error or rethrow if needed
                throw;
            }
        }


        // Update a TaskItem (restricted to the user's tasks)
        public async Task UpdateTaskAsync(TaskItem task, string userEmail)
        {
            using var connection = new SqlConnection(_connectionString);

            var query = @"
                UPDATE Tasks
                SET Name = @Name, Description = @Description, DueDate = @DueDate,
                    IsCompleted = @IsCompleted, Priority = @Priority, Complexity = @Complexity
                WHERE Id = @Id AND EXISTS (
                    SELECT 1 FROM UserTasks WHERE TaskId = @Id AND EmailAddress = @EmailAddress)";
            await connection.ExecuteAsync(query, new { task.Name, task.Description, task.DueDate, task.IsCompleted, task.Priority, task.Complexity, task.Id, EmailAddress = userEmail });
        }

        // Delete a TaskItem (restricted to the user's tasks)
        public async Task DeleteTaskAsync(int taskId, string userEmail)
        {
            using var connection = new SqlConnection(_connectionString);

            var query = @"
                DELETE FROM Tasks
                WHERE Id = @Id AND EXISTS (
                    SELECT 1 FROM UserTasks WHERE TaskId = @Id AND EmailAddress = @EmailAddress)";
            await connection.ExecuteAsync(query, new { Id = taskId, EmailAddress = userEmail });
        }
    }
}
