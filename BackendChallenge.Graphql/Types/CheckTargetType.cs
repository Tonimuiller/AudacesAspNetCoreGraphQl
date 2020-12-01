using BackendChallenge.Core.Model;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendChallenge.Graphql.Types
{
    public class CheckTargetType : ObjectGraphType<CheckTarget>
    {
        public CheckTargetType()
        {
            Field(x => x.Creation, nullable: true);
            Field(x => x.Target);
            Field(x => x.Sequence);
        }
    }
}
