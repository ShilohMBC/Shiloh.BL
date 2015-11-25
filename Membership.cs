using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shiloh.BL;
using Shiloh.BL.ShilohTableAdapters;

[System.ComponentModel.DataObject]
public class Membership {
    private membershipTableAdapter membershipAdapter = null;
    protected membershipTableAdapter Adapter {
        get {
            if (membershipAdapter == null)
                membershipAdapter = new membershipTableAdapter();

            return membershipAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public Shiloh.BL.Shiloh.membershipDataTable GetMembership()
    {
        return Adapter.GetMembership();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public Shiloh.BL.Shiloh.membershipDataTable GetMembershipByID(int ID)
    {
        return Adapter.GetMembershipByID(ID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public Shiloh.BL.Shiloh.membershipDataTable GetMembershipByCommunity(int communityID)
    {
        // TODO: 
        return Adapter.GetMembershipByCommunity(communityID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public Shiloh.BL.Shiloh.membershipDataTable GetMembershipByPerson(int personID)
    {
        // TODO: 
        return Adapter.GetMembershipByPerson(personID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public bool AddMembership(int ID, int communityID, int personID)
    {
        // Create a new MembershipRow instance
        Shiloh.BL.Shiloh.membershipDataTable membership = new Shiloh.BL.Shiloh.membershipDataTable();
        Shiloh.BL.Shiloh.membershipRow member = membership.NewmembershipRow();

        member.communityID = communityID;
        member.personID = personID;

        // Add the new community
        membership.AddmembershipRow(member);
        int rowsAffected = Adapter.Update(membership);

        // Return true if precisely one row was inserted, otherwise false
        return rowsAffected == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
    public bool UpdateMembership(int ID, int communityID, int personID)
    {
        Shiloh.BL.Shiloh.membershipDataTable membership = Adapter.GetMembershipByID(ID);
        if (membership.Count == 0)
            // no matching record found, return false
            return false;

        Shiloh.BL.Shiloh.membershipRow member = membership[0];

        // TOO: Add table adapters to get person and communty ID based upon string provided
        member.communityID = communityID;
        member.personID = personID;

        // Update the membership record
        int rowsAffected = Adapter.Update(member);

        // Return true if precisely one row was updated, otherwise false
        return rowsAffected == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public bool DeleteMembershipByID(int ID)
    {
        // TODO: Use person ID to retrieve membership row and add an expriation date and change membership status to inactive.
        int rowsAffected = Adapter.DeleteMembershipByID(ID);

        // Return true if precisely one row was deleted, otherwise false
        return rowsAffected == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public bool DeleteMembershipByCommunityID(int communityID) {
        // TODO: Use person ID to retrieve membership row and add an expriation date and change membership status to inactive.
        int rowsAffected = Adapter.DeleteMembershipByCommunityID(communityID);

        // Return true if precisely one row was deleted, otherwise false
        return rowsAffected == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public bool DeleteMembershipByPersonID(int personID) {
        // TODO: Use person ID to retrieve membership row and add an expriation date and change membership status to inactive.
        int rowsAffected = Adapter.DeleteMembershipByPersonID(personID);

        // Return true if precisely one row was deleted, otherwise false
        return rowsAffected == 1;
    }
}