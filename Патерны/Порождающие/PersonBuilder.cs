using System.Collections.Generic;
using BuilderDemo.Builders.Person.Interfaces;
using BuilderDemo.Interfaces;

namespace BuilderDemo.Builders.Person
{
    public class PersonBuilder :
        IFirstNameHolder, ILastNameHolder,
        IPrimaryContactHolder, ISecondaryContactHolder, IPersonBuilder
    {

        private string FirstName { get; set; }
        private string LastName { get; set; }
        private IList<IContactInfo> Contacts { get; set; } = new List<IContactInfo>();
        private IContactInfo PrimaryContact { get; set; }

        public static IFirstNameHolder Person() => new PersonBuilder();

        public ILastNameHolder WithFirstName(string firstName) =>
            new PersonBuilder()
            {
                FirstName = firstName
            };

        public bool IsValidFirstName(string name) => !string.IsNullOrEmpty(name);

        public IPrimaryContactHolder WithLastName(string lastName) =>
            new PersonBuilder()
            {
                FirstName = this.FirstName,
                LastName = lastName
            };

        public bool IsValidLastName(string surname) => !string.IsNullOrEmpty(surname);

        public ISecondaryContactHolder WithPrimaryContact(IContactInfo contact) =>
            new PersonBuilder()
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Contacts = new List<IContactInfo>(this.Contacts) { contact },
                PrimaryContact = contact
            };

        public ISecondaryContactHolder WithSecondaryContact(IContactInfo contact) =>
            new PersonBuilder()
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Contacts = new List<IContactInfo>(this.Contacts) { contact },
                PrimaryContact = this.PrimaryContact
            };

        public IPersonBuilder AndNoMoreContacts() => this;

        public bool Contains(IContactInfo contact) => this.Contacts.Contains(contact);

        public Models.Person Build()
        {

            Models.Person person = new Models.Person();

            person.Name = this.FirstName;
            person.Surname = this.LastName;

            foreach (IContactInfo contact in this.Contacts)
                person.ContactsList.Add(contact);

            person.PrimaryContact = this.PrimaryContact;

            return person;

        }

    }
}
