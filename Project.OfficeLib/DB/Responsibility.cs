using System;
using System.Collections.Generic;

namespace OfficeLib.DB
{
    public class Responsibility
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long BookId { get; set; }
        public string State { get; set; }
        public DateTime Date { get; set; }
    }
}
