using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqConsoleApp
{
    public partial class Form1 : Form
    {
        public static IEnumerable<Emp> Emps { get; set; }
        public static IEnumerable<Dept> Depts { get; set; }

        public Form1()
        {
            InitializeComponent();
            LoadData();
        }


        private void LoadData()
        {
            var empsCol = new List<Emp>();
            var deptsCol = new List<Dept>();

            #region Load depts
            var d1 = new Dept
            {
                Deptno = 1,
                Dname = "Research",
                Loc = "Warsaw"
            };

            var d2 = new Dept
            {
                Deptno = 2,
                Dname = "Human Resources",
                Loc = "New York"
            };

            var d3 = new Dept
            {
                Deptno = 3,
                Dname = "IT",
                Loc = "Los Angeles"
            };

            deptsCol.Add(d1);
            deptsCol.Add(d2);
            deptsCol.Add(d3);
            Depts = deptsCol;
            #endregion

            #region Load emps
            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            empsCol.Add(e1);
            empsCol.Add(e2);
            empsCol.Add(e3);
            empsCol.Add(e4);
            empsCol.Add(e5);
            empsCol.Add(e6);
            empsCol.Add(e7);
            empsCol.Add(e8);
            empsCol.Add(e9);
            empsCol.Add(e10);
            Emps = empsCol;
            ResultsDataGridView.DataSource = Emps;

            #endregion

        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        /*
            Celem ćwiczenia jest uzupełnienie poniższych metod.
         *  Każda metoda powinna zawierać kod C#, który z pomocą LINQ'a będzie realizować
         *  zapytania opisane za pomocą SQL'a.
         *  Rezultat zapytania powinien zostać wyświetlony za pomocą kontrolki DataGrid.
         *  W tym celu końcowy wynik należy rzutować do Listy (metoda ToList()).
         *  Jeśli dane zapytanie zwraca pojedynczy wynik możemy je wyświetlić w kontrolce
         *  TextBox WynikTextBox.
        */

        /// <summary>
        /// SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        private void Przyklad1Button_Click(object sender, EventArgs e)
        {

            //1. Query syntax (SQL)
            var res = (from emp in Emps
                      where emp.Job == "Backend programmer"
                      select new
                      {
                          Nazwisko = emp.Ename,
                          Zawod = emp.Job
                      }).ToList();

            //2. Lambda and Extension methods

            var res2 = Emps.Where(emp => emp.Job == "Backend programmer").ToList();

            WynikTextBox.Clear();
            ResultsDataGridView.DataSource = res2;
        }

        /// <summary>
        /// SELECT * FROM Emps Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
        /// </summary>
        private void Przyklad2Button_Click(object sender, EventArgs e)
        {
            var res = (from emp in Emps
                       where emp.Job == "Frontend programmer" && emp.Salary > 1000
                       orderby emp.Ename descending
                       select new
                       {
                           Nazwisko = emp.Ename,
                           Zarobki = emp.Salary
                       }).ToList();

            //-------------------------------------

            var res2 = Emps
                .Where((emp, indx) => emp.Job == "Frontend programmer" && emp.Salary > 1000)
                .OrderByDescending(emp => emp.Ename)
                .Select(emp => new
                {
                    Nazwisko = emp.Ename,
                    Zarobki = emp.Salary
                }).ToList();

            WynikTextBox.Clear();
            ResultsDataGridView.DataSource = res2.ToList();
        }

        /// <summary>
        /// SELECT MAX(Salary) FROM Emps;
        /// </summary>
        private void Przyklad3Button_Click(object sender, EventArgs e)
        {
            var res2 = Emps.Max(emp => emp.Salary);

            WynikTextBox.Clear();
            WynikTextBox.AppendText(res2.ToString());
            ResultsDataGridView.DataSource = null;

        }

        /// <summary>
        /// SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
        /// </summary>
        private void Przyklad4Button_Click(object sender, EventArgs e)
        {
            var result = Emps
                            .Where(emp => emp.Salary == (Emps.Max(em => em.Salary)))
                            .Select(emp => emp).ToList();
            ResultsDataGridView.DataSource = result;
        }

        /// <summary>
        /// SELECT ename AS Nazwisko, job AS Praca FROM Emps;
        /// </summary>
        private void Przyklad5Button_Click(object sender, EventArgs e)
        {
            var result = Emps
                .OrderByDescending(emp => emp.Ename)
                .Select(emp => new
                {
                    Nazwisko = emp.Ename,
                    Praca = emp.Job
                }).ToList();

            WynikTextBox.Clear();
            ResultsDataGridView.DataSource = result;
        }

        /// <summary>
        /// SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
        /// INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
        /// Rezultat: Złączenie kolekcji Emps i Depts.
        /// </summary>
        private void Przyklad6Button_Click(object sender, EventArgs e)
        {
            var result = Emps
                .Join(Depts, emp => emp.Deptno, dept => dept.Deptno, (emp, dept) => new
                {
                    emp.Ename,
                    emp.Job,
                    dept.Dname
                }).ToList();


            WynikTextBox.Clear();
            ResultsDataGridView.DataSource = result;
        }

        /// <summary>
        /// SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
        /// </summary>
        private void Przyklad7Button_Click(object sender, EventArgs e)
        {
            var result = Emps
                .GroupBy(emp => emp.Job)
                .Select(emp => new
                {
                    Praca = emp.Key,
                    LiczbaPracownikow = emp.Count()
                }).ToList();

            WynikTextBox.Clear();
            ResultsDataGridView.DataSource = result;
        }

        /// <summary>
        /// Zwróć wartość "true" jeśli choć jeden
        /// z elementów kolekcji pracuje jako "Backend programmer".
        /// </summary>
        private void Przyklad8Button_Click(object sender, EventArgs e)
        {
            var result = Emps
                .Any(emp => emp.Job == "Backend programmer")
                .ToString();


            WynikTextBox.Clear();
            WynikTextBox.AppendText(result);
            ResultsDataGridView.DataSource = null;
        }

        /// <summary>
        /// SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
        /// ORDER BY HireDate DESC;
        /// </summary>
        private void Przyklad9Button_Click(object sender, EventArgs e)
        {
            var result = Emps
                .Where(emp => emp.Job == "Frontend programmer")
                .OrderByDescending(emp => emp.HireDate)
                .Select(emp => emp)
                .FirstOrDefault()
                .ToString();


            WynikTextBox.Clear();
            WynikTextBox.AppendText(result);
            ResultsDataGridView.DataSource = null;
        }

        /// <summary>
        /// SELECT Ename, Job, Hiredate FROM Emps
        /// UNION
        /// SELECT "Brak wartości", null, null;
        /// </summary>
        private void Przyklad10Button_Click(object sender, EventArgs e)
        {
            var result = Emps
                .Select(emp => new
                {
                    Ename = emp.Ename,
                    Job = emp.Job,
                    Hiredate = emp.HireDate
                })
                .Union(Emps
                    .Select(n => new
                    {
                        Ename= "Brak wartości",
                        Job = (string) null,
                        Hiredate = (DateTime?)null
                    }))
                .ToList();

            WynikTextBox.Clear();
            ResultsDataGridView.DataSource = result;
        }

        //Znajdź pracownika z najwyższą pensją wykorzystując metodę Aggregate()
        private void Przyklad11Button_Click(object sender, EventArgs e)
        {
            var result = Emps
               .Aggregate((e1, e2) => e1.Salary > e2.Salary ? e2 : e1)
               .ToString();

            WynikTextBox.Clear();
            WynikTextBox.AppendText(result);
            ResultsDataGridView.DataSource = null;
        }

        //Z pomocą języka LINQ i metody SelectMany wykonaj złączenie
        //typu CROSS JOIN
        private void Przyklad12Button_Click(object sender, EventArgs e)
        {
            var result = Emps
                .SelectMany(emp => Depts, (emp, dept) => new
                {
                    emp.Ename,
                    dept.Dname
                })
                .ToList();

            WynikTextBox.Clear();
            ResultsDataGridView.DataSource = result;
        }


    }
}
