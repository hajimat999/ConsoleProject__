using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleProject.Models
{
    class Employee
    {
        private static int _count;
        public readonly string No;
        public string FullName { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }
        public string DepartmentName { get; set; }

        static Employee()
        {
            _count = 1000;
        }

        public Employee(string fullName, string position, double salary, string departmentName)
        {
            FullName = fullName;
            Position = position;
            Salary = salary;
            DepartmentName = departmentName;
            _count++;
            No = $"{departmentName.Substring(0, 2).ToUpper()}{_count}";
        }

        public override string ToString()
        {
            return $"FullName: {FullName} EmployeeNo: {No} DepartmentName: {DepartmentName} Position: {Position} Salary: {Salary}";
        }
    }
}
