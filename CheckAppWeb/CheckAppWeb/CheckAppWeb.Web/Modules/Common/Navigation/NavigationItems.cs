﻿using Serenity.Navigation;
using Northwind = CheckAppWeb.Northwind.Pages;
using Administration = CheckAppWeb.Administration.Pages;
using Basic = CheckAppWeb.BasicSamples.Pages;

[assembly: NavigationLink(1000, "Dashboard", url: "~/", permission: "", icon: "icon-speedometer")]

[assembly: NavigationMenu(7000, "Northwind", icon: "icon-anchor")]
[assembly: NavigationLink(7100, "Northwind/Customers", typeof(Northwind.CustomerController), icon: "icon-wallet")]
[assembly: NavigationLink(7200, "Northwind/Orders", typeof(Northwind.OrderController), icon: "icon-basket-loaded")]
[assembly: NavigationLink(7300, "Northwind/Products", typeof(Northwind.ProductController), icon: "icon-present")]
[assembly: NavigationLink(7400, "Northwind/Suppliers", typeof(Northwind.SupplierController), icon: "icon-magic-wand")]
[assembly: NavigationLink(7500, "Northwind/Shippers", typeof(Northwind.ShipperController), icon: "icon-plane")]
[assembly: NavigationLink(7600, "Northwind/Categories", typeof(Northwind.CategoryController), icon: "icon-folder-alt")]
[assembly: NavigationLink(7700, "Northwind/Regions", typeof(Northwind.RegionController), icon: "icon-map")]
[assembly: NavigationLink(7800, "Northwind/Territories", typeof(Northwind.TerritoryController), icon: "icon-puzzle")]

[assembly: NavigationMenu(7900, "Basic Samples", icon: "icon-magic-wand")]
[assembly: NavigationMenu(7900, "Basic Samples/Dialogs")]
[assembly: NavigationLink(7900, "Basic Samples/Dialogs/Chart in a Dialog", typeof(Basic.BasicSamplesController), action: "ChartInDialog")]
[assembly: NavigationLink(7900, "Basic Samples/Dialogs/Cloneable Entity Dialog", typeof(Basic.BasicSamplesController), action: "CloneableEntityDialog")]
[assembly: NavigationLink(7900, "Basic Samples/Dialogs/Default Values in New Dialog", typeof(Basic.BasicSamplesController), action: "DefaultValuesInNewDialog")]
[assembly: NavigationLink(7900, "Basic Samples/Dialogs/Multi Column Dialog", typeof(Basic.BasicSamplesController), action: "MultiColumnResponsiveDialog")]
[assembly: NavigationLink(7900, "Basic Samples/Dialogs/Responsive Dialog", typeof(Basic.BasicSamplesController), action: "ResponsiveDialog")]
[assembly: NavigationMenu(7900, "Basic Samples/Editors")]
[assembly: NavigationLink(7900, "Basic Samples/Editors/Filtered Lookup in Detail.", typeof(Basic.BasicSamplesController), action: "FilteredLookupInDetailDialog")]
[assembly: NavigationLink(7900, "Basic Samples/Editors/Lookup Filter by Multi Val.", typeof(Basic.BasicSamplesController), action: "LookupFilterByMultipleValues")]
[assembly: NavigationMenu(7900, "Basic Samples/Grids")]
[assembly: NavigationLink(7900, "Basic Samples/Grids/Cancellable Bulk Action", typeof(Basic.BasicSamplesController), action: "CancellableBulkAction")]
[assembly: NavigationLink(7900, "Basic Samples/Grids/Conditional Formatting", typeof(Basic.BasicSamplesController), action: "ConditionalFormatting")]
[assembly: NavigationLink(7900, "Basic Samples/Grids/Custom Links In Grid", typeof(Basic.BasicSamplesController), action: "CustomLinksInGrid")]
[assembly: NavigationLink(7900, "Basic Samples/Grids/Grouping and Sum. In Grid", typeof(Basic.BasicSamplesController), action: "GroupingAndSummariesInGrid")]
[assembly: NavigationLink(7900, "Basic Samples/Grids/Grid Filtered by Criteria", typeof(Basic.BasicSamplesController), action: "GridFilteredByCriteria")]
[assembly: NavigationLink(7900, "Basic Samples/Grids/View Without ID", typeof(Basic.BasicSamplesController), action: "ViewWithoutID")]

[assembly: NavigationMenu(8000, "Theme Samples", icon: "icon-diamond")]

[assembly: NavigationLink(8100, "Theme Samples/Dashboard v2", url: "~/AdminLTE/DashboardV2", permission: "", icon: "icon-speedometer")]
[assembly: NavigationLink(8200, "Theme Samples/Widgets", url: "~/AdminLTE/Widgets", permission: "", icon: "fa-th")]

[assembly: NavigationMenu(8300, "Theme Samples/Charts", icon: "fa-pie-chart")]
[assembly: NavigationLink(8310, "Theme Samples/Charts/ChartJS", url: "~/AdminLTE/Charts/ChartJS", permission: "")]
[assembly: NavigationLink(8320, "Theme Samples/Charts/Morris", url: "~/AdminLTE/Charts/Morris", permission: "")]
[assembly: NavigationLink(8330, "Theme Samples/Charts/Flot", url: "~/AdminLTE/Charts/Flot", permission: "")]

