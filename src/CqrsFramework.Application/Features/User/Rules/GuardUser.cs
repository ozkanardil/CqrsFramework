using CqrsFramework.Infrastructure.Errors;
using CqrsFramework.Infrastructure.Errors.Errors;

namespace CqrsFramework.Application.Features.User.Rules
{
    public static class Guard
    {
        public static GuardClause<T> Against<T>(T value)
        {
            return new GuardClause<T>(value);
        }
    }

    public class GuardClause<T>
    {
        private readonly T _value;

        public GuardClause(T value)
        {
            _value = value;
        }

        public GuardClause<T> Null()
        {
            if (_value == null)
            {
                throw new CustomException("Value cannot be null.", false);
            }

            return this;
        }

        public GuardClause<T> NotNull()
        {
            if (_value != null)
            {
                throw new CustomException("Value must be null.", false);
            }

            return this;
        }

        // Add more methods here for other guard clauses...

        public void KeepGoing()
        {

        }
        //public T Value => _value;
    }

}
