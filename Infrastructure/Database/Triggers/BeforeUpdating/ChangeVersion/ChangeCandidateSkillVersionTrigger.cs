using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using EntityFrameworkCore.Triggered;
using Infrastructure.Database.Exceptions;

namespace Infrastructure.Database.Triggers.BeforeUpdating.ChangeVersion
{
    public class ChangeCandidateSkillVersionTrigger : IBeforeSaveTrigger<CandidateSkill>
    {
        Task IBeforeSaveTrigger<CandidateSkill>.BeforeSave(ITriggerContext<CandidateSkill> context, CancellationToken cancellationToken)
        {
            if (context.UnmodifiedEntity is null) return Task.CompletedTask;
            if (context.Entity.Version != context.UnmodifiedEntity.Version)
            {
                throw new NotEqualItemVersionException("Candidate skills changed before you edit they. Try edit again");
            }
            context.Entity.Version++;
            return Task.CompletedTask;
        }
    }
}