[assembly: NavigationMenu(8400, "Theme Samples/UI Elements", icon: "fa-laptop")]
[assembly: NavigationLink(8410, "Theme Samples/UI Elements/General", url: "~/AdminLTE/UIElements/General", permission: "")]
[assembly: NavigationLink(8420, "Theme Samples/UI Elements/Icons", url: "~/AdminLTE/UIElements/Icons", permission: "")]
[assembly: NavigationLink(8430, "Theme Samples/UI Elements/Buttons", url: "~/AdminLTE/UIElements/Buttons", permission: "")]
[assembly: NavigationLink(8440, "Theme Samples/UI Elements/Sliders", url: "~/AdminLTE/UIElements/Sliders", permission: "")]
[assembly: NavigationLink(8450, "Theme Samples/UI Elements/Timeline", url: "~/AdminLTE/UIElements/Timeline", permission: "")]
[assembly: NavigationLink(8460, "Theme Samples/UI Elements/Modals", url: "~/AdminLTE/UIElements/Modals", permission: "")]

[assembly: NavigationMenu(8500, "Theme Samples/Forms", icon: "fa-edit")]
[assembly: NavigationLink(8510, "Theme Samples/Forms/General Elements", url: "~/AdminLTE/Forms/GeneralElements", permission: "")]
[assembly: NavigationLink(8520, "Theme Samples/Forms/Advanced Elements", url: "~/AdminLTE/Forms/AdvancedElements", permission: "")]
[assembly: NavigationLink(8530, "Theme Samples/Forms/Text Editors", url: "~/AdminLTE/Forms/TextEditors", permission: "")]

[assembly: NavigationMenu(8600, "Theme Samples/Tables", icon: "fa-table")]
[assembly: NavigationLink(8610, "Theme Samples/Tables/Simple Tables", url: "~/AdminLTE/Tables/SimpleTables", permission: "")]
[assembly: NavigationLink(8620, "Theme Samples/Tables/Data Tables", url: "~/AdminLTE/Tables/DataTables", permission: "")]

[assembly: NavigationLink(8700, "Theme Samples/Calendar", url: "~/AdminLTE/Calendar", permission: "", icon: "fa-calendar")]

[assembly: NavigationMenu(8800, "Theme Samples/Mailbox", icon: "fa-envelope")]
[assembly: NavigationLink(8810, "Theme Samples/Mailbox/Inbox", url: "~/AdminLTE/Mailbox/Inbox", permission: "")]
[assembly: NavigationLink(8820, "Theme Samples/Mailbox/Compose", url: "~/AdminLTE/Mailbox/Compose", permission: "")]
[assembly: NavigationLink(8820, "Theme Samples/Mailbox/Read", url: "~/AdminLTE/Mailbox/Read", permission: "")]

[assembly: NavigationMenu(8800, "Theme Samples/Examples", icon: "fa-folder")]
[assembly: NavigationLink(8810, "Theme Samples/Examples/Invoice", url: "~/AdminLTE/Examples/Invoice", permission: "")]
[assembly: NavigationLink(8820, "Theme Samples/Examples/Profile", url: "~/AdminLTE/Examples/Profile", permission: "")]
[assembly: NavigationLink(8820, "Theme Samples/Examples/Login", url: "~/AdminLTE/Examples/Login", permission: "")]
[assembly: NavigationLink(8820, "Theme Samples/Examples/Register", url: "~/AdminLTE/Examples/Register", permission: "")]
[assembly: NavigationLink(8820, "Theme Samples/Examples/Lockscreen", url: "~/AdminLTE/Examples/Lockscreen", permission: "")]
[assembly: NavigationLink(8820, "Theme Samples/Examples/Error 404", url: "~/AdminLTE/Examples/Error404", permission: "")]
[assembly: NavigationLink(8820, "Theme Samples/Examples/Error 500", url: "~/AdminLTE/Examples/Error500", permission: "")]
[assembly: NavigationLink(8820, "Theme Samples/Examples/Blank Page", url: "~/AdminLTE/Examples/BlankPage", permission: "")]
[assembly: NavigationLink(8820, "Theme Samples/Examples/Pace Page", url: "~/AdminLTE/Examples/PacePage", permission: "")]

[assembly: NavigationMenu(9000, "Administration", icon: "icon-screen-desktop")]
[assembly: NavigationLink(9100, "Administration/Languages", typeof(Administration.LanguageController), icon: "icon-bubbles")]
[assembly: NavigationLink(9200, "Administration/Translations", typeof(Administration.TranslationController), icon: "icon-speech")]
[assembly: NavigationLink(9300, "Administration/Roles", typeof(Administration.RoleController), icon: "icon-lock")]
[assembly: NavigationLink(9400, "Administration/User Management", typeof(Administration.UserController), icon: "icon-people")]