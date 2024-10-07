using Domain.Guest.Exceptions;
using Domain.Guest.Ports;
using Domain.Guest.ValueObjects;

namespace Domain.Guest.Entities
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

        private void ValidateState()
        {
            if (DocumentId == null ||
               string.IsNullOrEmpty(DocumentId.IdNumber) ||
               DocumentId.IdNumber.Length <= 3 ||
               DocumentId?.DocumentType == 0
               )
            {
                throw new InvalidPersonDocumentIdException();
            }

            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Surname) ||
                string.IsNullOrWhiteSpace(Email))
            {
                throw new MissingRequiredInformation();
            }

            if (!Utils.ValidateEmail(Email))
            {
                throw new InvalidEmailException();
            }
        }

        public async Task Save(IGuestRepository guestRepository)
        {
            ValidateState();

            if (Id == 0)
            {
                Id = await guestRepository.Create(this);
            }
            else
            {
                //await guestRepository.Update(this);
            }
        }
    }
}
