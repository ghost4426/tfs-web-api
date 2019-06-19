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

    public class DulicatedUsernameException : ApplicationException
    {
        public DulicatedUsernameException(string msg) : base(msg)
        {
        }
    }
}
