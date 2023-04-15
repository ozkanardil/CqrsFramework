using CqrsFramework.Infrastructure.Errors;
using CqrsFramework.Infrastructure.Errors.Errors;
using CqrsFramework.Application.Features.User.Commands;

namespace CqrsFramework.Application.Features.User.Rules
{
    public static class GuardUserCreate
    {
        public static GuardUserCreateClause Against(CreateUserCommand value)
        {
            return new GuardUserCreateClause(value);
        }
    }

    public class GuardUserCreateClause
    {
        private readonly CreateUserCommand _value;

        public GuardUserCreateClause(CreateUserCommand value)
        {
            _value = value;
        }

        public GuardUserCreateClause Null()
        {
            if (_value.Name == null || _value.Surname == null || _value.Email == null || _value.Password == null)
                throw new CustomException("Value cannot be null.", false);

            return this;
        }

        public GuardUserCreateClause NotNull()
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
        //public CreateUserCommand Value => _value;
    }

}
