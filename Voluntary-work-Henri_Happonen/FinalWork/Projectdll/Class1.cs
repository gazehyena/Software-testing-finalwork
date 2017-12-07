using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Projectdll
{
    public class Class1
    {
        /**
        * Function to create registry key with integer value
        * @param keyName specifying registry key name
        * @param value specifies integer registry value
        */
        public void CreateIntegerRegistryKey(string keyName, int value)
        {
            Registry.SetValue(keyName, "Integer value", value);
        }

        /**
        * Function to create registry key with string value
        * @param keyName specifying registry key name
        * @param value specifies string registry value
        */
        public void CreateStringRegistryKey(string keyName, string value)
        {
            Registry.SetValue(keyName, "String value", value);
        }

        /**
        * Function to read registry values from specified key
        * @param keyName specifying registry key name
        */
        public void ReadRegistryValues(string keyName)
        {
            int integerRegistryValue = (int)Registry.GetValue(keyName, "Integer value", -1);
            Console.WriteLine("Integer value: {0}", integerRegistryValue);

            string stringRegistryValue = (string)Registry.GetValue(keyName, "String value", "Value does not exist");
            Console.WriteLine("String value: {0}", stringRegistryValue);
        }

        /**
        * Function to delete registry key with string value
        * @param subKey specifying subkey for registry
        */
        public void DeleteRegistryKey(string subkey)
        {
            Registry.CurrentUser.DeleteSubKey(subkey);
        }
    }
}
