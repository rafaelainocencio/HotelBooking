﻿using Domain.Exceptions;
using Domain.Ports;
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

        private void ValidateState()
        {
            if(DocumentId == null ||
               DocumentId.IdNumber.Length <= 3 ||
               DocumentId.DocumentType == 0
               )
            {
                throw new InvalidPersonDocumentIdException();
            }

            if (Name == null || Surname == null || Email == null)
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
            this.ValidateState();

            if (this.Id == 0 )
            {
                this.Id = await guestRepository.Create(this);
            }
            else
            {
                //await guestRepository.Update(this);
            }
        }
    }
}
