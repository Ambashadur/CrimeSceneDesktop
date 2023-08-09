using System;
using System.Collections.Generic;
using System.Linq;
using CrimeSceneDesktop.Contracts;
using Microsoft.Maui.Controls;

namespace CrimeSceneDesktop;

public partial class CommonPage : ContentPage
{
	private IEnumerable<Person> _persons = Enumerable.Empty<Person>();

	public CommonPage()
	{
		InitializeComponent();

		MainCollectionView.ItemsSource = GetPersonRecords();
		LogoutBtn.Clicked += (obj, args) => Shell.Current.GoToAsync("//mainPage");
	}

	private IEnumerable<Person> GetPersonRecords() {
		_persons = new List<Person>() {
			new() {
				Id = Random.Shared.Next(0, 100),
				FullName = "Ivan Ivanov Ivanovich",
				Start = DateTime.Now.AddHours(-1),
				End = DateTime.Now,
				Count= Random.Shared.Next(0, 10)
			}
		};

		return _persons;
	}
}