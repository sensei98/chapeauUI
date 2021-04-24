using Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Policy;
using System;
using System.Configuration;
using System.Data;

namespace DAL
{
    public class EmployeeDAO : Base
    {
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            SqlCommand cmd = new SqlCommand("SELECT employeeID, firstName, [password], pin, phone, lastName, roleID FROM Employees", dbConnection);
            dbConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Employee employee = ReadEmployee(reader);
                employees.Add(employee);
            }
            reader.Close();
            dbConnection.Close();
            return employees;
        }
        private Employee ReadEmployee(SqlDataReader reader)
        {
            Employee employee = new Employee
            {
                EmployeeID = (int)reader["employeeID"],
                FirstName = (string)reader["firstName"],
                LastName = (string)reader["lastName"],
                Role = (Role)reader["roleID"],
                Password = (string)reader["password"],
                Pin = (int)reader["pin"],
                PhoneNumber = (int)reader["phone"]
            };
            return employee;
        }
        public int EditEmployee(Employee employee)
        {
            SqlCommand cmd = new SqlCommand("UPDATE employees SET firstName = @fname, lastname = @lname, phone = @phone, [password] = @pass, pin = @pin, roleID = @role WHERE employeeID = @empid", dbConnection);
            cmd.Parameters.AddWithValue("@fname", employee.FirstName);
            cmd.Parameters.AddWithValue("@lname", employee.LastName);
            cmd.Parameters.AddWithValue("@phone", employee.PhoneNumber);
            cmd.Parameters.AddWithValue("@pass", employee.Password);
            cmd.Parameters.AddWithValue("@pin", employee.Pin);
            cmd.Parameters.AddWithValue("@role", employee.Role);
            cmd.Parameters.AddWithValue("@empid", employee.EmployeeID);
            dbConnection.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            dbConnection.Close();
            return rowsAffected;
        }
        public int AddNewEmployee(Employee employee)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO employees(firstname, lastname, phone, roleID, [password], pin) VALUES	(@fname, @lname, @phone, @role, @pass, @pin);", dbConnection);
            cmd.Parameters.AddWithValue("@fname", employee.FirstName);
            cmd.Parameters.AddWithValue("@lname", employee.LastName);
            cmd.Parameters.AddWithValue("@phone", employee.PhoneNumber);
            cmd.Parameters.AddWithValue("@role", employee.Role);
            cmd.Parameters.AddWithValue("@pass", employee.Password);
            cmd.Parameters.AddWithValue("@pin", employee.Pin);
            dbConnection.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            dbConnection.Close();
            return rowsAffected;
        }
        public int RemoveEmployee(Employee employee)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Employees WHERE employeeid = @id", dbConnection);
            cmd.Parameters.AddWithValue("@id", employee.EmployeeID);
            dbConnection.Open();

            int rowsAffected = cmd.ExecuteNonQuery();
            dbConnection.Close();
            return rowsAffected;
        }
        public Employee GetEmployeeInfo(int username, string password)
        {
            SqlCommand sqlCommand = new SqlCommand("SELECT employeeID, firstName, lastName, phone, roleID, [password], PIN FROM Employees WHERE employeeID = @id AND [password] = @pass", dbConnection);
            sqlCommand.Parameters.AddWithValue("@id", username);
            sqlCommand.Parameters.AddWithValue("@pass", password);
            dbConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            reader.Read();
            Employee employee = new Employee
            {
                EmployeeID = (int)reader["employeeID"],
                FirstName = (string)reader["firstName"],
                LastName = (string)reader["lastName"],
                Password = (string)reader["password"],
                Pin = (int)reader["PIN"],
                PhoneNumber = (int)reader["phone"],
                Role = (Role)reader["roleID"]
            };
            reader.Close();
            dbConnection.Close();

            return employee;

        }
    }
}
