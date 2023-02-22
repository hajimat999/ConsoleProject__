using System;
using ConsoleProject.Services;
using ConsoleProject.Models;

namespace ConsoleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            HumanResourceManager humanResourceManager = new HumanResourceManager();
            do
            {
                Console.WriteLine("=====Welcome Course Management=====\n");
                Console.WriteLine("Etmek Istediyiniz Emeliyyatin Reqemini Secin\n");
                Console.WriteLine("1.  Get Departments");
                Console.WriteLine("2.  Add Department");
                Console.WriteLine("3.  Edit Department");
                Console.WriteLine("4.  Get All Employees");
                Console.WriteLine("5.  Get EmployeeByDepartment");
                Console.WriteLine("6.  Add Employee");
                Console.WriteLine("7.  Edit Employee");
                Console.WriteLine("8.  Remove Employee");
                Console.WriteLine("9.  Sistemden Cix");

                string choose = Console.ReadLine();
                int chooseNum;
                while (!int.TryParse(choose, out chooseNum) || chooseNum < 1 || chooseNum > 9)
                {
                    Console.WriteLine("Duzgun Reqem Daxil Edin");
                    choose = Console.ReadLine();
                }

                switch (chooseNum)
                {
                    case 1:
                        GetDepartments(ref humanResourceManager);
                        break;
                    case 2:
                        AddDepartment(ref humanResourceManager);
                        break;
                    case 3:
                        EditDepartment(ref humanResourceManager);
                        break;
                    case 4:
                        GetAllEmployees(ref humanResourceManager);
                        break;
                    case 5:
                        GetEmployeesByDepartmentName(ref humanResourceManager);
                        break;
                    case 6:
                        AddEmployee(ref humanResourceManager);
                        break;
                    case 7:
                        EditEmployee(ref humanResourceManager);
                        break;
                    case 8:
                        RemoveEmployee(ref humanResourceManager);
                        break;
                    case 9:
                        return;
                }

            } while (true);

        }
            
        static void GetDepartments(ref HumanResourceManager humanResourceManager)
        {
            Console.Clear();
            Console.WriteLine("Departmentlerin Siyahisi");
            foreach (Department department in humanResourceManager.GetDepartments())
            {
                Console.WriteLine($"DepartmentName: {department.Name} WorkerCount: {department.Employees.Length} WorkerAverageSalary: {department.CalcSalaryAverage()}");
            }
        }

        static void AddDepartment(ref HumanResourceManager humanResourceManager)
        {
            Console.Clear();
            Console.WriteLine("Department Adini Daxil Et");
            string departmentName = Console.ReadLine();
            while (!(departmentName.Length >= 2) || !char.IsLetter(departmentName[0]) || !char.IsLetter(departmentName[1]))
            {
                Console.WriteLine("Duzgun DepartmentName Daxil Edin");
                departmentName = Console.ReadLine();
            }
            Console.WriteLine("Department WorkerLimiti Daxil Edin");
            string workerLimit = Console.ReadLine();
            int workerLimitNum;
            while(!int.TryParse(workerLimit, out workerLimitNum) || workerLimitNum < 1)
            {
                Console.WriteLine("Duzgun WorkerLimit Daxil Edin");
                workerLimit = Console.ReadLine();
            }
            Console.WriteLine("Department SalaryLimiti Daxil Et");
            string salaryLimit = Console.ReadLine();
            double salaryLimitNum;
            while(!double.TryParse(salaryLimit, out salaryLimitNum) || salaryLimitNum < 250)
            {
                Console.WriteLine("Duzgun SalaryLimit Daxil Edin");
                salaryLimit = Console.ReadLine();
            }
            humanResourceManager.AddDepartment(departmentName, workerLimitNum, salaryLimitNum);          
        }

        static void EditDepartment(ref HumanResourceManager humanResourceManager)
        {
            Console.Clear();
            foreach (Department department in humanResourceManager.GetDepartments())
            {
                Console.WriteLine(department);
            }
            Console.WriteLine("Editlemek Istediyiniz Departmentin Adini Daxil Et");
            string departmentName = Console.ReadLine();
            while (!(departmentName.Length >= 2) || !char.IsLetter(departmentName[0]) || !char.IsLetter(departmentName[1]))
            {
                Console.WriteLine("Duzgun DepartmentName Daxil Edin");
                departmentName = Console.ReadLine();
            }

            if(humanResourceManager.FindDepartmentByDepartmentName(departmentName) != null)
            {
                Console.WriteLine("Department Haqqinda Melumatlar");
                Console.WriteLine($"Name: {humanResourceManager.FindDepartmentByDepartmentName(departmentName).Name}");
                Console.WriteLine($"SalaryLimit: {humanResourceManager.FindDepartmentByDepartmentName(departmentName).SalaryLimit}");
                Console.WriteLine($"WorkerLimit: {humanResourceManager.FindDepartmentByDepartmentName(departmentName).WorkerLimit}");           
                Console.WriteLine("Department Ucun Yeni Adi Daxil ET");
                string newDepartmentName = Console.ReadLine();
                while (!(newDepartmentName.Length >= 2) || !char.IsLetter(newDepartmentName[0]) || !char.IsLetter(newDepartmentName[1]))
                {
                    Console.WriteLine("Duzgun DepartmentName Daxil Edin");
                    newDepartmentName = Console.ReadLine();
                }
                humanResourceManager.EditDepartment(departmentName, newDepartmentName);
            }
            else
            {
                Console.WriteLine($"Qeyd Etdiyiniz DepartmentName: {departmentName} Siyahida Yoxdur");             
            }           
        }

        static void GetAllEmployees(ref HumanResourceManager humanResourceManager)
        {
            Console.Clear();
            Console.WriteLine("Sistemdeki Butun Iscilerin Siyahisi");
            foreach (Employee employee in humanResourceManager.GetAllEmployees())
            {
                Console.WriteLine(employee);
            }
        }

        static void GetEmployeesByDepartmentName(ref HumanResourceManager humanResourceManager)
        {
            Console.Clear();
            Console.WriteLine("Siyahidan DepartmentName Secin");
            foreach (Department department in humanResourceManager.GetDepartments())
            {
                Console.WriteLine(department);
            }
            string departmentName = Console.ReadLine();
            while (!(departmentName.Length >= 2) || !char.IsLetter(departmentName[0]) || !char.IsLetter(departmentName[1]))
            {
                Console.WriteLine("Duzgun DepartmentName Daxil Edin");
                departmentName = Console.ReadLine();
            }
            if(humanResourceManager.FindDepartmentByDepartmentName(departmentName) == null)
            {
                Console.WriteLine($"Sistemde {departmentName} Adinda Department Yoxdur");
                return;
            }
            Console.WriteLine("Telebelerin Siyahisi");
            foreach (Employee employee in humanResourceManager.GetEmployeesByDepartmentName(departmentName))
            {
                Console.WriteLine(employee);
            }
        }

        static void AddEmployee(ref HumanResourceManager humanResourceManager)
        {
            Console.Clear();
            Console.WriteLine("Employee Ad ve Soyad Elave Edin");
            string employeeFullName = Console.ReadLine();
            while(!(employeeFullName.Split(' ').Length >= 2) || string.IsNullOrWhiteSpace(employeeFullName))
            {
                Console.WriteLine("Duzgun FullName Daxil Edin");
                employeeFullName = Console.ReadLine();
            }
            Console.WriteLine("Employee Position Elave Edin");
            string employeePosition = Console.ReadLine();
            while (!char.IsLetter(employeePosition[0]) || !char.IsLetter(employeePosition[1]) || !(employeePosition.Length >=2))
            {
                Console.WriteLine("Duzgun Position Elave Edin");
                employeePosition = Console.ReadLine();
            }
            Console.WriteLine("Salary Elave Edin");
            string employeeSalary = Console.ReadLine();
            double employeeSalaryNum;
            while (!double.TryParse(employeeSalary, out employeeSalaryNum) || employeeSalaryNum < 250)
            {
                Console.WriteLine("Duzgun Salary Daxil Edin");
                employeeSalary = Console.ReadLine();
            }
            Console.WriteLine("Employee Departmente Elave Etmek Ucun DepartmentName Secin");
            foreach (Department department in humanResourceManager.GetDepartments())
            {
                Console.WriteLine(department);
            }
            string employeeDepartmentName = Console.ReadLine();
            while (!(employeeDepartmentName.Length >= 2) || !char.IsLetter(employeeDepartmentName[0]) || !char.IsLetter(employeeDepartmentName[1]))
            {
                Console.WriteLine("Duzgun DepartmentName Daxil Edin");
                employeeDepartmentName = Console.ReadLine();
            }
            humanResourceManager.AddEmployee(employeeFullName, employeePosition, employeeSalaryNum, employeeDepartmentName);
        }

        static void EditEmployee(ref HumanResourceManager humanResourceManager)
        {
            Console.Clear();
            Console.WriteLine("Edit Etmek Istediyiniz Employee-nin DepartmentName Daxil Edin");
            string employeeDepartmentName = Console.ReadLine();
            while (!(employeeDepartmentName.Length >= 2) || !char.IsLetter(employeeDepartmentName[0]) || !char.IsLetter(employeeDepartmentName[1]))
            {
                Console.WriteLine("Duzgun DepartmentName Daxil Edin");
                employeeDepartmentName = Console.ReadLine();
            }
            if(humanResourceManager.FindDepartmentByDepartmentName(employeeDepartmentName) == null)
            {
                Console.WriteLine($"Daxil Edilen DepartmentName: {employeeDepartmentName} Adinda Department Yoxdur");
                return;
            }
            foreach (Employee employee in humanResourceManager.GetEmployeesByDepartmentName(employeeDepartmentName))
            {
                Console.WriteLine(employee);
            }
            Console.WriteLine("Employee Siyahisindan Edit Etmek Istediyiniz Employee-nin EmployeeNo-sunu Daxil Edin");
            string employeeNo = Console.ReadLine();
            while (employeeNo.Length != 6 || string.IsNullOrWhiteSpace(employeeNo))
            {
                Console.WriteLine("Duzgun EmployeeNo Daxil Edin");
                employeeNo = Console.ReadLine();
            }
            if (humanResourceManager.FindEmployeeByEmployeeNo(employeeNo) == null)
            {
                Console.WriteLine("Qeyd Etdiyiniz EmployeeNo Adinda Employee Departmentde Yoxdur");
                return;
            }
            Console.WriteLine("Employee Haqqinda Melumatlar");
            Console.WriteLine($"EmployeeFullName: {humanResourceManager.FindEmployeeByEmployeeNo(employeeNo).FullName}");
            Console.WriteLine($"Salary: {humanResourceManager.FindEmployeeByEmployeeNo(employeeNo).Salary}");
            Console.WriteLine($"Position: {humanResourceManager.FindEmployeeByEmployeeNo(employeeNo).Position}");
            Console.WriteLine($"DepartmentName: {humanResourceManager.FindEmployeeByEmployeeNo(employeeNo).DepartmentName}");
            string employeeFullName = humanResourceManager.FindEmployeeByEmployeeNo(employeeNo).FullName;
            Console.WriteLine("Employee Ucun New Position Daxil Edin");
            string newPosition = Console.ReadLine();
            while (!char.IsLetter(newPosition[0]) || !char.IsLetter(newPosition[1]) || !(newPosition.Length >= 2))
            {
                Console.WriteLine("Duzgun Position Elave Edin");
                newPosition = Console.ReadLine();
            }
            Console.WriteLine("Employee Ucun New Salary Daxil Edin");
            string newSalary = Console.ReadLine();
            double newSalaryNum;
            while (!double.TryParse(newSalary, out newSalaryNum) || newSalaryNum < 250)
            {
                Console.WriteLine("Duzgun Salary Daxil Edin");
                newSalary = Console.ReadLine();
            }
            humanResourceManager.EditEmployee(employeeDepartmentName, employeeNo, employeeFullName, newSalaryNum, newPosition);
        }

        static void RemoveEmployee(ref HumanResourceManager humanResourceManager)
        {
            Console.Clear();
            Console.WriteLine("Department Siyahisindan DepartmentName Secin");
            foreach (Department department in humanResourceManager.GetDepartments())
            {
                Console.WriteLine(department);
            }
            string departmentName = Console.ReadLine();
            while (!(departmentName.Length >= 2) || !char.IsLetter(departmentName[0]) || !char.IsLetter(departmentName[1]))
            {
                Console.WriteLine("Duzgun DepartmentName Daxil Edin");
                departmentName = Console.ReadLine();
            }
            if(humanResourceManager.FindDepartmentByDepartmentName(departmentName) == null)
            {
                Console.WriteLine($"Daxil Edilen {departmentName} Adinda Department Sistemde Yoxdur");
                return;
            }
            Console.WriteLine("Employee Sirasindan EmployeeNo Secin");
            foreach (Employee employee in humanResourceManager.GetEmployeesByDepartmentName(departmentName))
            {
                Console.WriteLine(employee);
            }
            Console.WriteLine("Silmek Istediyiniz EmployeeNo Secin");
            string employeeNo = Console.ReadLine();
            while (employeeNo.Length != 6 || string.IsNullOrWhiteSpace(employeeNo))
            {
                Console.WriteLine("Duzgun GroupNo Daxil Edin");
                employeeNo = Console.ReadLine();

            }
            if(humanResourceManager.FindEmployeeByEmployeeNo(employeeNo) == null)
            {
                Console.WriteLine($"Sistemde {employeeNo} Adinda Employee Yoxdur");
                return;
            }
            humanResourceManager.RemoveEmployee(employeeNo, departmentName);
        }
    }
}








