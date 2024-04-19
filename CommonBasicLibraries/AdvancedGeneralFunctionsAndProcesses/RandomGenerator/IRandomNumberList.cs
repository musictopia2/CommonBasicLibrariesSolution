namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.RandomGenerator;
public interface IRandomNumberList
{
    int GetRandomNumber(int maxNumber, int startingPoint = 1, BasicList<int>? previousList = null);
    BasicList<int> GenerateRandomList(int maxNumber, int howMany = -1, int startingNumber = 1, BasicList<int>? previousList = null, BasicList<int>? setToContinue = null, bool putBefore = false);
    int Next(int max);
    int Next(int min, int max);
}