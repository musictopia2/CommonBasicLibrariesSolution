using System.Xml.Linq; //not common enough this time to put to global for now.
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
public class XMLBuilder
{
    readonly Stack<XElement> _elements = new();
    XElement? _lastElement;
    string _firstTag = "";
    string _nsValue = "";
    public void OpenElement(string value)
    {
        XElement element = new(value);

        if (_elements.Count > 0)
        {
            _elements.Peek().Add(element);
        }
        _elements.Push(element);
        if (_firstTag == "")
        {
            _firstTag = value;
        }
    }
    public void AddAttribute(string name, string value)
    {
        if (name == "xmlns")
        {
            if (_nsValue == "")
            {
                _nsValue = value;
            }
            return;
        }
        _elements.Peek().SetAttributeValue(name, value);
    }
    public void AddContent(string content)
    {
        _elements.Peek().Value = content;
    }
    public string GetContent()
    {
        if (_elements.Count is not 0)
        {
            throw new CustomBasicException("You have elements that has not been closed");
        }
        if (_lastElement is null)
        {
            throw new CustomBasicException("You never closed anything");
        }
        string output = _lastElement.ToString();
        if (_nsValue != "")
        {
            output = output.Replace($"<{_firstTag}", $"<{_firstTag} xmlns={Constants.QQ}{_nsValue}{Constants.QQ}");
        }
        return output;
    }
    public void CloseElement()
    {
        _lastElement = _elements.Pop();
    }
}