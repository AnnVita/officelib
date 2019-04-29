using System.Collections.Generic;

namespace OfficeLib.DB
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Position { get; set; }
        public string Password { get; set; }
        //public List<BookItem> Books { get; set; }
    }
}

//_dbContext.Users.Include(user => user.Books).Where(...)
