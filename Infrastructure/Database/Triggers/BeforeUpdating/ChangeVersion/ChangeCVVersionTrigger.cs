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
    public class ChangeCVVersionTrigger : IBeforeSaveTrigger<CV>
    {
        Task IBeforeSaveTrigger<CV>.BeforeSave(ITriggerContext<CV> context, CancellationToken cancellationToken)
        {
            if (context.UnmodifiedEntity is null) return Task.CompletedTask;
            if (context.Entity.Version != context.UnmodifiedEntity.Version)
            {
                throw new NotEqualItemVersionException();
            }
            context.Entity.Version++;
            return Task.CompletedTask;
        }
    }
}
