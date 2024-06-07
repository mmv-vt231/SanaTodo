using App.GraphQL.Query;
using GraphQL.Types;

namespace App.GraphQL.Mutation
{
    public class RootMutation : ObjectGraphType
    {
        public RootMutation()
        {
            Field<TaskMutation>("taskMutation").Resolve(_ => new { });
        }
    }
}
