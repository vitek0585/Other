using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using Moq;
using NUnit.Framework;
using Repository.Interfaces;
using DynamicExpression = System.Linq.Dynamic.DynamicExpression;
namespace TestNUnit
{
    [TestFixture]
    public class TestMVC
    {
        private IEnumerable<Employee> _employees;

        #region Setup

        [TestFixtureSetUp]
        public void Setup()
        {
            _employees = new List<Employee>()
            {
                new Employee()
                {
                    Id = 1,
                    DateBirthday = new DateTime(1990, 02, 05),
                    FirstName = "Vitek",
                    LastName = "Ivanov",
                    INN = "1234355",
                    EmpPromotion = new List<EmpPromotion>()
                },
                new Employee()
                {
                    Id = 2,
                    DateBirthday = new DateTime(1995, 06, 16),
                    FirstName = "Dmitry",
                    LastName = "Petrov",
                    INN = "09585774",
                    EmpPromotion = new List<EmpPromotion>()
                },
                new Employee()
                {
                    Id = 3,
                    DateBirthday = new DateTime(1978, 07, 10),
                    FirstName = "Sanek",
                    LastName = "Deveev",
                    INN = "098447",
                    EmpPromotion = new List<EmpPromotion>()
                },
                new Employee()
                {
                    Id = 4,
                    DateBirthday = new DateTime(1989, 05, 01),
                    FirstName = "Dmitry",
                    LastName = "Petrov",
                    INN = "09585774",
                    EmpPromotion = new List<EmpPromotion>()
                },
                new Employee()
                {
                    Id = 5,
                    DateBirthday = new DateTime(1995, 12, 25),
                    FirstName = "Victorya",
                    LastName = "Sheveleva",
                    INN = "7563893",
                    EmpPromotion = new List<EmpPromotion>()
                }
            };
        }

        #endregion
        [Test]
        public void TestByID()
        {
            var repositoryMock = new Mock<IEmployeeRepository>();

            //Setup mock that will return Region list when called:
            repositoryMock.Setup(x => x.GetAll()).Returns(_employees);
            var allEmployee = new RepositoryTest(repositoryMock.Object);
            //public Region GetById(int id)
            var employee = allEmployee.GetById(1);

            //Assert
            Assert.IsNotNull(employee); // Test if null
#pragma warning disable 612
            Assert.IsInstanceOfType(typeof(Employee), employee);
#pragma warning restore 612


        }
        [Test]
        public void TestExpression(string lastName = "o", string firstName = "a")
        {
            Expression<Func<string>> nameF = () =>lastName;
            ParameterExpression p = Expression.Parameter(typeof(Employee),"p");
            MemberExpression pm = Expression.Property(p, nameF.Name);

        }
        //Expression<Func<string>> bodyL = () => LastName;
        //MemberInfo nameL = (bodyL.Body as MemberExpression).Member;

        //Expression<Func<string>> bodyF = () => FirstName;
        //MemberInfo nameF = (bodyF.Body as MemberExpression).Member;

        //var q = from ch in new Expression<Func<MemberInfo>>[] { () => nameL, () => nameF }
        //        select predicate.Or(DynamicExpression.ParseLambda(new[] {predicate.Parameters},))
        [Test]
        public void TestMethod_Get_Region_By_Predicate()
        {
            var repositoryMock = new Mock<IEmployeeRepository>();
            repositoryMock.Setup(x => x.GetAll()).Returns(_employees);
            var allRegion = new RepositoryTest(repositoryMock.Object);

            string[] keywords = new string[2] { "o", "a" };

            Expression<Func<Employee, bool>> predicate = PredicateBuilder.False<Employee>();
            ParameterExpression it = Expression.Parameter(typeof(string), "fn");
            
            string str = String.Format("it.{0}.Contains(@0)","FirstName");
            string str1 = String.Format("it.{0}.Contains(@0)","LastName");

            var e = DynamicExpression.ParseLambda<Employee, bool>(str, "a");
            var e1 = DynamicExpression.ParseLambda<Employee, bool>(str1, "a");
            Expression<Func<Employee, bool>>  predicate1 = e.And(e1);
            //foreach (string keyword in keywords)
            //{
            //    string temp = keyword;
            //    predicate = predicate.Or(p => p.LastName.Contains(temp));
            //}
            //IEnumerable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
            IEnumerable<Employee> listRegion = allRegion.FindBy(predicate1);
            int count = listRegion.Count();

            //Assert
            Assert.IsNotNull(listRegion); // Test if null
            Assert.IsInstanceOfType(typeof(IEnumerable<Employee>), listRegion);
            Assert.AreEqual(1,count);
        }

    }
    class RepositoryTest
    {
        private readonly IEmployeeRepository _employeeRepository;

        public RepositoryTest(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }
        public IEnumerable<Employee> FindBy(Expression<Func<Employee, bool>> predicate)
        {
            var employees = _employeeRepository.GetAll().AsQueryable();
            var query = employees.Where(predicate);
            return query;
        }
        public Employee GetById(int id)
        {
            var employees = _employeeRepository.GetAll();
            return employees.Where(p => p.Id == id).FirstOrDefault();
        }

    }
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }


}
