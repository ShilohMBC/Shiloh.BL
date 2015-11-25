using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shiloh.BL.ShilohTableAdapters;
using Shiloh.BL;

[System.ComponentModel.DataObject]
public class Community {
    private communityTableAdapter communityAdapter = null;
    protected communityTableAdapter Adapter {
        get {
            if (communityAdapter == null)
                communityAdapter = new communityTableAdapter();

            return communityAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public Shiloh.BL.Shiloh.communityDataTable GetCommunity()
    {
        return Adapter.GetCommunity();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public Shiloh.BL.Shiloh.communityDataTable GetCommunityByID(int ID)
    {
        return Adapter.GetCommunityByID(ID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public Shiloh.BL.Shiloh.communityDataTable GetCommunityByName(String name)
    {
        return Adapter.GetCommunityByName(name);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public Shiloh.BL.Shiloh.communityDataTable GetParentCommunityByID(int parentID)
    {
        return Adapter.GetParentCommunityByID(parentID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public bool AddCommunity(string type, string name, string description, string telephone, string streetAddress, string city, string state, string zipCode, string emailAddress, string imagePath, string status, int parentID) {
        // Create a new CommunityRow instance
        Shiloh.BL.Shiloh.communityDataTable communities = new Shiloh.BL.Shiloh.communityDataTable();
        Shiloh.BL.Shiloh.communityRow community = communities.NewcommunityRow();

        community.type = type;
        community.name = name;
        if (description == null) community.SetdescriptionNull(); else community.description = description;
        if (telephone == null) community.SettelephoneNull(); else community.telephone = validateTelephone(telephone);
        if (streetAddress == null) community.SetstreetAddressNull(); else community.streetAddress = streetAddress;
        if (city == null) community.SetcityNull(); else community.city = city;
        if (state == null) community.SetstateNull(); else community.state = state;
        if (zipCode == null) community.SetzipCodeNull(); else community.zipCode = validateZipCode(zipCode);
        if (emailAddress == null) community.SetemailAddressNull(); else community.emailAddress = emailAddress;
        if (imagePath == null) community.SetimagePathNull(); else community.imagePath = imagePath;
        if (status == null) community.SetstatusNull(); else community.status = status;
        community.parentID = parentID;

        // Add the new community
        communities.AddcommunityRow(community);
        int rowsAffected = Adapter.Update(communities);

        // Return true if precisely one row was inserted, otherwise false
        return rowsAffected == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
    public bool UpdateComunity(string type, string name, string description, string telephone, string streetAddress, string city, string state, string zipCode, string emailAddress, string imagePath, string status, int parentID, int ID) {

        Shiloh.BL.Shiloh.communityDataTable communities = Adapter.GetCommunityByID(ID);
        if (communities.Count == 0)
            // no matching record found, return false.
            return false;

        Shiloh.BL.Shiloh.communityRow community = communities[0];

        community.type = type;
        community.name = name;
        if (description == null) community.SetdescriptionNull(); else community.description = description;
        if (telephone == null) community.SettelephoneNull(); else community.telephone = validateTelephone(telephone);
        if (streetAddress == null) community.SetstreetAddressNull(); else community.streetAddress = streetAddress;
        if (city == null) community.SetcityNull(); else community.city = city;
        if (state == null) community.SetstateNull(); else community.state = state;
        if (zipCode == null) community.SetzipCodeNull(); else community.zipCode = validateZipCode(zipCode);
        if (emailAddress == null) community.SetemailAddressNull(); else community.emailAddress = emailAddress;
        if (imagePath == null) community.SetimagePathNull(); else community.imagePath = imagePath;
        if (status == null) community.SetstatusNull(); else community.status = status;
        community.parentID = parentID;

        // Update the community record
        int rowsAffected = Adapter.Update(community);

        // Return true if precisely one row was updated, otherwise false
        return rowsAffected == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public bool DeleteCommunityByID(int ID) {
        int rowsAffected = Adapter.DeleteCommunityByID(ID);

        // TODO: Delete all memberships associated with the community
        // int rowsAffected = rowsAffected + Adapter.DeleteMembershipsByCommunityID(ID);

        // Return true if precisely one row was deleted, otherwise false
        return rowsAffected == 1;
    }
    private decimal validateTelephone(string telephone) {
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

    private decimal validateZipCode(string zipCode) {
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
