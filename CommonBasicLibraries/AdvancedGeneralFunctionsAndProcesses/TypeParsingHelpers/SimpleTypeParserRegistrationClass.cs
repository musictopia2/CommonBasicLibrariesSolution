#nullable enable
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.TypeParsingHelpers;
public static class SimpleTypeParserRegistrationClass
{
    public static void RegisterSimpleTypeClasses()
    {
        CustomTypeParsingHelpers<char>.MasterContext = new CharTypeGenerator();
        CustomTypeParsingHelpers<float>.MasterContext = new FloatTypeGenerator();
        CustomTypeParsingHelpers<double>.MasterContext = new DoubleTypeGenerator();
        CustomTypeParsingHelpers<decimal>.MasterContext = new DecimalTypeGenerator();
        CustomTypeParsingHelpers<bool>.MasterContext = new BoolTypeGenerator();
        CustomTypeParsingHelpers<DateOnly>.MasterContext = new DateOnlyTypeGenerator();
        CustomTypeParsingHelpers<string>.MasterContext = new StringTypeGenerator();
        CustomTypeParsingHelpers<int>.MasterContext = new IntegerTypeGenerator();
    }
}