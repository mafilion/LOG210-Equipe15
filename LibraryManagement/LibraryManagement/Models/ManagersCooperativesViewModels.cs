﻿using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class ManagersCooperativesViewModels
    {
        public Manager manager { get; set; }
        public Cooperative cooperative { get; set; }
    }
}