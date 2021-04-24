using System.Collections.Generic;
using DAL;
using Model;
using System;

namespace Logic
{
    public class EmployeeService
    {
        private EmployeeDAO employeeDAO = new EmployeeDAO();
        private List<Employee> employees = new List<Employee>();
        public List<Employee> GetAllEmployees()
        {
           

            try { return employees = employeeDAO.GetAllEmployees(); }
            catch (Exception)
            {
                Employee e = new Employee()
                {
                    EmployeeID = 0,
                    FirstName = "test",
                    LastName = "test",
                    Role = Role.unspecified,
                    Password = "test",
                    Pin = 1234,
                    PhoneNumber = 0000
                };
                employees.Add(e);
                return employees;
            }
        }
        public int EditEmployee(Employee employee, int employeeID)
        {
            try 
            { 
                return employeeDAO.EditEmployee(employee); 
            }
            catch(Exception)
            {
                int EditemployeeWithID = 0;
                return EditemployeeWithID;
            }
        }
        public void AddNewEmployee(Employee employee)
        {
            try { employeeDAO.AddNewEmployee(employee); }
            catch(Exception)
            {
                Employee e = new Employee()
                {
                    FirstName = "test",
                    LastName = "test",
                    Role = Role.unspecified,
                    Password = "test",
                    Pin = 1234,
                    PhoneNumber = 0000
                };
                employees.Add(e);
            }
        }
        public void RemoveEmployee(Employee employee)
        {
            try { employeeDAO.RemoveEmployee(employee); }
            catch(Exception)
            {
                Employee e = new Employee()
                {
                    EmployeeID = 0,
                    FirstName = "test",
                    LastName = "test",
                    Role = Role.unspecified,
                    Password = "test",
                    Pin = 1234,
                    PhoneNumber = 0000
                };
                employees.Add(e);
            }
        }
        public Employee GetEmployeeInfo(int username, string password)
        {
            Employee employee;

            try { return employee = employeeDAO.GetEmployeeInfo(username, password); }
            catch (Exception)
            {
                Employee e = new Employee()
                {
                    Password = "test",
                    Pin = 1234,
                    PhoneNumber = 0000
                };
                employees.Add(e);
                return e;
            }
        }
    }
}
