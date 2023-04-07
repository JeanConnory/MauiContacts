﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contacts.Maui.Views_MVVM;
using Contacts.UseCases.Interfaces;
using System.Collections.ObjectModel;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Maui.ViewModels
{
	public partial class ContactsViewModel : ObservableObject
	{
		private readonly IViewContactsUseCase _viewContactsUseCase;
		private readonly IDeleteContactUseCase _deleteContactUseCase;

		public ObservableCollection<Contact> Contacts { get; set; }

        public ContactsViewModel(IViewContactsUseCase viewContactsUseCase, IDeleteContactUseCase deleteContactUseCase)
        {
			_viewContactsUseCase = viewContactsUseCase;
			_deleteContactUseCase = deleteContactUseCase;
			this.Contacts = new ObservableCollection<Contact>();
		}

		public async Task LoadContactAsync()
		{
			this.Contacts.Clear();

			var contacts = await _viewContactsUseCase.ExecuteAsync(null);

			if(contacts != null && contacts.Count > 0) 
			{
				foreach(var contact in contacts)
				{
					this.Contacts.Add(contact);
				}
			}
		}

		[RelayCommand]
		public async Task DeleteContact(int contactId)
		{
			await _deleteContactUseCase.ExecuteAsync(contactId);
			await LoadContactAsync();
		}

		[RelayCommand]
		public async Task GoToEditContact(int contactId)
		{
			await Shell.Current.GoToAsync($"{nameof(EditContactPage_MVVM)}?Id={contactId}");
		}
    }
}