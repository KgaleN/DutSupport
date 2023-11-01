namespace Support
{
    public class Templates
    {
        public Dictionary<string, string> MessageTemplates { get; private set; }

        public Templates()
        {
            MessageTemplates = new Dictionary<string, string>
            {
                //message templates
                {"Student Finance", "Dear [StudentName], your current balance is [Balance]. Please pay by [DueDate]."},
                {"Staff Salary", "Dear [StaffName], your salary for [Month] is [SalaryAmount]. Please check your bank account."},
                {"Document Request", "Dear [Name], your requested document for [DocumentType] is ready for collection at [Location]."},
                {"Student Scholarship", "Congratulations [StudentName]! You've been awarded the [ScholarshipName] scholarship worth [ScholarshipAmount]."},
                {"Staff Leave", "Dear [StaffName], your leave request from [StartDate] to [EndDate] has been approved. Please liaise with your department for any handovers."},
                {"Student Transcript", "Dear [StudentName], your transcript for [Year] has been processed. You can collect it from the [DepartmentName] department on [PickupDate]."},
                {"Staff Expense Reimbursement", "Dear [StaffName], your expense claim for [ExpenseDate] totaling [ExpenseAmount] has been approved. The reimbursement will be added to your next paycheck."},
                {"Student Housing", "Dear [StudentName], your application for housing in [HousingBlock] has been approved. Your move-in date is [MoveInDate]."},
                {"Staff Meeting Schedule", "Dear [StaffName], there is a scheduled department meeting on [MeetingDate] at [MeetingTime]. The venue is [MeetingLocation]. Attendance is mandatory."},
                {"Student Club Membership", "Congratulations [StudentName]! You've been accepted as a member of the [ClubName]. Our next meeting is on [MeetingDate]. We're excited to see you there!"}
            };
        }
    }
}
