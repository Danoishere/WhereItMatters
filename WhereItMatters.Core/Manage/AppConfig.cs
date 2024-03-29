﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereItMatters.Core
{
    public class AppConfig
    {
        public static string AppName { get; set; } = "Selfless Health";

        public static string DomainMain { get; set; } = "http://whereitmattersdev.azurewebsites.net";
        public static string DomainAdmin { get; set; } = "http://whereitmattersdev-admin.azurewebsites.net";

        public const string RoleADMIN  = "ADMIN";
        public const string RoleNGO  = "NGO";
    }
}
