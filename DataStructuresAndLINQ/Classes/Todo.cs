﻿using System;

namespace DataStructuresAndLINQ
{
    public class Todo
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public bool IsComlete { get; set; }
        public int UserId { get; set; }
    }
}
