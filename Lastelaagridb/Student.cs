﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lastelaagridb
{
    public class Student
    {
        public int ID { get; set; }
        public String Nimi { get; set; }
        public String Isikukood { get; set; }
        public String Kool { get; set; }
        public int Klass { get; set; }
        public String Telefon { get; set; }
        public String Aadress { get; set; }
        public int Ruhm { get; set; }

        public Student() { }
    }
}
