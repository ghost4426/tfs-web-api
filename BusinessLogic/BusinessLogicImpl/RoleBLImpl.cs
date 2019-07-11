using BusinessLogic.IBusinessLogic;
using DataAccess.IRepositories;
using Entities = DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessLogicImpl
{
    public class RoleBLImpl : IRoleBL
    {
        private IRoleRepository _roleRepos;

        public RoleBLImpl(IRoleRepository roleRepos)
        {
                _roleRepos = roleRepos;
        }

        public async Task<Entities.Role> GetById(int id)
        {
           return await _roleRepos.GetByIdAsync(id);
        }
        public async Task<IList<Entities.Role>> GetAllRole()
        {
            IList<Entities.Role> roleList = await _roleRepos.GetAllAsync();
            return roleList;
        }
    }
}
