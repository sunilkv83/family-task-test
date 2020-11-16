using AutoMapper.Configuration.Conventions;
using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModel
{
    public class TaskVm
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid Member { get; set; }

        public string Subject { get; set; }
        public bool IsComplete { get; set; }
    }
}
