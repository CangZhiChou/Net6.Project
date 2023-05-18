namespace Advanced.NET6.ExceptionService
{
    public class ExceptionInFoService
    {
        public void Show()
        {
            throw new Exception("Service层的异常     ");
        }
    }
}