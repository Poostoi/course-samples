﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Topic_19_Regular_expression_
{
	class Program
	{
		static void Main(string[] args)
		{			
			string clientInformation = "Петров Сергей Генадиевич, тел.: +37377951206, Женат, счет: 100.23.1000, сумма: 1 000 000";
						
			Console.WriteLine($"Исследуем данные клиента: {clientInformation}");
			Console.WriteLine("_ _");
			var Customer = CustomerResearch(clientInformation);			

			foreach (var item in Customer)
			{
				Console.WriteLine($"{item.Key} = {item.Value}");
			}

			Console.WriteLine("_ _");
			Console.WriteLine("Производим замену семейного положения клиента");
			ChangeMaritalStatus(clientInformation);	

		}
		public static Dictionary<string, string> CustomerResearch(string clientInformation)
		{
			//Исследуем данные клиента
			var Client = new Dictionary<string, string>();
			var regexSurname = new Regex(@"\w*ов");
			var regexName = new Regex(@"(?<=ов)\s\w*");
			var regexPatronymic = new Regex(@"(\w*вич)|(\w*вна)");
			var regexFonNumber = new Regex(@"[7]{2}[0-9]{6}");
			var regexMaritalStatus = new Regex(@"Женат|Холост", RegexOptions.IgnoreCase);
			var regexAccount = new Regex(@"\d{3}\.\d{2}\.\d{3}");
			var regexSum = new Regex(@"([0-9]\d*\s[0-9]{3}\s[0-9]{3})");		
			
			Client.Add("Surname", regexSurname.Match(clientInformation).ToString());
			Client.Add("Name", regexName.Match(clientInformation).ToString());
			Client.Add("Patronymic", regexPatronymic.Match(clientInformation).ToString());
			Client.Add("FonNumber", regexFonNumber.Match(clientInformation).ToString());
			Client.Add("MaritalStatus", regexMaritalStatus.Match(clientInformation).ToString());
			Client.Add("Account", regexAccount.Match(clientInformation).ToString());
			Client.Add("Sum", regexSum.Match(clientInformation).ToString());

			return Client;
		}
		public static void ChangeMaritalStatus(string clientInformation)
		{
			//Производим замену семейного положения клиента
			string pattern = @"женат";
			string targetMaritalStatus = "Холост";

			var regex = new Regex(pattern, RegexOptions.IgnoreCase);	
			
			Console.WriteLine(regex.Replace(clientInformation, targetMaritalStatus));
		}
	}
}
