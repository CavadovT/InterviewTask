namespace Work.Data.Entites
{
    public class Employee:BaseEntity
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public Department Department { get; set; }
    }
}
