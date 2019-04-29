
using System;
using System.Collections.Generic;

namespace OfficeLib.DB
{
    public class Assignment
    {
        public long Id { get; set; }
        public long AssignerId { get; set; }
        public long UserId { get; set; }
        public long BookId { get; set; }
        public DateTime Date { get; set; }
        public string State { get; set; }
    }
}
