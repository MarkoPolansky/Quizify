﻿using Quizify.Api.DAL.EF.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizify.Api.DAL.EF.Entities
{
    public abstract record EntityBase : IEntity
    {
        public required Guid Id { get; init; }
  
    }
}
