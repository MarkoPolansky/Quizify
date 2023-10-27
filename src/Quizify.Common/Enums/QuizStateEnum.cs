namespace Quizify.Common.Enums;

public enum QuizStateEnum
{
    Creation = 0, //only user who created can see it 

    Published = 1, //everybody with gamePin Can join

    Running = 2, //everybody who joined can answer to quiz
    
    Ended = 3,
}