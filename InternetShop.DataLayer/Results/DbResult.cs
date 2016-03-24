using System.Collections.Generic;

namespace InternetShop.DataLayer.Results
{
    public abstract class DbResult
    {
        public List<string> Errors { get; }
        public bool IsSucceeded { get; private set; }

        protected DbResult()
        {
            Errors = new List<string>();
            IsSucceeded = true;
        }

        public void AddError(string error)
        {
            Errors.Add(error);
            IsSucceeded = false;
        }
    }
}