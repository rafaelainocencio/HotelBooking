using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        //não queremos que isso vire uma tabela
        //mas que os atributos do obj se tornem campos da tb do Guest
        public PersonId DocumentId { get; set; } 
    }
}
