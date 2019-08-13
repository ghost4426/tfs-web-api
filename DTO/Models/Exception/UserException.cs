using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.Exception
{
    public class InvalidUsernameOrPasswordException : ApplicationException
    {
       public InvalidUsernameOrPasswordException(string msg) : base(msg)
        {
        }
    }

    public class DuplicatedUsernameException : ApplicationException
    {
        public DuplicatedUsernameException(string msg) : base(msg)
        {
        }
    }
    public class DuplicatedPremisesNameException : ApplicationException
    {
        public DuplicatedPremisesNameException(string msg) : base(msg)
        {
        }
    }


    public class DeActivedUsernameException : ApplicationException
    {
        public DeActivedUsernameException(string msg) : base(msg)
        {
        }
    }
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string msg) : base(msg)
        {
        }
    }
    public class NotMatchException : ApplicationException
    {
        public NotMatchException(string msg) : base(msg)
        {
        }

    }
}
