namespace CommonBasicLibraries.DatabaseHelpers.SourceGeneratorHelpers;
public interface ICommandQuery<E>
    where E : class
{
    abstract static BasicList<E> Query(IDbCommand command, EnumDatabaseCategory category);
    abstract static Task<BasicList<E>> QueryAsync(IDbCommand command, EnumDatabaseCategory category);
}
public interface ICommandQuery<E, R>
    where E : class
{
    abstract static BasicList<R> Query(IDbCommand command, EnumDatabaseCategory category);
    abstract static Task<BasicList<R>> QueryAsync(IDbCommand command, EnumDatabaseCategory category);
}
public interface ICommandQuery<TFirst, TSecond, TReturn>
    where TFirst : class
    where TSecond : class
    where TReturn : class
{
    abstract static BasicList<TReturn> Query(IDbCommand command, Func<TFirst, TSecond?, TFirst> action, EnumDatabaseCategory category);
    abstract static Task<BasicList<TReturn>> QueryAsync(IDbCommand command, Func<TFirst, TSecond?, TFirst> action, EnumDatabaseCategory category);
}
public interface ICommandQuery<TFirst, TSecond, TThird, TReturn>
    where TFirst : class
    where TSecond : class
    where TThird : class
    where TReturn : class
{
    abstract static BasicList<TReturn> Query(IDbCommand command, Func<TFirst, TSecond?, TThird?, TFirst> action, EnumDatabaseCategory category);
    abstract static Task<BasicList<TReturn>> QueryAsync(IDbCommand command, Func<TFirst, TSecond?, TThird?, TFirst> action, EnumDatabaseCategory category);
}