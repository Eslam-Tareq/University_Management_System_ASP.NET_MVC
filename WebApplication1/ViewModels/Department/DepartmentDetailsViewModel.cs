namespace WebApplication1.ViewModels.Department
{
    public class DepartmentDetailsViewModel
    {
        public int Dept_Id { set; get; }

        public string Name { set; get; }
        public string PhoneNumber { set; get; }

        public string Location { set; get; }

        public List<string>? Students_Names { set; get; }
    }
}
