using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace examTaskVariant2.UtilityClasses
{
    public static class UtilityClass
    {
        private const string testData = "examTaskVariant2.Resources.testData.json";
        private const string configData = "examTaskVariant2.Resources.configData.json";


        public static string ReturnValue(string key,string resourc)
        {
            string dataPath = "";
            switch (resourc)
            {
                case "testdata":
                    dataPath = testData;
                    break;
                case "config":
                    dataPath = configData;
                    break;
                default:
                    break;
            }

            string text;
            var assembly = typeof(ExamTest).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream(dataPath);
            using (var reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }

            Dictionary<string, string> keyValuePairs = DeSerializeClass.DeserializeObject<Dictionary<string, string>>(text);
            return keyValuePairs[key];
        }        

        public static long GetRidOfLettersAndSymbols(string str)
        {
            string pattern = @"\D";
            string target = "";
            Regex regex = new Regex(pattern);
            string strWithoutLetters = regex.Replace(str, target);
            long answer = Convert.ToInt64(strWithoutLetters);
            return answer;
        }
       
        public static string ReturnLogString()
        {
            var arrStr = File.ReadAllLines(ReturnValue("pathToLog","config"));
            string str = String.Join('\n', arrStr);
            return str;
        } 
        
        public static string ReturnDigitFromEndOfString(string str)
        {
            string digit = "";
            for (int i = str.Length - 1; 0 < i; i--)
            {
                if (Char.IsDigit(str[i]))
                {
                    digit += str[i];
                }
                else
                {
                    return digit;
                }
            }
            return digit;
        }

    }
}
