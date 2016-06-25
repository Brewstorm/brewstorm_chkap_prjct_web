﻿namespace CheckAppWeb.Northwind {
    export class CustomerForm extends Serenity.PrefixedContext {
        static formKey = 'Northwind.Customer';

    }

    export interface CustomerForm {
        CustomerID: Serenity.StringEditor;
        CompanyName: Serenity.StringEditor;
        ContactName: Serenity.StringEditor;
        ContactTitle: Serenity.StringEditor;
        Representatives: Serenity.LookupEditor;
        Address: Serenity.StringEditor;
        City: Serenity.StringEditor;
        Region: Serenity.StringEditor;
        PostalCode: Serenity.StringEditor;
        Country: Serenity.StringEditor;
        Phone: Serenity.StringEditor;
        Fax: Serenity.StringEditor;
        NoteList: NotesEditor;
    }

    [['CustomerID', () => Serenity.StringEditor], ['CompanyName', () => Serenity.StringEditor], ['ContactName', () => Serenity.StringEditor], ['ContactTitle', () => Serenity.StringEditor], ['Representatives', () => Serenity.LookupEditor], ['Address', () => Serenity.StringEditor], ['City', () => Serenity.StringEditor], ['Region', () => Serenity.StringEditor], ['PostalCode', () => Serenity.StringEditor], ['Country', () => Serenity.StringEditor], ['Phone', () => Serenity.StringEditor], ['Fax', () => Serenity.StringEditor], ['NoteList', () => NotesEditor]].forEach(x => Object.defineProperty(CustomerForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

