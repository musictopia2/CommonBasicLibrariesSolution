namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.CSharpCodeWriters;
public interface ICommentBlock
{
    ICommentBlock WriteLine(string text);
    ICommentBlock WriteLine(Action<IWriter> action); //since this is intended to be all comments, no chances for code blocks
}