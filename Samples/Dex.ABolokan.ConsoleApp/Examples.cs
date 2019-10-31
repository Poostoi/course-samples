﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using Dex.ABolokan.Services.IComparableTest;
using Dex.ABolokan.Services.IEnumerableTask;
using Dex.ABolokan.Services.Interfaces;
using Dex.ABolokan.Services.Linq;
using Dex.ABolokan.Services.ObjectEquals;
using Dex.ABolokan.Services.Services;
using PersonSimple = Dex.ABolokan.Services.IEnumerableTask.Person;

namespace Dex.ABolokan.ConsoleApp
{
	public class Examples : ExampleBase
	{
		/// <summary>
		/// 3. ООП - объектно ориентированный подход
		/// </summary>
		public static void OopTest()
		{
			Start();

			var reportIds = new[] { 1, 2, 3, 4, 5 };

			var reportServices = new List<IReportService> { new ExcelReportService(), new PdfReportService() };

			foreach (var reportId in reportIds)
			{
				foreach (var reportService in reportServices)
				{
					var data = reportService.GetReportData(reportId);
					reportService.SaveReportData(data);
				}
				Line();
			}

			Finish();
		}

		/// <summary>
		/// 7. Базовые интерфейсы для работы со списками
		/// </summary>
		public static void EnumerableTest()
		{
			PersonSimple[] peopleArray = new PersonSimple[3]
			{
				new PersonSimple("John", "Smith"),
				new PersonSimple("Jim", "Johnson"),
				new PersonSimple("Sue", "Rabon"),
			};

			People peopleList = new People(peopleArray);

			foreach (PersonSimple p in peopleList)
				Console.WriteLine(p.FirstName + " " + p.LastName);
		}

		/// <summary>
		/// 8. Queryable - Linq расширения
		/// </summary>
		public static void QueryableTest()
		{
			var service = new ProductService();

			Console.WriteLine("\nИсходный список");

			var products = service.GetAll();
			foreach (var product in products)
			{
				Console.WriteLine(product.ToString());
			}

			Console.WriteLine("\nTest Where is a new product");

			var filteredList = products.Where(p => p.IsNew && p.Count < 500).ToList();
			foreach (var product in filteredList)
			{
				Console.WriteLine(product.ToString());
			}

			Console.WriteLine("\nTest order by Count");

			var orderedList = products.OrderBy(p => p.Count).ToList();
			foreach (var product in orderedList)
			{
				Console.WriteLine(product.ToString());
			}

			Console.WriteLine("\nTest group by TypeId");

			var groupedList = products.GroupBy(p => p.TypeId).ToList();
			foreach (var groupProducts in groupedList)
			{
				Console.WriteLine($"Group: {groupProducts.Key}");
				foreach (var product in groupProducts)
				{
					Console.WriteLine(product.ToString());
				}

			}

			Console.WriteLine($"\nThe list has new product: {products.Any(p => p.IsNew)}");
			Console.WriteLine($"\nAll products are new: {products.All(p => p.IsNew)}");


			Console.WriteLine("Total amount: " + products.Sum(p => p.Count));
			Console.WriteLine("Min count: " + products.Min(p => p.Count));
			Console.WriteLine("Max amount: " + products.Max(p => p.Count));
		}

		/// <summary>
		/// 9 IComparable
		/// </summary>
		public static void ComparableTest()
		{
			var circlesList = new List<Circle>();
			Random random = new Random();

			for (int i = 0; i < 10; i++)
			{
				circlesList.Add(new Circle(random.Next(10, 1000)));
			}

			Console.WriteLine("Исходный список");
			foreach (var circle in circlesList)
			{
				Console.WriteLine(circle.ToString());
			}

			var circles = circlesList.ToArray();

			Array.Sort(circles);

			Console.WriteLine("Сортировка по списка по площади(IComparable)");
			foreach (var circle in circles)
			{
				Console.WriteLine(circle.ToString());
			}

			Console.WriteLine("Сортировка по списка по радиусу(IComparer)");

			Array.Sort(circles, new RadiusComparer());

			foreach (var circle in circles)
			{
				Console.WriteLine(circle.ToString());
			}
		}

