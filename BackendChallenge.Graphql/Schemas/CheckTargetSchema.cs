using BackendChallenge.Graphql.Queries;
using GraphQL.Types;
using System;

namespace BackendChallenge.Graphql.Schemas
{
    public class CheckTargetSchema : Schema, ISchema
    {
        public CheckTargetSchema(IServiceProvider serviceProvider)
            :base(serviceProvider)
        {
            Query = (CheckTargetQuery) serviceProvider.GetService(typeof(CheckTargetQuery));
        }
    }
}
