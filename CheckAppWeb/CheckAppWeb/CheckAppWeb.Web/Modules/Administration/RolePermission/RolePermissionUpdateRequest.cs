﻿
namespace CheckAppWeb.Administration
{
    using Newtonsoft.Json;
    using Serenity;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class RolePermissionUpdateRequest : ServiceRequest
    {
        public Int32? RoleID { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public string Module { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public string Submodule { get; set; }
        public List<string> Permissions { get; set; }
    }
}