using System;

namespace Game.Tasks.Models
{
    public interface ITaskModel
    {
        int Id { get; }
        
        string Description { get; }
        
        bool IsComplete { get; }
        
    }
}