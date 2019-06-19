using BusinessLogic.IBusinessLogic;
using DataAccess.IRepositories;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessLogicImpl
{
    public class RoleBLImpl : IRoleBL
    {
        private IRoleRepository roleRepository;

        public RoleBLImpl(IRoleRepository roleRepository)
        {
            if (roleRepository != null)
                this.roleRepository = roleRepository;
        }

        public async Task<Role> GetById(int id)
        {
           return await roleRepository.GetByIdAsync(id);
        }
    }
}
