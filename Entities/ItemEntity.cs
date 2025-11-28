namespace ItemTrackerAPI.Entities
{
    public class ItemEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // entity specific methods and properties
        public void UpdateName(string name)
        {
            this.Name = name;
        }
    }
}
