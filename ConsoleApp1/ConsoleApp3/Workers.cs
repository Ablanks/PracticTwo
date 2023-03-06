namespace ConsoleApp2
{
    public class Workers
    {
        private string name;
        private string surname;
        private double rate;
        private int days;
        
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
            }
        }
        public double Rate
        {
            get
            {
                return Rate;
            }
            set
            {
                rate = value;
            }
        }
        public int Days
        {
            get
            {
                return days;
            }
            set
            {
                days = value;
            }
        }
        public double GetSalary()
        {
            return rate * days;
        }
    }
}