using Test66bit2.src.DbVersionators;

namespace Test66bit2.src.DbVersionators.CountVersionator
{
    class CountVersionator : IDbVersionator<int>
    {
        private static int version = 0;
        public void СommitDatabaseСhange() => version++;
        public int GetDatabaseVersion() => version;
    }
}
