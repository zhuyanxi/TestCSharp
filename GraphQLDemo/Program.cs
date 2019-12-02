using System;
using GraphQL;
using GraphQL.Types;

namespace GraphQLDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //var schema = Schema.For(@"
            //  type Query {
            //    hello: String
            //  }
            //");
            //var json = schema.Execute(_ =>
            //{
            //    _.Query = "{ hello }";
            //    _.Root = new { Hello = "Hello World!" };
            //});


            //var schema = Schema.For(@"
            //  type Droid {
            //    id: String!
            //    name: String!
            //  }

            //  type Query {
            //    hero: Droid
            //  }
            //", _ => {
            //    _.Types.Include<Query>();
            //});
            //var json = schema.Execute(_ =>
            //{
            //    _.Query = "{ hero { id name } }";
            //});


            var schema = new Schema { Query = new StarWarsQuery() };
            var json = schema.Execute(_ =>
            {
                _.Query = "{ hero { id name } }";
            });

            Console.WriteLine(json);
            Console.ReadKey();
        }
    }
}
