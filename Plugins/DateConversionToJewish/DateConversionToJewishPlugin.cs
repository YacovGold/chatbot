using BasePlugin.Interfaces;
using BasePlugin.Records;
using System;
using System.Globalization;
using System.Linq;
using System.Text;


namespace DateConversionToJewish
{
    public class DateConversionToJewishPlugin : IPlugin

    {
        public const string _Id = "DateConversionToJewish";
        public string Id => _Id;

        public void Execute(PluginInput input)
        {
            if (input.Message == "")
            {
                input.Callbacks.SendMessage("Please enter a date: Day/Month/Year ");
            }
            else
            {
                Console.OutputEncoding = new UTF8Encoding();
                var res = GetHebrewJewishDateString(DateTime.Parse(input.Message));
                input.Callbacks.SendMessage(res);
            }
        }


        public string GetHebrewJewishDateString(DateTime anyDate)
        {
            System.Text.StringBuilder hebrewFormatedString = new System.Text.StringBuilder();
            // Create the hebrew culture to use hebrew (Jewish) calendar 
            CultureInfo jewishCulture = CultureInfo.CreateSpecificCulture("he-IL");
            jewishCulture.DateTimeFormat.Calendar = new HebrewCalendar();

            // Day of the week in the format " " 
            hebrewFormatedString.Append(anyDate.ToString("dddd", jewishCulture) + " ");
            // Day of the month in the format "'" 
            hebrewFormatedString.Append(anyDate.ToString("dd", jewishCulture) + " ");
            // Month and year in the format " " 
            hebrewFormatedString.Append("" + anyDate.ToString("y", jewishCulture));

            return hebrewFormatedString.ToString();
        }
    }
}
