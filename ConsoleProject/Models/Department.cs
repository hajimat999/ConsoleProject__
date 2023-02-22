using System;
using System.Collections.Generic;
using System.Text;
using ConsoleProject.Services;


namespace ConsoleProject.Models
{
    class Department
    {
        public string Name { get; set; }
        public int WorkerLimit { get; set; }
        public double SalaryLimit { get; set; }
        public Employee[] Employees;

        public double TotalSalary()
        {
            double totalSalary = 0;

            foreach (Employee employee in Employees)
            {
                totalSalary += employee.Salary;
            }
            return totalSalary;
        }

        public double CalcSalaryAverage()
        {
            double salaryAverage = 0;
            foreach (Employee employee in Employees)
            {
                salaryAverage += employee.Salary;
            }
            if(Employees.Length != 0)
            {
                return salaryAverage / Employees.Length;
            }
            return salaryAverage;
        }

        public Department(string name, int workerLimit, double salaryLimit)
        {
            Employees = new Employee[0];
            Name = name;
            WorkerLimit = workerLimit;
            SalaryLimit = salaryLimit;
        }

        public override string ToString()
        {
            return $"Name: {Name} WorkerLimit: {WorkerLimit} SalaryLimit: {SalaryLimit}";
        }
    }

}
















    

