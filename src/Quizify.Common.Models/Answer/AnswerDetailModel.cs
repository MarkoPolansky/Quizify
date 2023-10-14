using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizify.Common.Models
{
    public record AnswerDetailModel : IRequiredId
    {
        public required Guid Id { get; init; }

    }
}
