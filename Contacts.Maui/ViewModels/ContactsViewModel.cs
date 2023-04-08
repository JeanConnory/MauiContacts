using CommunityToolkit.Mvvm.ComponentModel;
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

		public ObservableCollection<Contacts.CoreBusiness.Contact> Contacts { get; set; }

		private string filterText;

		public string FilterText
		{
			get { return filterText; }
			set 
			{ 
				filterText = value;
				LoadContactAsync(filterText);
			}
		}


		public ContactsViewModel(IViewContactsUseCase viewContactsUseCase, IDeleteContactUseCase deleteContactUseCase)
        {
			_viewContactsUseCase = viewContactsUseCase;
			_deleteContactUseCase = deleteContactUseCase;
			this.Contacts = new ObservableCollection<Contacts.CoreBusiness.Contact>();
		}

		public async Task LoadContactAsync(string filterText = null)
		{
			this.Contacts.Clear();

			var contacts = await _viewContactsUseCase.ExecuteAsync(filterText);

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

		[RelayCommand]
		public async Task GoToAddContact()
		{
			await Shell.Current.GoToAsync(nameof(AddContactPage_MVVM));
		}
    }
}
