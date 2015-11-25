using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shiloh.BL;
using Shiloh.BL.ShilohTableAdapters;

[System.ComponentModel.DataObject]
public class Person {
    private personTableAdapter personAdapter = null;
    protected personTableAdapter Adapter {
        get {
            if (personAdapter == null)
                personAdapter = new personTableAdapter();

            return personAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public Shiloh.BL.Shiloh.personDataTable GetPerson()
    {
        return Adapter.GetPerson();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public Shiloh.BL.Shiloh.personDataTable GetPersonByID(int ID)
    {
        return Adapter.GetPersonByID(ID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public Shiloh.BL.Shiloh.personDataTable GetPersonByName(String surname, String givenName)
    {
        return Adapter.GetPersonByName(givenName, surname);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public bool AddPerson(string givenName, string surname, string title, string familiarName, string sex, 
        string telephone, string emailAddress, string streetAddress, string city, string state, string zipCode,
        string login, string password, string photoPath) {

        // Create a new PersonRow instance
            Shiloh.BL.Shiloh.personDataTable person = new Shiloh.BL.Shiloh.personDataTable();
            Shiloh.BL.Shiloh.personRow aPerson = person.NewpersonRow();

        aPerson.givenName = givenName;
        aPerson.surname = surname;
        aPerson.title = title;
        aPerson.familiarName = familiarName;
        aPerson.sex = sex;
        aPerson.telephone = validateTelephone(telephone);
        aPerson.emailAddress = emailAddress;
        aPerson.streetAddress = streetAddress;
        aPerson.city = city;
        aPerson.state = state;
        aPerson.zipCode = validateZipCode(zipCode);
        aPerson.login = login;
        aPerson.password = password;
        aPerson.photoPath = photoPath;

        // Add the new community
        person.AddpersonRow(aPerson);
        int rowsAffected = Adapter.Update(person);

        // Return true if precisely one row was inserted, otherwise false
        return rowsAffected == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
    public bool UpdatePerson(int ID, string givenName, string surname, string title, string familiarName, string sex,
        string telephone, string extension, string emailAddress, string streetAddress, string city, string state, string zipCode,
        string password, string photoPath, string status)
    {
        Shiloh.BL.Shiloh.personDataTable person = Adapter.GetPersonByID(ID);
        if (person.Count == 0)
            // no matching record found, return false
            return false;

        Shiloh.BL.Shiloh.personRow aPerson = person[0];

        aPerson.givenName = givenName;
        aPerson.surname = surname;
        aPerson.title = title;
        aPerson.familiarName = familiarName;
        aPerson.sex = sex;
        aPerson.telephone = validateTelephone(telephone);
        aPerson.emailAddress = emailAddress;
        aPerson.streetAddress = streetAddress;
        aPerson.city = city;
        aPerson.state = state;
        aPerson.zipCode = validateZipCode(zipCode);
        aPerson.password = password;
        aPerson.photoPath = photoPath;

        // Update the person record
        int rowsAffected = Adapter.Update(aPerson);

        // Return true if precisely one row was updated, otherwise false
        return rowsAffected == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public bool DeletePersonByID(int ID)
    {
        int rowsAffected = Adapter.DeletePersonByID(ID);

        // TODO: Archive all membership entries for the person
        // Return true if precisely one row was deleted, otherwise false
        return rowsAffected == 1;
    }

    static internal decimal validateTelephone(string telephone) {
        decimal telephoneDecimal = 0;
        StringBuilder numberBuilder = new StringBuilder();

        for (int i = 0; i < telephone.Length; i++) {
            if (char.IsDigit(telephone[i])) {
                numberBuilder.Append(telephone[i]);
            }
        }

        switch (numberBuilder.Length) {
            case 7: {
                    //seven digit telephone number, add the local area code, no extension
                    telephoneDecimal = decimal.Parse("907" + numberBuilder.ToString());
                    break;
                }
            case 10: {
                    //ten digit telephone number, assume area code given with no extension
                    telephoneDecimal = decimal.Parse(numberBuilder.ToString());
                    break;
                }
            case 14: {
                    //ten digit telephone number, assume area code given with as well as extension
                    telephoneDecimal = decimal.Parse(numberBuilder.ToString());
                    telephoneDecimal = telephoneDecimal / 10000;
                    break;
                }
            default: {
                    // TODO: Add error for invalid telephone number length
                    // This assumes only a 4 number extension is valid
                    break;
                }
        }

        return telephoneDecimal;
    }

    static internal decimal validateZipCode(string zipCode) {
        decimal zipCodeDecimal = 0;
        StringBuilder numberBuilder = new StringBuilder();

        for (int i = 0; i < zipCode.Length; i++) {
            if (char.IsDigit(zipCode[i])) {
                numberBuilder.Append(zipCode[i]);
            }
        }

        switch (numberBuilder.Length) {
            case 5: {
                    //five digit zip code, no plus four
                    zipCodeDecimal = decimal.Parse(numberBuilder.ToString());
                    break;
                }
            case 9: {
                    //five digit zip code with plus four
                    zipCodeDecimal = decimal.Parse(numberBuilder.ToString());
                    zipCodeDecimal = zipCodeDecimal / 10000;
                    break;
                }
            default: {
                    // TODO: Add error for invalid zip code length
                    break;
                }
        }
        return zipCodeDecimal;
    }
}