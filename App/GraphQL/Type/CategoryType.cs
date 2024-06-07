using App.Models;
using GraphQL.Types;

namespace App.GraphQL.Type
{
    public class CategoryType : ObjectGraphType<Category>
    {
        public CategoryType() {
            Field(c => c.Id);
            Field(c => c.Name);
        }
    }
}
