﻿
namespace CheckAppWeb.Northwind.Entities
{
    using Newtonsoft.Json;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Data.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    [ConnectionKey("Northwind"), DisplayName("Orders"), InstanceName("Order"), TwoLevelCached]
    [ReadPermission(Northwind.PermissionKeys.General)]
    [ModifyPermission(Northwind.PermissionKeys.General)]
    public sealed class OrderRow : Row, IIdRow, INameRow
    {
        [DisplayName("Order ID"), NotNull, Identity, QuickSearch]
        public Int32? OrderID
        {
            get { return Fields.OrderID[this]; }
            set { Fields.OrderID[this] = value; }
        }

        [DisplayName("Customer"), Size(5), NotNull, ForeignKey("Customers", "CustomerID"), LeftJoin("c"), CustomerEditor]
        public String CustomerID
        {
            get { return Fields.CustomerID[this]; }
            set { Fields.CustomerID[this] = value; }
        }

        [DisplayName("Customer"), Expression("c.[CompanyName]"), QuickSearch]
        public String CustomerCompanyName
        {
            get { return Fields.CustomerCompanyName[this]; }
            set { Fields.CustomerCompanyName[this] = value; }
        }

        [DisplayName("Employee"), ForeignKey("Employees", "EmployeeID"), LeftJoin("e")]
        [LookupEditor(typeof(EmployeeRow)), TextualField("EmployeeFullName")]
        public Int32? EmployeeID
        {
            get { return Fields.EmployeeID[this]; }
            set { Fields.EmployeeID[this] = value; }
        }

        [DisplayName("Employee"), Expression("CONCAT(e.[FirstName], CONCAT(' ', e.[LastName]))")]
        public String EmployeeFullName
        {
            get { return Fields.EmployeeFullName[this]; }
            set { Fields.EmployeeFullName[this] = value; }
        }

        [DisplayName("Employee Gender"), Expression("(CASE WHEN e.[TitleOfCourtesy] LIKE '%s%' THEN 2 WHEN e.[TitleOfCourtesy] LIKE '%Mr%' THEN 1 END)")]
        public Gender? EmployeeGender
        {
            get { return (Gender?)Fields.EmployeeGender[this]; }
            set { Fields.EmployeeGender[this] = (Int32?)value; }
        }

        [DisplayName("Order Date"), NotNull]
        public DateTime? OrderDate
        {
            get { return Fields.OrderDate[this]; }
            set { Fields.OrderDate[this] = value; }
        }

        [DisplayName("Required Date")]
        public DateTime? RequiredDate
        {
            get { return Fields.RequiredDate[this]; }
            set { Fields.RequiredDate[this] = value; }
        }

        [DisplayName("Shipped Date")]
        public DateTime? ShippedDate
        {
            get { return Fields.ShippedDate[this]; }
            set { Fields.ShippedDate[this] = value; }
        }

        [DisplayName("Shipping State"), Expression("(CASE WHEN T0.[ShippedDate] IS NULL THEN 0 ELSE 1 END)")]
        public OrderShippingState? ShippingState
        {
            get { return (OrderShippingState?)Fields.ShippingState[this]; }
            set { Fields.ShippingState[this] = (Int32?)value; }
        }

        [DisplayName("Ship Via"), ForeignKey("Shippers", "ShipperID"), LeftJoin("via"), LookupEditor(typeof(ShipperRow))]
        public Int32? ShipVia
        {
            get { return Fields.ShipVia[this]; }
            set { Fields.ShipVia[this] = value; }
        }

        [DisplayName("Freight"), Scale(4), DisplayFormat("#,##0.####"), AlignRight]
        public Decimal? Freight
        {
            get { return Fields.Freight[this]; }
            set { Fields.Freight[this] = value; }
        }

        [DisplayName("Ship Name"), Size(40)]
        public String ShipName
        {
            get { return Fields.ShipName[this]; }
            set { Fields.ShipName[this] = value; }
        }

        [DisplayName("Ship Address"), Size(60)]
        public String ShipAddress
        {
            get { return Fields.ShipAddress[this]; }
            set { Fields.ShipAddress[this] = value; }
        }

        [DisplayName("Ship City"), Size(15)]
        public String ShipCity
        {
            get { return Fields.ShipCity[this]; }
            set { Fields.ShipCity[this] = value; }
        }

