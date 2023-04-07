using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contacts.Maui.Models;
using Contact = Contacts.Maui.Models.Contact;

namespace Contacts.Maui.ViewModels
{
	public partial class ContactViewModel : ObservableObject
	{
        private Contact contact;

		public Contact Contact 
        {
            get => contact;
            set
            {
                SetProperty(ref contact, value);
            }
        }

        public ContactViewModel()
        {
            this.Contact = ContactRepository.GetContactById(1);
        }

        [RelayCommand]
        public void SaveContact()
        {
            ContactRepository.UpdateContact(this.Contact.ContactId, this.Contact);
        }
    }
}
 