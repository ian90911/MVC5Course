using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVC5Course.Models
{   
	public  class ClientRepository : EFRepository<Client>, IClientRepository
	{
        public List<Client> GetClientByName(string name,int take,int skip)
        {
            var query = this.All();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.FirstName.Contains(name) || x.MiddleName.Contains(name) || x.LastName.Contains(name));
            }
            query = query.Where(x=>x.IsDeleted==false).Include(c => c.Occupation).OrderByDescending(x => x.ClientId).Skip(skip).Take(take);
            return query.ToList();
        }

        public Client GetClientById(int id)
        {
            var query = this.All().FirstOrDefault(x => x.ClientId == id);
            return query;
        }

        public override void Delete(Client entity)
        {
            entity.IsDeleted = true;
            this.UnitOfWork.Context.Entry(entity).State = EntityState.Modified;
            this.UnitOfWork.Commit();
        }
    }

	public  interface IClientRepository : IRepository<Client>
	{
        List<Client> GetClientByName(string name, int take, int skip);
        Client GetClientById(int id);
    }
}