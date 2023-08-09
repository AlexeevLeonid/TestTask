namespace TestTask.src.Model
{
    public static class SoccerExtension
    {
        public static Soccer ToSoccer(this List<string> list)
        {
            if (list.Count() != 7) throw new ArgumentException();
            return new Soccer(list[0], list[1], list[2], list[3], list[4], list[5], list[6]);
        }

        public static bool HasEmptyFieldExceptId(this Soccer soccer)
        {
            return soccer.name == "" ||
                soccer.surname == "" ||
                soccer.sex == "" ||
                soccer.date == "" ||
                soccer.team == "" ||
                soccer.country == "";
        }
    }
}
