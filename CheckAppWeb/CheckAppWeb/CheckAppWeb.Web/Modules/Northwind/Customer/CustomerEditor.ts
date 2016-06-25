﻿namespace CheckAppWeb.Northwind {

    @Serenity.Decorators.registerEditor()
    export class CustomerEditor extends Serenity.LookupEditorBase<CustomerRow, any> {

        constructor(hidden: JQuery) {
            super(hidden);
        }

        protected getLookupKey() {
            return 'Northwind.Customer';
        }

        protected getItemText(item, lookup) {
            return super.getItemText(item, lookup) + ' [' + item.CustomerID + ']';
        }
    }
}