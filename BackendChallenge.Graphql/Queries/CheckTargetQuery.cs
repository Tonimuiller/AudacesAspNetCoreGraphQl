using BackendChallenge.Core.Model;
using BackendChallenge.Core.Repository;
using BackendChallenge.Core.Services;
using BackendChallenge.Graphql.Types;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendChallenge.Graphql.Queries
{
    public class CheckTargetQuery : ObjectGraphType
    {
        private readonly ICheckTargetRepository checkTargetRepository;
        private readonly ICheckTargetService checkTargetService;

        public CheckTargetQuery(ICheckTargetRepository checkTargetRepository,
            ICheckTargetService checkTargetService)
        {
            this.checkTargetRepository = checkTargetRepository;
            this.checkTargetService = checkTargetService;

            FieldAsync<ListGraphType<CheckTargetType>>("cheks",
                arguments: new QueryArguments(
                    new QueryArgument<DateGraphType> { Name = "start" },
                    new QueryArgument<DateGraphType> { Name = "end" }
                ), resolve: async context =>
                {
                    var start = context.GetArgument<DateTime>("start");
                    var end = context.GetArgument<DateTime>("end");
                    return await this.checkTargetRepository.GetCheckTargetByPeriodAsync(start, end);
                });

            FieldAsync<ListGraphType<IntGraphType>>("checkTarget",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "target" },
                    new QueryArgument<ListGraphType<IntGraphType>> { Name = "sequence" }
                ), resolve: async context =>
                {
                    var target = context.GetArgument<int>("target");
                    var sequence = context.GetArgument<List<int>>("sequence");
                    return await this.checkTargetService.CheckAsync(new CheckTarget
                    {
                        Target = target,
                        Sequence = sequence
                    });
                });
        }
    }
}
