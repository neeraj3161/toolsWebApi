using toolsWebApi.IServices.hibernateConfig;

namespace toolsWebApi.Models
{
    public class UserModel:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public DateTime Created { get; set; }
    }
}
