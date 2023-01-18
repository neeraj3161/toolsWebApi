namespace toolsWebApi.Entity
{
    public class RegistrationEntity:ITenantEntity
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Surname { get; set; }

        public virtual string Email { get; set; }   

        public  virtual string Password { get; set; }

    }
}
