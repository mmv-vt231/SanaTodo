using GraphQL.Types;

namespace App.GraphQL.Type
{
    public class TaskInputType : InputObjectGraphType
    {
        public TaskInputType()
        {
            Field<IntGraphType>("id");
            Field<StringGraphType>("text");
            Field<BooleanGraphType>("completed");
            Field<DateTimeGraphType>("endDate");
            Field<IntGraphType>("categoryId");
        }
    }
}
