namespace ToDoList.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<TaskToDo> Tasks { get; set; }
    }
}
