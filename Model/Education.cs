namespace Company.Function.Model
{
    public class Education
    {
        //Experience Parameters
        public int RowKey { get; }
        public string Name { get; }
        public string ImageHREF { get; }
        public string Institution { get; }
        public string StartDate { get; }
        public string EndDate { get; }

        //Experience Constructor
        public Education(
            int rowKey,
            string name,
            string imageHREF,
            string institution,
            string startDate,
            string endDate) {
                RowKey = rowKey;
                Name = name;
                ImageHREF = imageHREF;
                Institution = institution;
                StartDate = startDate;
                EndDate = endDate;
        }
    }
}