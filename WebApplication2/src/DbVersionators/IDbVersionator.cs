namespace TestTask.src.DbVersionators
{
    public interface IDbVersionator<TValue>
    {
        void СommitDatabaseСhange();
        TValue GetDatabaseVersion();
    }
}
