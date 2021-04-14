using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicLibraries.CollectionClasses;
using CommonBasicLibraries.DatabaseHelpers.Attributes;
using CommonBasicLibraries.DatabaseHelpers.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace CommonBasicLibraries.DatabaseHelpers.MiscClasses
{
    public class ChangeTracker
    {
        private Dictionary<string, object> _originalValues = new ();
        public void PopulateOriginalDictionary(Dictionary<string, object> savedOriginal) //the server has to put in the original dictionary
        {
            _originalValues = savedOriginal;
        }
        public Dictionary<string, object> GetOriginalValues()
        {
            return new Dictionary<string, object>(_originalValues);
        }
        public void Initialize() //from api if updating, needs to call the populateoriginaldictionary
        {
            //you are on your own if you mess up unfortunately.
            _originalValues.Clear();
            var tempList = _thisType.GetProperties().Where(Items => Items.CanMapToDatabase() == true && Items.Name != "ID"); //id can never be tracked
            tempList = tempList.Where(Items => Items.HasAttribute<ExcludeUpdateListenerAttribute>() == false);
            tempList = tempList.Where(Items => Items.HasAttribute<ForeignKeyAttribute>() == false); //because the foreigns would never be updated obviously.
            foreach (PropertyInfo property in tempList)
            {
                _originalValues.Add(property.Name, property.GetValue(_thisObject, null)!);
            }
        }
        private readonly object _thisObject;
        private readonly Type _thisType;
        public ChangeTracker(object thisObject)
        {
            _thisObject = thisObject;
            _thisType = _thisObject.GetType();
        }
        public BasicList<string> GetChanges()
        {
            BasicList<string> output = new ();
            foreach (var thisValue in _originalValues)
            {
                PropertyInfo property = _thisType.GetProperties().Where(Items => Items.Name == thisValue.Key).Single();
                object newValue = property.GetValue(_thisObject, null)!;
                if (IsUpdate(thisValue.Value, newValue) == true)
                {
                    output.Add(property.Name);
                }
            }
            return output;
        }
        private static bool IsUpdate(object thisValue, object newValue)
        {
            if (thisValue == null && newValue == null)
            {
                return false;
            }
            if (thisValue == null)
            {
                return true;
            }
            if (thisValue.Equals(newValue) == false)
            {
                return true;
            }
            return false;
        }
    }
}