namespace Api.Domain.Entities
{
    //1ºPasso vai criar um user entity herdando de BaseEntity
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }       
    }
}
