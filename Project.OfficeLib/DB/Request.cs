using System;
using System.Collections.Generic;

namespace OfficeLib.DB
{
    public class Request
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long BookId { get; set; }
        public string Comment { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
    }
}