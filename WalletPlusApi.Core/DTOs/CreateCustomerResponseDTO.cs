using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletPlusApi.Core.Data;

namespace WalletPlusApi.Core.DTOs
{
    public class CreateCustomerResponseDTO
    {
        public string WalletId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string EncryptedSecKey { get; set; } // To be passed in header for every other request to the api asides login
        public string LastName { get; set; }

        public CreateCustomerResponseDTO PopulateDTO(CustomerDTO model1, PointWallet model2)
        {
            if (model1 ==null || model2 == null)
            {
                return null;
            }
            return new CreateCustomerResponseDTO
            {
                WalletId = model2.WalletId,
                Email = model1.Email,
                FirstName = model1.FirstName,
                LastName = model1.LastName,
            };
        }
    }
}
