namespace Test66bit2.src.DbVersionators
{
    public interface IDbVersionator<TValue>
    {
        void СommitDatabaseСhange();
        TValue GetDatabaseVersion();
    }
}
