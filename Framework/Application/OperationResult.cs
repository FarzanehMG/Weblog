namespace Framework.Application
{
    public class OperationResult
    {
        public bool IsSucceeded { get; set; }
        public string Message { get; set; }

        public OperationResult()
        {
            IsSucceeded = false;
        }

        public OperationResult Succedded(string message = "عملیات با موفقیت انجام شد")
        {
            IsSucceeded = true;
            Message = message;
            return this;
        }

        public OperationResult Failed(string message)
        {
            IsSucceeded = false;
            Message = message;
            return this;
        }

        public OperationResult Failed(object passwordsNotMatch)
        {
            throw new NotImplementedException();
        }
    }
    /*public static class OperationResult
{
    public static OperationResult Succedded(string message = "عملیات با موفقیت انجام شد")
    {
        return new OperationResult(true, message);
    }

    public static OperationResult Failed(string message)
    {
        return new OperationResult(false, message);
    }

    public static OperationResult Failed(object passwordsNotMatch)
    {
        throw new NotImplementedException();
    }
}*/
}