		/// <summary>
		/// Сравнение ссылочных объектов
		/// </summary>
		public static void PersonEqualTest()
		{
			var person1 = new Dex.ABolokan.Services.ObjectEquals.Person
			{
				FirstName = "Ivan",
				LastName = "Ivanov",
				MidleName = "Ivanko",
				Dob = new DateTime(1989, 01, 11),
				PassportId = "Z123456789",
				Place = "Типасполь"
			};
			var person2 = new Dex.ABolokan.Services.ObjectEquals.Person
			{
				FirstName = "Ivan",
				LastName = "Ivanov",
				MidleName = "Ivanko",
				Dob = new DateTime(1989, 01, 11),
				PassportId = "Z123456789",
				Place = "Типасполь"
			};

			var person3 = new Dex.ABolokan.Services.ObjectEquals.Person
			{
				FirstName = "Ivan",
				LastName = "Ivanov",
				MidleName = "Ivan'ko",
				Dob = new DateTime(1989, 01, 11),
				PassportId = "Z123456789",
				Place = "Типасполь"
			};

			Console.WriteLine($"Ожидаю(True):{person1.Equals(person2)} ");
			Console.WriteLine($"Ожидаю(False):{person1.Equals(person3)} ");
			Console.WriteLine($"Ожидаю(False):{person2.Equals(person3)} ");

			Console.WriteLine("\n Hash Codes:");
			Console.WriteLine($"Person 1 : {person1.GetHashCode()}");
			Console.WriteLine($"Person 2 : {person2.GetHashCode()}");
			Console.WriteLine($"Person 3 : {person3.GetHashCode()}");
		}

		/// <summary>
		/// Dictionary test
		/// </summary>
		public static void ListTest()
		{
			const string dateFormat = "dd MM yyyy";
			var dob = new DateTime(1989, 01, 11);
			var personDictionary = new Dictionary<(string, string, string, DateTime, string, string), string>
			{
				{ ("Ivanov", "Ivan", "Ivanko", dob, "Tiraspol", "123456"), "Tiraspol" },
				{ ("Ivanova", "Anna", "Anya", dob, "Tiraspol", "678900"), "Moskow" },
			};

			Console.Write("Введите Фамилию Имя Отчество:");

			string firstName = string.Empty;
			string lastName = string.Empty;
			string midleName = string.Empty;

			var fio = Console.ReadLine();
			if (!string.IsNullOrWhiteSpace(fio))
			{
				var mas = fio.Split(' ');
				firstName = mas[0];
				lastName = mas.Length > 1 ? mas[1] : string.Empty;
				midleName = mas.Length > 2 ? mas[2] : string.Empty;
			}

			Console.Write($"Введите дату рождения({dateFormat}): ");
			DateTime.TryParseExact(Console.ReadLine(), dateFormat, null, DateTimeStyles.None, out DateTime dateDob);

			Console.Write("Введите место рождения:");
			var place = Console.ReadLine();

			Console.Write("Введите номер пасорта:");
			var passportId = Console.ReadLine();

			var key = (firstName, lastName, midleName, dateDob, place, passportId);

			if (personDictionary.ContainsKey(key))
			{
				var result = personDictionary[key];
				Console.WriteLine($"Проживает в {result}");
			}
			else
			{
				Console.WriteLine("Адрес проживания не найден");
			}
		}

	}

	public class ExampleBase
	{
		public static void Start()
		{
			Console.WriteLine("Start! \n");
		}

		public static void Line()
		{
			Console.WriteLine(new string('-', 25));
		}

		public static void Finish()
		{
			Console.WriteLine("\n Finish!");
		}
	}

}
