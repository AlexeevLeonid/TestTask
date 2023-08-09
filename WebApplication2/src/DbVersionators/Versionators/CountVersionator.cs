using TestTask.src.DbVersionators;

namespace TestTask.src.DbVersionators.CountVersionator
{
    class CountVersionator : IDbVersionator<int>
    {
        private static int version = 0;
        public void СommitDatabaseСhange() => version++;
        public int GetDatabaseVersion() => version;
    }
}
