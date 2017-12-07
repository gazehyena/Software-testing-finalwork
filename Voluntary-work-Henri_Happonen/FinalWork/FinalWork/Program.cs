using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Threading;
using Projectdll;

namespace FinalWork
{
    /*! This class uses Projectdll to create, read and write registry values. */
    class Program
    {
        const string userRoot = "HKEY_CURRENT_USER";
        const string subkey = "HenriHapponen";
        const string keyName = userRoot + "\\" + subkey;
        int number = 1234;
        bool deleteOnClose = false;
        bool isLoopping = false;

        /**
        * Main method that is responsible for starting and running the program.
        * @param args string array holding command line arguments
        */
        static void Main(string[] args)
        {
            Program registryProgram = new Program();
            registryProgram.ReadCommanlineArguments(args);

            Class1 testDll = new Class1();

            testDll.CreateIntegerRegistryKey(keyName, registryProgram.number);
            testDll.CreateStringRegistryKey(keyName, "Default value");

            registryProgram.PrintRegistryValues(testDll);
            registryProgram.ReadUserInput(testDll);
            registryProgram.CloseProgram(testDll);
        }

        /**
        * A void function that loops through command line arguments and sets program running state according to command line arguments.
        * @param args string array holding command line arguments
        */
        private void ReadCommanlineArguments(string[] args)
        {
            foreach (string arg in args)
            {
                switch (arg.Substring(0, 2).ToUpper())
                {
                    case "-C":
                        Console.WriteLine("Clean up mode");
                        deleteOnClose = true;
                        break;
                    case "-L":
                        Console.WriteLine("Loop mode");
                        isLoopping = true;
                        break;
                    case "-H":
                        Console.WriteLine("Command line options: " +
                            "\n-C clean registry after using program" +
                            "\n-L run program in a loop" +
                            "\n-H show command line options");
                        break;
                    default:
                        Console.WriteLine("Unrecognized argument: " + arg.Substring(0, 2).ToUpper());
                        break;
                }
            }
        }

        /**
        * Function to print registry values
        * @param dll Class1 object, reference to dll
        */
        private void PrintRegistryValues(Class1 dll)
        {
            Console.WriteLine("Reading default registry values: ");
            dll.ReadRegistryValues(keyName);
        }

        /**
        * Function to read user input to be written in the registry
        * @param dll Class1 object, reference to dll
        */
        private void ReadUserInput(Class1 dll)
        {
            do
            {
                Console.WriteLine("Give an string to write in registry");
                if (isLoopping)
                    Console.WriteLine("Q exits the application");
                string stringLine = Console.ReadLine();
                if (stringLine != "Q")
                {
                    Console.WriteLine("Give an integer to write in registry");
                    string integerLine = Console.ReadLine();
                    while (!int.TryParse(integerLine, out number))
                    {
                        Console.WriteLine("Input was not an integer, enter numeric value:");
                        integerLine = Console.ReadLine();
                    }

                    dll.CreateIntegerRegistryKey(keyName, number);
                    dll.CreateStringRegistryKey(keyName, stringLine);
                }
                else
                    isLoopping = false;
            } while (isLoopping);
        }

        /**
        * Function to show goodbye message to user and call registry clean up if program was run in clean up mode.
        * @param dll Class1 object, reference to dll
        */
        private void CloseProgram(Class1 dll)
        {
            if (deleteOnClose)
            {
                Console.WriteLine("Closing program and deleting registry entries. Peace out!");
                dll.DeleteRegistryKey(subkey);
                Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine("Closing program!");
                Thread.Sleep(1000);
            }
        }
    }
}
