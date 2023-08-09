namespace TestTask.src.Model
{
    public class Soccer {
        public string id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string sex { get; set; }
        public string date { get; set; }
        public string team { get; set; }
        public string country { get; set; }

        public Soccer(string id, string name, string surname, string sex, string date, string team, string country)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.sex = sex;
            this.date = date;
            this.team = team;
            this.country = country;
        }

    };
}
