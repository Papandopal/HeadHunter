using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using UseCases.Database.Repositories;
using UseCases.Services.ValuedSkillServices.General.DTOs;

namespace Infrastructure.Database.Repositories
{
    public class PositionRepository(AppDbContext dbContext) : IPositionRepository
    {
        private DbSet<Position> positions = dbContext.Set<Position>();
        void IRepository<Position>.Add(Position entity)
        {
            positions.Add(entity);
        }

        void IRepository<Position>.Delete(Guid id)
        {
            var entity = positions.First(x => x.Id == id);
            positions.Remove(entity);
        }

        void IPositionRepository.DeleteRange(IEnumerable<Guid> ids)
        {
            var removedPositions = positions.Where(x => ids.Contains(x.Id));
            positions.RemoveRange(removedPositions);    
        }

        IQueryable<Position> IRepository<Position>.GetAll()
        {
            return positions.Include(x=>x.ProjectTags).Include(x=>x.AccessRules).ThenInclude(y => y.Skill).Include(x=>x.PositionSkills).ThenInclude(x=>x.Skill);
        }

        Position IRepository<Position>.GetById(Guid id)
        {
            return positions.Include(x=>x.ProjectTags).Include(x=>x.AccessRules).ThenInclude(y => y.Skill).Include(x=>x.PositionSkills).ThenInclude(y=>y.Skill).First(x=>x.Id == id);
        }

        IEnumerable<Position> IPositionRepository.GetByIds(IEnumerable<Guid> ids)
        {
            return positions.Include(x => x.ProjectTags).Include(x => x.AccessRules).ThenInclude(y => y.Skill).Include(x=>x.PositionSkills).ThenInclude(x=>x.Skill).Where(x => ids.Contains(x.Id));
        }

        private Func<ValuedSkillDTO, bool> BuildFunc(AccessRule accessRule)
        { 
            ParameterExpression? parameter = null;
            MemberExpression? member = null;
            ConstantExpression? searchValue = null;

            parameter = Expression.Parameter(typeof(ValuedSkillDTO), typeof(ValuedSkillDTO).Name);
            member = Expression.Property(parameter, typeof(ValuedSkillDTO).Name);

            searchValue = Expression.Constant(accessRule.Value, typeof(string));

            BinaryExpression binaryExpression;

            switch (accessRule.Operator)
            {
                case FilterOperators.Equal:
                    binaryExpression = Expression.Equal(member, searchValue);
                    break;
                case FilterOperators.NotEqual:
                    binaryExpression = Expression.NotEqual(member, searchValue);
                    break;
                case FilterOperators.GreaterThan:
                    binaryExpression = Expression.GreaterThan(member, searchValue);
                    break;
                case FilterOperators.GreaterThanOrEqual:
                    binaryExpression = Expression.GreaterThanOrEqual(member, searchValue);
                    break;
                case FilterOperators.LessThan:
                    binaryExpression = Expression.LessThan(member, searchValue);
                    break;
                case FilterOperators.LessThanOrEqual:
                    binaryExpression = Expression.LessThanOrEqual(member, searchValue);
                    break;
                default:
                    throw new Exception("unknow operator");
            }

            return Expression.Lambda<Func<ValuedSkillDTO, bool>>(binaryExpression, parameter).Compile();
        }

        IEnumerable<Position> IPositionRepository.GetPersonaledPositionsBySkills(IEnumerable<ValuedSkillDTO> valuedSkills)
        {
            var valuedSkillsDictionary = valuedSkills.ToDictionary(x => x.Skill.Id);
            return positions
                .Where(x => x.AccessRules.Select(x => x.SkillId).Except(valuedSkills.Select(x => x.Skill.Id)).Count() == 0).AsEnumerable().Where(x => x.AccessRules.All(y => BuildFunc(y)(valuedSkillsDictionary[y.SkillId])));
        }

        bool IRepository<Position>.IsExists(Position entity)
        {
            return positions.FirstOrDefault(x => x.Id == entity.Id) is not null;
        }

        void IRepository<Position>.Update(Position entity)
        {
            positions.Update(entity);
        }

        Position IPositionRepository.GetByOwnerId(Guid ownerId)
        {
            return positions.First(x=>x.OwnerId == ownerId);
        }
    }
}
