using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;
using Domain.Enums;

namespace Domain
{
    public static class CandidateSkillExtensions
    {
        public static void ChangeValue(this CandidateSkill skill, string value)
        {
            switch (skill.Skill.Type)
            {
                case SkillTypes.Period:
                    var parts = skill.Value.Split('-');
                    if(value.First() == '-')
                    {
                        skill.Value = parts[0] + value;
                    }
                    else
                    {
                        skill.Value = new StringBuilder().Append(value).Append('-').Append(parts[1]).ToString();
                    }
                    break;
                default:
                    skill.Value = value;
                    break;
            }
        }
    }
}
