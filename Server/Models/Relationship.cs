namespace Server.Models
{
    public class Relationship : BaseModel
    {
        public string TableName { get; set; }

        public int FatherId { get; set; }
        public Person Father { get; set; }

        public int MotherId { get; set; }
        public Person Mother { get; set; }


    }
}