        [DisplayName("Ship Region"), Size(15)]
        public String ShipRegion
        {
            get { return Fields.ShipRegion[this]; }
            set { Fields.ShipRegion[this] = value; }
        }

        [DisplayName("Ship Postal Code"), Size(10)]
        public String ShipPostalCode
        {
            get { return Fields.ShipPostalCode[this]; }
            set { Fields.ShipPostalCode[this] = value; }
        }

        [DisplayName("Ship Country"), Size(15)]
        public String ShipCountry
        {
            get { return Fields.ShipCountry[this]; }
            set { Fields.ShipCountry[this] = value; }
        }

        [DisplayName("Customer Contact Name"), Expression("c.[ContactName]")]
        public String CustomerContactName
        {
            get { return Fields.CustomerContactName[this]; }
            set { Fields.CustomerContactName[this] = value; }
        }

        [DisplayName("Customer Contact Title"), Expression("c.[ContactTitle]")]
        public String CustomerContactTitle
        {
            get { return Fields.CustomerContactTitle[this]; }
            set { Fields.CustomerContactTitle[this] = value; }
        }

        [DisplayName("Customer City"), Expression("c.[City]")]
        public String CustomerCity
        {
            get { return Fields.CustomerCity[this]; }
            set { Fields.CustomerCity[this] = value; }
        }

        [DisplayName("Customer Region"), Expression("c.[Region]")]
        public String CustomerRegion
        {
            get { return Fields.CustomerRegion[this]; }
            set { Fields.CustomerRegion[this] = value; }
        }

        [DisplayName("Customer Country"), Expression("c.[Country]")]
        public String CustomerCountry
        {
            get { return Fields.CustomerCountry[this]; }
            set { Fields.CustomerCountry[this] = value; }
        }

        [DisplayName("Customer Phone"), Expression("c.[Phone]")]
        public String CustomerPhone
        {
            get { return Fields.CustomerPhone[this]; }
            set { Fields.CustomerPhone[this] = value; }
        }

        [DisplayName("Customer Fax"), Expression("c.[Fax]")]
        public String CustomerFax
        {
            get { return Fields.CustomerFax[this]; }
            set { Fields.CustomerFax[this] = value; }
        }

        [DisplayName("Ship Via"), Expression("via.[CompanyName]")]
        public String ShipViaCompanyName
        {
            get { return Fields.ShipViaCompanyName[this]; }
            set { Fields.ShipViaCompanyName[this] = value; }
        }

        [DisplayName("Ship Via Phone"), Expression("via.[Phone]")]
        public String ShipViaPhone
        {
            get { return Fields.ShipViaPhone[this]; }
            set { Fields.ShipViaPhone[this] = value; }
        }

        [DisplayName("Details"), MasterDetailRelation(foreignKey: "OrderID"), ClientSide]
        public List<OrderDetailRow> DetailList
        {
            get { return Fields.DetailList[this]; }
            set { Fields.DetailList[this] = value; }
        }

        IIdField IIdRow.IdField
        {
            get { return Fields.OrderID; }
        }

        StringField INameRow.NameField
        {
            get { return Fields.CustomerID; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public OrderRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field OrderID;
            public StringField CustomerID;
            public Int32Field EmployeeID;
            public DateTimeField OrderDate;
            public DateTimeField RequiredDate;
            public DateTimeField ShippedDate;
            public Int32Field ShipVia;
            public DecimalField Freight;
            public StringField ShipName;
            public StringField ShipAddress;
            public StringField ShipCity;
            public StringField ShipRegion;
            public StringField ShipPostalCode;
            public StringField ShipCountry;

            public StringField CustomerCompanyName;
            public StringField CustomerContactName;
            public StringField CustomerContactTitle;
            public StringField CustomerCity;
            public StringField CustomerRegion;
            public StringField CustomerCountry;
            public StringField CustomerPhone;
            public StringField CustomerFax;

            public StringField EmployeeFullName;
            public Int32Field EmployeeGender;

            public StringField ShipViaCompanyName;
            public StringField ShipViaPhone;

            public Int32Field ShippingState;
            public RowListField<OrderDetailRow> DetailList;

            public RowFields()
                : base("Orders")
            {
                LocalTextPrefix = "Northwind.Order";
            }
        }
    }
}