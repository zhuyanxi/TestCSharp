using System;
using GraphQL;
using GraphQL.Types;

namespace GraphQLDemo
{
    public class Droid
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class DroidType : ObjectGraphType<Droid>
    {
        public DroidType()
        {
            Field(x => x.Id).Description("The Id of the Droid.");
            Field(x => x.Name).Description("The name of the Droid.");
        }
    }

    public class Query
    {
        [GraphQLMetadata("hero")]
        public Droid GetHero() => new Droid { Id = "1", Name = "R2-D2" };
        //{
        //    return new Droid { Id = "1", Name = "R2-D2" };
        //}
    }

    public class StarWarsQuery : ObjectGraphType
    {
        public StarWarsQuery()
        {
            Field<DroidType>(
              "hero",
              resolve: context => new Droid { Id = "1", Name = "R2-D2" }
            );
        }
    }
}
