using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using MVC5Course.ViewModel;

namespace MVC5Course.Models
{   
	public  class ClientRepository : EFRepository<Client>, IClientRepository
	{
        public List<Client> Search(ClientQueryCondition cond)
        {
            var query = this.All();
            if (!string.IsNullOrEmpty(cond.name))
            {
                query = query.Where(x => x.FirstName.Contains(cond.name) || x.MiddleName.Contains(cond.name) || x.LastName.Contains(cond.name));
            }
            if (cond.CreditRating.HasValue)
            {
                query = query.Where(x => x.CreditRating == cond.CreditRating.Value);
            }
            query = query.Include(c => c.Occupation).OrderByDescending(x => x.ClientId).Skip(cond.skip).Take(cond.take);
            return query.ToList();
        }

        public List<Client> GetClientByName(string name,int take,int skip)
        {
            var query = this.All();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.FirstName.Contains(name) || x.MiddleName.Contains(name) || x.LastName.Contains(name));
            }
            query = query.Include(c => c.Occupation).OrderByDescending(x => x.ClientId).Skip(skip).Take(take);
            return query.ToList();
        }

        public Client GetClientById(int id)
        {
            var query = this.All().FirstOrDefault(x => x.ClientId == id);
            return query;
        }

        public override IQueryable<Client> All()
        {
            return base.All().Where(x=>x.IsDeleted==false);
        }

        public override void Delete(Client entity)
        {
            entity.IsDeleted = true;
        }
    }

	public  interface IClientRepository : IRepository<Client>
	{
        List<Client> GetClientByName(string name, int take, int skip);
        Client GetClientById(int id);
        List<Client> Search(ClientQueryCondition cond);
    }
}