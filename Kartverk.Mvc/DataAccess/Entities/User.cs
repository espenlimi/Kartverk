namespace Kartverk.Mvc.DataAccess.Entities
{
    public class User
    {
        public string Name { get; set; }
        /// <summary>
        /// FK for auth user
        /// </summary>
        public string Email { get; set; }

        public string Language { get; set; }
    }
}
