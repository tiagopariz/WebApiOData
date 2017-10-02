using System;
using System.Collections.Generic;

namespace WebApiOData.Entities
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Fullname { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual List<Email> Emails { get; set; }
    }
}