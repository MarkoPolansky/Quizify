using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizify.Api.DAL.EF.Entities.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; init; }
    }
}
