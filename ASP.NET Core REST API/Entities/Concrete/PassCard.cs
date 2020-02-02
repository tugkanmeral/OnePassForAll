using Core.CoreEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class PassCard : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
    }
}
