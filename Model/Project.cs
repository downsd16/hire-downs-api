namespace Company.Function.Model
{
    public class Project
    {
        //Project Parameters
        public int RowKey { get; }
        public string Name { get; }
        public string ImageHREF { get; }
        public string Description { get; }
        public string Repository { get; }
        public string[] Skills { get; }
        public string[] Tags { get; }

        //Project Constructor
        public Project(
            int rowKey,
            string name,
            string imageHREF,
            string description,
            string repository,
            string[] skills,
            string[] tags) {
                RowKey = rowKey;
                Name = name;
                ImageHREF = imageHREF;
                Description = description;
                Repository = repository;
                Skills = skills;
                Tags = tags;
        }
    }
}