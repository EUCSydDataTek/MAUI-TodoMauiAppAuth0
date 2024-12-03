namespace TodoApiAuth0.Data;

public class TodoItem
{
    public int Id { get; set; }

    public DateTime CreatedTime { get; set; } //= DateTime.Now.ToUniversalTime();

    public required string Description { get; set; }

    public PriorityLevel Priority { get; set; } = PriorityLevel.Normal;

    public bool Completed { get; set; } = false;
}

public enum PriorityLevel
{
    Low, Normal, High
}
