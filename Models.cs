using System;

namespace AFApp
{
    public class SportsIcon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Sports { get; set; }
    }

    public class SportsIconCreateModel
    {
        public string Name { get; set; }
        public string NickName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Sports { get; set; }
    }

    public class SportsIconUpdateModel
    {
        public string NickName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Sports { get; set; }
    }
}
