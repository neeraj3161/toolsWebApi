using toolsWebApi.Entity;
using toolsWebApi.Models;

namespace toolsWebApi.Mappings
{
    public class RegistrationMapping
    {
        public RegistrationModel Map(RegistrationEntity source)
        {
            return new RegistrationModel()
            {
                Surname = source.Surname,
                Name = source.Name,
                Email = source.Email,
                Password = source.Password,
            };
        }
    }
}
