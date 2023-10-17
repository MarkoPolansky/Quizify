namespace Quizify.Common.Models
{
    public abstract record ModelBase : IRequiredId
    {
        public required Guid Id { get; init; }
    }
}
