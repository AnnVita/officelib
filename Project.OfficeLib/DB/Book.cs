
namespace OfficeLib.DB
{
    public class Book
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public byte[] File { get; set;}        
        public byte[] Cover { get; set;}
        public string State { get; set;}
        public string Categories { get; set;}
        public string Type { get; set;}
    }
}