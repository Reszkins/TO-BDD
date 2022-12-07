namespace TO_BDD.Enums
{
    public enum BookType
    {
        HISTORICAL,
        HORROR,
        FANTASY
    }

    public class BookTypeMapper 
    { 
        public static string GetBookTypeString(BookType type)
        {
            switch (type) 
            {
                case BookType.HISTORICAL:
                    return "historical";
                case BookType.HORROR:
                    return "horror";
                case BookType.FANTASY:
                    return "fantasy";
                default:
                    return "-";
            }
        }
    }

}
