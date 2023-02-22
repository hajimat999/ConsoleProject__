using ConsoleProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using ConsoleProject.Interfaces;

namespace ConsoleProject.Services

{
    class HumanResourceManager : IHumanResourceManager
    {
        private Department[] _departments;

        public Department[] Departments { get => _departments;}

        public HumanResourceManager()
        {
            _departments = new Department[0];
        }

        public void AddDepartment(string name, int workerLimit, double salaryLimit)
        {
            Department department = FindDepartmentByDepartmentName(name);

            if(department != null)
            {
                Console.WriteLine("Qeyd Etdiyiniz Department Movcuddur");
                return;
            }
            if((workerLimit * 250) <= salaryLimit)
            {
                department = new Department(name, workerLimit, salaryLimit);
                Array.Resize(ref _departments, _departments.Length + 1);
                _departments[_departments.Length - 1] = department;
                return;
            }
            else
            {
                Console.WriteLine($"SalaryLimit: {salaryLimit} TotalSalary-den boyuk ve ya Beraber Olmalidir");
            }
        }

        public Department[] GetDepartments()
        {
            return _departments;
        }

        public void EditDepartment(string name, string newName)
        {
            Department department = FindDepartmentByDepartmentName(name);
            if (department != null)
            {
                Department department1 = FindDepartmentByDepartmentName(newName);
                if(department1 != null)
                {
                    Console.WriteLine($"Daxil Etdiyiniz NewDepartmentName: {newName} Siyahida Movcuddur");
                    return;
                }
                department.Name = newName;
                return;
            }
            Console.WriteLine($"Daxil Edilen DepartmentName: {name} Siyahida Yoxdur");
        }


        public void AddEmployee(string fullName, string position, double salary, string departmentName)
        {     
            Department department = FindDepartmentByDepartmentName(departmentName);
            if(department != null)
            {
                if(department.WorkerLimit > department.Employees.Length && department.SalaryLimit > department.TotalSalary() + salary)
                {
                    Employee employee = new Employee(fullName, position, salary, departmentName);
                    Array.Resize(ref department.Employees, department.Employees.Length + 1);
                    department.Employees[department.Employees.Length - 1] = employee;
                    return;
                }
                else
                {
                    Console.WriteLine("Departmentde Yer Yoxdur ve ya SalaryLimit-i kecir");
                    return;
                }
            }
            Console.WriteLine($"Qeyd Etdiyiniz {departmentName} Siyahida Movcud Deyil");

        }

        public void RemoveEmployee(string employeeNo, string departmentName)
        {
            Department department = FindDepartmentByDepartmentName(departmentName);

            if(department != null)
            {
                Employee employee = FindEmployeeByEmployeeNo(employeeNo);
                if(employee != null)
                {
                    for (int i = 0; i < department.Employees.Length; i++)
                    {
                        if (department.Employees[i].No == employeeNo.ToUpper())
                        {
                            department.Employees[i] = department.Employees[department.Employees.Length - 1];
                            Array.Resize(ref department.Employees, department.Employees.Length - 1);
                            return;
                        }                       
                    }               
                }
                Console.WriteLine($"Daxil Etdiyiniz EmployeeNo {employeeNo} Adinda Employee Sistemde Yoxdur");
                return;
            }
            Console.WriteLine($"Daxil Etdiyiniz DepartmentName: {departmentName} Sistemde Yoxdur");
        }

        public void EditEmployee(string departmentName, string employeeNo, string fullName, double salary, string position)
        {
            Department department = FindDepartmentByDepartmentName(departmentName);
            if(department != null)
            {
                Employee employee = FindEmployeeByEmployeeNo(employeeNo);
                if(employee != null)
                {
                    if((department.TotalSalary() - employee.Salary) + salary <= department.SalaryLimit)
                    { 
                        employee.FullName = fullName;
                        employee.Salary = salary;
                        employee.Position = position;
                        return;
                    }
                    Console.WriteLine("Iscinin NewSalary-si DepartmentSalaryLimiti Asir");
                    return;
                }
                Console.WriteLine($"{employeeNo} Adli Employee Departmentde Yoxdur");
                return;
            }
            Console.WriteLine($"{departmentName} Adli Department Department Siyahisinda Yoxdur");
        }

        public Department FindDepartmentByDepartmentName(string departmentName)
        {
            foreach (Department department in _departments)
            {
                if(department.Name.ToUpper() == departmentName.ToUpper())
                {
                    return department;
                }
            }
            return null;
        }

        public Employee FindEmployeeByEmployeeNo(string employeeNo)
        {
            foreach (Department department in _departments)
            {
                foreach (Employee employee in department.Employees)
                {
                    if (employee.No == employeeNo.ToUpper())
                    {
                        return employee;
                    }
                }
            }
            return null;
        }

        public Employee[] GetAllEmployees()
        {
            Employee[] allEmployee = new Employee[0];

            foreach (Department department in _departments)
            {
                foreach (Employee employee in department.Employees)
                {
                    Array.Resize(ref allEmployee, allEmployee.Length + 1);
                    allEmployee[allEmployee.Length - 1] = employee;
                }
            }
            return allEmployee;
        }

        public Employee[] GetEmployeesByDepartmentName(string departmentName)
        {
            Employee[] allEmployeesByDepartmentName = new Employee[0];
            foreach (Department department in _departments)
            {                
                if(department.Name.ToUpper() == departmentName.ToUpper())
                {
                    allEmployeesByDepartmentName = department.Employees;
                }
            }
            return allEmployeesByDepartmentName;            
        }
    }
}
