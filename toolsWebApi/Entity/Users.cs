namespace toolsWebApi.Entity
{
    public class Users : Entity
    {
        public virtual int Id { get; set; }
        public virtual string name { get; set; }

        public virtual string email { get; set; }

        public virtual string surname { get; set; }


    }
}
