﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardGame.Web.Models
{
    public class Cart
    {
        public int Money { get; set; }
        public List<Packages> Packs { get; set; }
    }
}