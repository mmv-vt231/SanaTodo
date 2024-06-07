using GraphQL.Types;

namespace App.GraphQL.Query
{
    public class RootQuery : ObjectGraphType    
    {
        public RootQuery() {
            Field<CategoryQuery>("categoryQuery").Resolve(_ => new { });
            Field<TaskQuery>("taskQuery").Resolve(_ => new { });
        }
    }
}
