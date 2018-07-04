using System;
using System.Collections.Generic;

namespace DataStructuresAndLINQ.DataStructures
{
    public class User
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public virtual IEnumerable<Post> Posts { get; set; }
        public virtual IEnumerable<Todo> Todos { get; set; }
        public Address Address { get; set; }
    }
}
