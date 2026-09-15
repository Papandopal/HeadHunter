using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using UseCases.Database.Repositories;

namespace Infrastructure.Database.Repositories
{
    public class SkillRepository(AppDbContext dbContext) : ISkillRepository
    {
        private DbSet<Skill> skills = dbContext.Set<Skill>();
        void IRepository<Skill>.Add(Skill entity)
        {
            skills.Add(entity);
        }

        void IRepository<Skill>.Delete(Guid id)
        {
            skills.Remove(skills.First(x=>x.Id==id));
        }

        IQueryable<Skill> IRepository<Skill>.GetAll()
        {
            return skills.Include(x=>x.Category);
        }

        Skill IRepository<Skill>.GetById(Guid id)
        {
            return skills.Include(x => x.Category).First(x=>x.Id == id);
        }

        Skill ISkillRepository.GetByName(string name)
        {
            return skills.Include(x => x.Category).First(x=>x.Name==name);
        }

        Guid ISkillRepository.GetIdBySkillName(string skillName)
        {
            return skills.Where(x=>x.Name == skillName).Select(x=>x.Id).First();
        }

        SkillTypes ISkillRepository.GetSkillTypeBySkillName(string skillName)
        {
            return skills.Where(x => x.Name == skillName).Select(x => x.Type).First();
        }

        bool IRepository<Skill>.IsExists(Skill entity)
        {
            return skills.Include(x => x.Category).FirstOrDefault(x=>x.Id == entity.Id) is not null;
        }

        void IRepository<Skill>.Update(Skill entity)
        {
            skills.Update(entity);
        }
    }
}
