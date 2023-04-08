using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contacts.Maui.Models;
using Contacts.Maui.Views_MVVM;
using Contacts.UseCases.Interfaces;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Maui.ViewModels
{
	public partial class ContactViewModel : ObservableObject
	{
        private Contact contact;
		private readonly IViewContactUseCase _viewContactUseCase;
		private readonly IEditContactUseCase _editContactUseCase;
		private readonly IAddContactUseCase _addContactUseCase;

		public Contact Contact 
        {
            get => contact;
            set
            {
                SetProperty(ref contact, value);
            }
        }

        public bool IsNameProvided { get; set; }

		public bool IsEmailProvided { get; set; }

		public bool IsEmailFormatValid { get; set; }

		public ContactViewModel(IViewContactUseCase viewContactUseCase, IEditContactUseCase editContactUseCase, IAddContactUseCase addContactUseCase)
        {
            this.Contact = new Contact();
			_viewContactUseCase = viewContactUseCase;
			_editContactUseCase = editContactUseCase;
			_addContactUseCase = addContactUseCase;
		}

        public async Task LoadContact(int contactId)
        {
            this.Contact = await _viewContactUseCase.ExecuteAsync(contactId);
        }

        [RelayCommand]
        public async Task EditContact()
        {
            if (await ValidateContact())
            {
                await _editContactUseCase.ExecuteAsync(this.contact.ContactId, this.contact);
                await Shell.Current.GoToAsync($"{nameof(Contacts_MVVM_Page)}");
            }
        }

        [RelayCommand]
        public async Task BackToContacts()
        {
            await Shell.Current.GoToAsync($"{nameof(Contacts_MVVM_Page)}");
        }

		[RelayCommand]
		public async Task AddContact()
		{
            if (await ValidateContact())
            {
                await _addContactUseCase.ExecuteAsync(this.contact);
                await Shell.Current.GoToAsync($"{nameof(Contacts_MVVM_Page)}");
            }
		}

        private async Task<bool> ValidateContact()
        {
            if(!this.IsNameProvided)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Name is required!", "OK");
                return false;
            }

			if (!this.IsEmailProvided)
			{
				await Application.Current.MainPage.DisplayAlert("Error", "Email is required!", "OK");
				return false;
			}

			if (!this.IsEmailFormatValid)
			{
				await Application.Current.MainPage.DisplayAlert("Error", "Email format incorrect!", "OK");
				return false;
			}

            return true;
		}
	}
}
 