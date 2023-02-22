using System;
using System.Collections.Generic;
using System.Text;
using ConsoleProject.Models;

namespace ConsoleProject.Interfaces
{
    interface IHumanResourceManager
    {
        Department[] Departments { get; }
        void AddDepartment(string name, int workerLimit, double salaryLimit);
        Department[] GetDepartments();
        void EditDepartment(string name, string newName);
        void AddEmployee(string fullName, string position, double salary, string departmentName);
        void RemoveEmployee(string employeeNo, string departmentName);
        void EditEmployee(string departmentName, string employeeNo, string fullName, double salary, string position);
        Employee[] GetAllEmployees();
        Employee[] GetEmployeesByDepartmentName(string departmentName);
    }
}
