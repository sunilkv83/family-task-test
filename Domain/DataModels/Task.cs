using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DataModels
{
    public class Task
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [MapTo("Member")]
        public Guid? AssignedToId{ get; set; }
        public bool IsComplete { get; set; }
        public string Subject { get; set; }
    }
}
