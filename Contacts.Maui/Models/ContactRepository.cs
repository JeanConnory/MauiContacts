using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Maui.Models
{
	public static class ContactRepository
	{
		public static List<Contact> _contacts = new List<Contact>()
		{
			new Contact { ContactId = 1, Name = "John Doe", Email = "johndoe@gmail.com"},
			new Contact { ContactId = 2, Name = "Jane Doe", Email = "janedoe@gmail.com"},
			new Contact { ContactId = 3, Name = "Tom Hanks", Email = "tomhanks@gmail.com"},
			new Contact { ContactId = 4, Name = "Frank Liu", Email = "frankliu@gmail.com"}
		};

		public static List<Contact> GetContacts() => _contacts;

		public static Contact GetContactById(int contactId)
		{
			var contact = _contacts.FirstOrDefault(x => x.ContactId == contactId);
			if (contact != null)
			{
				return new Contact
				{
					ContactId = contact.ContactId,
					Name = contact.Name,
					Email = contact.Email,
					Phone = contact.Phone,
					Address = contact.Address,
				};
			}

			return null;
		}

		public static void UpdateContact(int contactId, Contact contact)
		{
			if (contactId != contact.ContactId) return;

			var contactToUpdate = _contacts.FirstOrDefault(x => x.ContactId == contactId);
			if (contactToUpdate != null)
			{
				//AutoMapper
				contactToUpdate.Name = contact.Name;
				contactToUpdate.Email = contact.Email;
				contactToUpdate.Phone = contact.Phone;
				contactToUpdate.Address = contact.Address;
			}
		}

		public static void AddContact(Contact contact)
		{
			var maxId = _contacts.Max(x => x.ContactId);
			contact.ContactId = maxId + 1;
			_contacts.Add(contact);
		}

		public static void DeleteContact(int contactId)
		{
			var contact = _contacts.FirstOrDefault(x => x.ContactId == contactId);
			if (contact != null)
			{
				_contacts.Remove(contact);
			}
		}
	}
}
