namespace Scripts
{
    [DocumentationClass("Lebro", "Whey")]
    public struct SameStruct
    {
        public int Value;
        
        public SameStruct(int value)
        {
            Value = value;
        }
    }
    
    [DocumentationClass("Bebir", "Test fonk")]
    public class BebraTest
    {
        public BebraTest()
        {
            
        }

        public BebraTest(int bober)
        {
            
        }

        public BebraTest(string hlep)
        {
            
        }
        
        [DocumentationMethod("Method")]
        public int SomeMethod(string nuhalBebru)
        {
            return 1;
        }
        [DocumentationMethod("Method")]
        public void SomeMethod1()
        {
            
        }
        [DocumentationMethod("Method")]
        public void SomeMethod7()
        {
            
        }
        [DocumentationMethod("Method")]
        public void SomeMethod5()
        {
            
        }
    }
}