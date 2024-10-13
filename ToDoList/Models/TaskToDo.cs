namespace ToDoList.Models
{
    public class TaskToDo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeadLine { get; set; }
        public string? FileName { get; set; }

        public Member Member { get; set; }
        public int MemberId { get; set; }
    }
}
