using QuanLySach.DAL.EF.Models;

namespace QuanLySach.DAL.EF.Mappers
{
    public static class CustomerMapper
    {
        public static Customer ToEntity(DomainModels.Customer? customer)
        {
            if (customer == null)
            {
                return null;
            }

            return new Customer
            {
                Id = customer.Id,
                Email = customer.Email,
                Address = customer.Address,
                DateOfBirth = customer.DateOfBirth,
                FullName = customer.FullName,
                IdentityCardNumber = customer.IdentityCardNumber,
                PhoneNumber = customer.PhoneNumber
            };
        }

        public static DomainModels.Customer ToDomain(Customer customer)
        {
            if (customer == null)
            {
                return null;
            }

            return new DomainModels.Customer
            {
                Id = customer.Id,
                Email = customer.Email,
                Address = customer.Address,
                DateOfBirth = customer.DateOfBirth,
                FullName = customer.FullName,
                IdentityCardNumber = customer.IdentityCardNumber,
                PhoneNumber = customer.PhoneNumber
            };
        }
    }
}
