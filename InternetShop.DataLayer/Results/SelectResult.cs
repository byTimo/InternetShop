namespace InternetShop.DataLayer.Results
{
    public class SelectResult<T> : DbResult
    {
        public T Result { get; internal set; }
    }
}