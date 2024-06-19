using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CapitalPlacementTest.Common
{
    public static class QuestionType
    {
        public const string YesNoQuestion = "YesNo";
        public const string ParagraghQuestion = "Paragragh";
        public const string MultipleChoiceQuestion = "MultipleChoice";
        public const string NumberQuestion = "Number";
        public const string DateQuestion = "Date";
        public const string DropDownQuestion = "DropDown";
        public static List<string> questionTypes = new List<string>() { "YesNo", "Paragragh", "MultipleChoice", "Number", "Date", "DropDown" };


        public static string ValidateQuestionType(string questionType)
        {
            if (string.IsNullOrEmpty(questionType))
            {
               return string.Empty;
            }

            foreach(var type in questionTypes)
            {
                if(type.Equals(questionType, StringComparison.OrdinalIgnoreCase))
                {
                    return type;
                }
            }
            
          return string.Empty;
        }

        public static bool ValidateQuestionType(List<string> choices, string questionType)
        {
            if (
                !questionType.Equals(DropDownQuestion, StringComparison.OrdinalIgnoreCase) 
                && 
                !questionType.Equals(MultipleChoiceQuestion, StringComparison.OrdinalIgnoreCase)
                && choices.Count > 0
                )
            {
                return false;
            }

            return true;
        }
    }
}
