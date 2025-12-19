namespace IFS.Person.WebApi.Repository.Exceptions
{
    public class IfsCustomException : Exception
    {
        public IfsCustomException(string name)
    : base(String.Format("IFS Exception: {0}", name)) { }
    }
}
